using Sisdas.Models.Otros;

namespace Sisdas.Repositorios.Interfaces;

public interface IUserData
{
    Task<ResultModel> LoginAD(string UserName, string Password);
}