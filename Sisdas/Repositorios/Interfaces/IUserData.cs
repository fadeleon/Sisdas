using Sisdas.Data;
using Sisdas.Models.Otros;

namespace Sisdas.Repositorios.Interfaces;

public interface IUserData
{
    Task<ResultModel> CreateUser(ApplicationUser UserData);
    Task<ResultModel> LoginAD(string UserName, string Password);
    Task<ResultGenericModel<ActiveDirectoryUserModel>> FindUserByEmail(string Email);
}