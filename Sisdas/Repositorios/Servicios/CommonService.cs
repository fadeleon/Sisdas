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
    
    public async Task<List<CatRegionSalud>> GetJustRegiones()
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
            await using var ctx = await _ContextCommon.CreateDbContextAsync();
            var query = ctx.CatUnidadNotificadora.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtroInst))
                query = query.Where(i => i.NombreUn!.Contains(filtroInst));

            return await query
                .OrderBy(i => i.NombreUn)
                .Select(x => new ListModel {
                    Id   = x.IdUn,
                    Name = x.NombreUn ?? ""
                })
                .Take(20)
                .ToListAsync();
        }
        catch
        {
            return new List<ListModel>();
        }
    }
    
    public async Task<ListModel?> GetInstalacionById(int id)
    {
        try
        {
            await using var ctx = await _ContextCommon.CreateDbContextAsync();
            var entity = await ctx.CatUnidadNotificadora
                .Where(x => x.IdUn == id)
                .Select(x => new ListModel
                {
                    Id = x.IdUn,
                    Name = x.NombreUn ?? ""
                })
                .FirstOrDefaultAsync();

            return entity;
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<List<ListModel>> GetProvincias()
    {
        List<ListModel> Lista = new List<ListModel>();
        try
        {
            using (var localContext = await _ContextCommon.CreateDbContextAsync())
            {
                Lista = await localContext.CatProvincia
                    .Select(x => new ListModel()
                    {
                        Id = x.IdProvincia,
                        Name = x.NombreProvincia ?? "",
                    }).ToListAsync();
            }
        }
        catch (Exception)
        {
        }
        return Lista;
    }
    
    public async Task<List<ListModel>> GetRegiones(int ProvinciaId)
    {
        List<ListModel> Lista = new List<ListModel>();
        try
        {
            using (var localContext = await _ContextCommon.CreateDbContextAsync())
            {
                Lista = await localContext.CatRegionSalud.Where(x => x.IdProvincia == ProvinciaId)
                    .Select(x => new ListModel()
                    {
                        Id = x.IdRegion,
                        Name = x.NombreRegion ?? "",
                    }).ToListAsync();

                Lista = Lista.OrderBy(x => x.Name).ToList();
            }
        }
        catch (Exception)
        {
        }
        return Lista;
    }
    
    public async Task<List<ListModel>> GetDistritos(int RegionId)
    {
        List<ListModel> Lista = new List<ListModel>();
        try
        {
            using (var localContext = await _ContextCommon.CreateDbContextAsync())
            {
                Lista = await localContext.CatDistrito.Where(x => x.IdRegion == RegionId)
                    .Select(x => new ListModel()
                    {
                        Id = x.IdDistrito,
                        Name = x.NombreDistrito ?? "",
                    }).ToListAsync();

                Lista = Lista.OrderBy(x => x.Name).ToList();
            }
        }
        catch (Exception)
        {
        }
        return Lista;
    }

    public async Task<List<ListModel>> GetCorregimientos(int DistritoId)
    {
        List<ListModel> Lista = new List<ListModel>();
        try
        {
            using (var localContext = await _ContextCommon.CreateDbContextAsync())
            {
                Lista = await localContext.CatCorregimiento.Where(x => x.IdDistrito == DistritoId)
                    .Select(x => new ListModel()
                    {
                        Id = x.IdCorregimiento,
                        Name = x.NombreCorregimiento ?? "",
                    }).ToListAsync();

                Lista = Lista.OrderBy(x => x.Name).ToList();
            }
        }
        catch (Exception)
        {
        }
        return Lista;
    }

}