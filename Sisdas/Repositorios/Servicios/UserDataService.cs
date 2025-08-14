using System.Net;
using Sisdas.Models.Otros;
using Sisdas.Repositorios.Interfaces;

namespace Sisdas.Repositorios.Servicios;

public class UserDataService  : IUserData
{
    private readonly ActiveDirectoryApiModel ActiveDirectoryModel;
    private readonly HttpClient _HttpClient;
    
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
}