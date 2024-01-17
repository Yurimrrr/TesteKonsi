using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TesteKonsi.Domain.ValueObjects;

public class AuthCommand: CommandBase
{
    public AuthCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
    [JsonProperty("username")]
    public String Username { get; set; }
    [JsonProperty("password")]
    public String Password { get; set; }
}