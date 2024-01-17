namespace TesteKonsi.Domain.ValueObjects;

public class AuthCommand: CommandBase
{
    public AuthCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public String Username { get; set; }
    public String Password { get; set; }
}