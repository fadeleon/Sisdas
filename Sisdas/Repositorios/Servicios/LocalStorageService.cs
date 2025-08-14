using Microsoft.JSInterop;
using Sisdas.Repositorios.Interfaces;

namespace Sisdas.Repositorios.Servicios;

public class LocalStorageService : ILocalStorage
{
    private readonly IJSRuntime _IJSRuntime;

    public LocalStorageService(IJSRuntime IJSRuntime)
    {
        _IJSRuntime = IJSRuntime;
    }
    public async Task AddItem(string Key, string Value)
    {
        await _IJSRuntime.InvokeVoidAsync("localStorage.setItem", Key, Value);
    }

    public async Task RemoveItem(string Key)
    {
        await _IJSRuntime.InvokeVoidAsync("localStorage.removeItem", Key);
    }

    public async Task<string> GetItem(string Key)
    {
        return await _IJSRuntime.InvokeAsync<string>("localStorage.getItem", Key);
    }
}