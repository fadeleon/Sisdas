using Sisdas.Models.Entities.Sisdas;
using Sisdas.Models.Otros;

namespace Sisdas.Repositorios.Interfaces;

public interface ICommon
{
    Task<string> GetFakePassword();
    Task<List<CatRegionSalud>> GetRegiones();
    Task<List<ListModel>> GetInstalaciones(string filtroInst);
}