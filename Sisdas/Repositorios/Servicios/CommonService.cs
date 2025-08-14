using Microsoft.EntityFrameworkCore;
using Sisdas.Models.Entities.Sisdas;
using Sisdas.Models.Otros;
using Sisdas.Repositorios.Interfaces;

namespace Sisdas.Repositorios.Servicios;

public class CommonService : ICommon
{
    private readonly IConfiguration _Configuration;
    private readonly IDbContextFactory<DBContextSisdas> _ContextCommon;
    
    public CommonService(IConfiguration Configuration, IDbContextFactory<DBContextSisdas> ContextCommon)
    {
        _Configuration = Configuration;
        _ContextCommon = ContextCommon;
    }
    public async Task<string> GetFakePassword()
    {
        string Password = _Configuration.GetSection("FakePass").Value ?? "";
        return await Task.FromResult(Password);
    }
    
    public async Task<List<CatRegionSalud>> GetRegiones()
    {
        List<CatRegionSalud> regiones = new List<CatRegionSalud>();
        using (var localContext = await _ContextCommon.CreateDbContextAsync())
        {
            regiones = await localContext.CatRegionSalud.OrderBy(x => x.NombreRegion).ToListAsync();
        }
        return regiones;
    }
    
    public async Task<List<ListModel>> GetInstalaciones(string filtroInst)
    {
        try
        {
            await using var ctx = await _Context.CreateDbContextAsync();
            var query = ctx.TbInstalacionSalud.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
                query = query.Where(i => i.Nombre!.Contains(filtro));

            return await query
                .OrderBy(i => i.Nombre)
                .Select(x => new ListModel {
                    Id   = x.Id,
                    Name = x.Nombre ?? ""
                })
                .Take(20)        // límite de resultados
                .ToListAsync();
        }
        catch
        {
            return new List<ListModel>();
        }
    }

}