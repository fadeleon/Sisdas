using Microsoft.AspNetCore.Identity;

namespace Sisdas.Data;

public class ApplicationUser : IdentityUser
{
    public string Nombres { get; set; } = "";
    public string Apellidos { get; set; } = "";
    public string NumeroDocumentoIdentidad { get; set; } = "";
    public string Genero { get; set; } = "";
    public string TelefonoLaboral { get; set; } = "";
    public int? idInstalacionSalud { get; set; }
    public int idRegionSalud { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool IsFromActiveDirectory { get; set; } = true;
    public bool IsActivo { get; set; } = true;
}