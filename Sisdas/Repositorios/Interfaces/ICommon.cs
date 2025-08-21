using Sisdas.Models.Entities.Sisdas;
using Sisdas.Models.Otros;

namespace Sisdas.Repositorios.Interfaces;

public interface ICommon
{
    Task<string> GetFakePassword();
    Task<List<CatRegionSalud>> GetJustRegiones();
    Task<List<ListModel>> GetInstalaciones(string filtroInst);
    Task<ListModel?> GetInstalacionById(int id);
    Task<List<ListModel>> GetProvincias();
    Task<List<ListModel>> GetRegiones(int ProvinciaId);
    Task<List<ListModel>> GetDistritos(int RegionId);
    Task<List<ListModel>> GetCorregimientos(int DistritoId);
}