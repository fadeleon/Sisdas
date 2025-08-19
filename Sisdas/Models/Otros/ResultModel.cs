namespace Sisdas.Models.Otros;

public class ResultModel
{
    public bool Resultado { get; set; }
    public string Mensaje { get; set; } = "";
    public List<string> Errores { get; set; } = new();
}