using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sisdas.Data;
using Sisdas.Models.Entities.Sisdas;
using Sisdas.Models.Otros;
using Sisdas.Repositorios.Interfaces;

namespace Sisdas.Repositorios.Servicios;

public class UserDataService  : IUserData
{
    private readonly IDbContextFactory<DBContextSisdas> _Context;
    private readonly IConfiguration _Configuration;
    private readonly ActiveDirectoryApiModel ActiveDirectoryModel;
    private readonly HttpClient _HttpClient;
    private readonly UserManager<ApplicationUser> _UserManager;
    private readonly string FakePassword = "";

    public UserDataService(UserManager<ApplicationUser> UserManager, IConfiguration Configuration, HttpClient HttpClient, IDbContextFactory<DBContextSisdas> Context)
    {
        _Context = Context;
        _UserManager = UserManager;
        _Configuration = Configuration;
        FakePassword = _Configuration["FakePass"] ?? "";
        ActiveDirectoryModel = new ActiveDirectoryApiModel()
        {
            BaseUrl = _Configuration["API_INFO:URL"] ?? "",
            Token = _Configuration["API_INFO:Token"] ?? "",
        };
        _HttpClient = HttpClient;
        _HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", ActiveDirectoryModel.Token);
        _HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<ResultModel> CreateUser(ApplicationUser UserData, string password)
    {
        ResultModel ResultModel;
        try
        {
            var checkUser = await _UserManager.FindByEmailAsync(UserData.Email);
            if (checkUser != null)
            {
                return new ResultModel()
                {
                    Resultado = false,
                    Mensaje = "El usuario ya existe",
                };
            }

            var user = await _UserManager.CreateAsync(UserData, password);
            if (!user.Succeeded)
            {
                return new ResultModel()
                {
                    Resultado = false,
                    Mensaje = "Error creando el usuario.",
                };
            }

            return new ResultModel()
            {
                Resultado = true,
                Mensaje = $"El usuario fue creado correctamente.",
            };
        }
        catch (Exception ex)
        {
            return new ResultModel()
            {
                Resultado = false,
                Mensaje = $"Error: {ex.Message}.",
            };
        }
    }
    
    public async Task<ResultModel> LoginAD(string UserName, string Password)
    {
        try
        {
            string EncodedUserName = WebUtility.UrlEncode(UserName);
            string EncodedPassword = WebUtility.UrlEncode(Password);
            string LoginUrl = $"{ActiveDirectoryModel.BaseUrl}login/{EncodedUserName}/{EncodedPassword}";
            string Result = await _HttpClient.GetFromJsonAsync<string>(LoginUrl) ?? "";
            return new ResultModel()
            {
                Resultado = true,
                Mensaje = Result,
            };
        }
        catch (HttpRequestException ex)
        {
            return new ResultModel()
            {
                Resultado = false,
                Mensaje = ex.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "Error: Acceso denegado." : "",
            };
        }
    }
    
    public async Task<ResultGenericModel<ActiveDirectoryUserModel>> FindUserByEmail(string Email)
    {
        ResultGenericModel<ActiveDirectoryUserModel> ResultUserModel =
            new ResultGenericModel<ActiveDirectoryUserModel>();
        try
        {
            string EncodedEmail = WebUtility.UrlEncode(Email);
            string FindUrl = $"{ActiveDirectoryModel.BaseUrl}findbyemail/{EncodedEmail}";
            var data = await _HttpClient.GetFromJsonAsync<ActiveDirectoryUserModel>(FindUrl);
            if (data == null)
            {
                ResultUserModel.Success = false;
                ResultUserModel.Message = "No se encontro el usuario.";
                ResultUserModel.Data = null;
            }
            else
            {
                ResultUserModel.Success = true;
                ResultUserModel.Message = "Usuario encontrado.";
                ResultUserModel.Data = data;
            }
        }
        catch (HttpRequestException ex)
        {
            ResultUserModel.Success = false;
            ResultUserModel.Message =
                ex.StatusCode == System.Net.HttpStatusCode.Unauthorized ? "Error: Acceso denegado." : ex.Message;
            ResultUserModel.Data = null;
        }

        return ResultUserModel;
    }
    
    public async Task<ResultModel> CrearPersona(TblPersonas persona)
    {
        try
        {
            await using var context = await _Context.CreateDbContextAsync();

            bool existe = await context.TblPersonas
                .AnyAsync(p => p.NumeroIdentificacion == persona.NumeroIdentificacion);

            if (existe)
            {
                return new ResultModel
                {
                    Resultado = false,
                    Mensaje = "Ya existe una persona con ese número de identificación."
                };
            }
            
            persona.AuditoriaFecha = DateTime.Now;
            persona.AuditoriaUsuario = 1;
            context.TblPersonas.Add(persona);
            await context.SaveChangesAsync();

            return new ResultModel
            {
                Resultado = true,
                Mensaje = "Persona creada correctamente"
            };
        }
        catch (Exception ex)
        {
            return new ResultModel
            {
                Resultado = false,
                Mensaje = $"Error al crear persona: {ex.Message}"
            };
        }
    }
    
    public async Task<List<TblPersonas>> BuscarPersonas(string term)
    {
        try
        {
            await using var context = await _Context.CreateDbContextAsync();

            if (string.IsNullOrWhiteSpace(term))
                return new List<TblPersonas>();

            var filtro = term.Trim().ToUpper();

            var pattern = $"%{filtro}%";

            var query = context.TblPersonas.AsQueryable();

            query = query.Where(p =>
                EF.Functions.Like((p.NumeroIdentificacion ?? "").ToUpper(), pattern)
                ||
                EF.Functions.Like(
                    (
                        (p.PrimerNombre ?? "") + " " +
                        (p.SegundoNombre ?? "") + " " +
                        (p.PrimerApellido ?? "") + " " +
                        (p.SegundoApellido ?? "")
                    ).ToUpper(),
                    pattern
                )
            );

            var results = await query
                .OrderBy(p => p.PrimerApellido)
                .ThenBy(p => p.PrimerNombre)
                .Take(200)
                .ToListAsync();

            return results;
        }
        catch (Exception)
        {
            return new List<TblPersonas>();
        }
    }

    public async Task<ResultModel> GuardarHistoricoProcedimiento(TblCasosAdmisiones entidad)
    {
        try
        {
            await using var context = await _Context.CreateDbContextAsync();

            try
            {
                entidad.AuditoriaFecha = DateTime.Now;
            }
            catch
            {
            }

            context.TblCasosAdmisiones.Add(entidad);
            await context.SaveChangesAsync();

            return new ResultModel
            {
                Resultado = true,
                Mensaje = "Registro guardado correctamente."
            };
        }
        catch (DbUpdateException dbEx)
        {
            return new ResultModel
            {
                Resultado = false,
                Mensaje = $"Error al guardar el procedimiento (BD): {dbEx.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ResultModel
            {
                Resultado = false,
                Mensaje = $"Error al guardar el procedimiento: {ex.Message}"
            };
        }
    }

}