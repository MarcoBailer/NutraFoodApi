namespace Nutra.Models.Dtos.Registro;

public class RegisterModelDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
