using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Sisdas.Data;
using Sisdas.Models.Otros;
using Sisdas.Repositorios.Interfaces;

namespace Sisdas.Repositorios.Servicios;

public class UserDataService  : IUserData
{
    private readonly IConfiguration _Configuration;
    private readonly ActiveDirectoryApiModel ActiveDirectoryModel;
    private readonly HttpClient _HttpClient;
    private readonly UserManager<ApplicationUser> _UserManager;
    private readonly string FakePassword = "";

    public UserDataService(UserManager<ApplicationUser> UserManager, IConfiguration Configuration, HttpClient HttpClient)
    {
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

    public async Task<ResultModel> CreateUser(ApplicationUser UserData)
    {
        ResultModel ResultModel;
        try
        {
            var checkUser = await _UserManager.FindByEmailAsync(UserData.Email);
            if (checkUser != null)
            {
                return ResultModel = new ResultModel()
                {
                    Resultado = false,
                    Mensaje = "El usuario ya existe",
                };
            }
            
            var user = await _UserManager.CreateAsync(UserData, FakePassword);
            if (!user.Succeeded)
            {
                ResultModel = new ResultModel()
                {
                    Resultado = false,
                    Mensaje = "Error creando el usuario.",
                };
            }

            if (!user.Succeeded)
            {
                return ResultModel = new ResultModel()
                {
                    Resultado = false,
                    Mensaje = "Error creando el usuario.",
                };
            }

            return ResultModel = new ResultModel()
            {
                Resultado = true,
                Mensaje = $"El usuario fue creado correctamente.",
            };
        }
        catch (Exception ex)
        {
            return ResultModel = new ResultModel()
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
}