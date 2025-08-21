using Sisdas.Data;
using Sisdas.Models.Entities.Sisdas;
using Sisdas.Models.Otros;

namespace Sisdas.Repositorios.Interfaces;

public interface IUserData
{
    Task<ResultModel> CreateUser(ApplicationUser UserData, string password);
    Task<ResultModel> LoginAD(string UserName, string Password);
    Task<ResultGenericModel<ActiveDirectoryUserModel>> FindUserByEmail(string Email);
    Task<ResultModel> CrearPersona(TblPersonas persona);
}