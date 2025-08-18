namespace Sisdas.Models.Otros;

public class ActiveDirectoryUserModel
{
    public string email { get; set; } = "";
    public string firstName { get; set; } = "";
    public string lastName { get; set; } = "";
    public string middleName { get; set; } = "";
    public bool enabled { get; set; } = false;
    public DateTime? lastLoginDate { get; set; }
}