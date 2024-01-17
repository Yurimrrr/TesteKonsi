namespace TesteKonsi.Domain.ValueObjects.Response.Auth;

public class AuthResponse
{
    public Boolean Success { get; set; }
    public Data Data { get; set; }

    public AuthResponse(bool success, Data data)
    {
        Success = success;
        Data = data;
    }
}
public class Data
{
    public String Token { get; set; }
    public String Type { get; set; }
    public DateTime ExpiresIn { get; set; }
}