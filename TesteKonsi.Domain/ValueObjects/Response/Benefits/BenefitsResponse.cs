namespace TesteKonsi.Domain.ValueObjects.Response.Benefits;

public class BenefitsResponse
{
    public Boolean Success { get; set; }
    public Data Data { get; set; }

    public BenefitsResponse(bool success, Data data)
    {
        Success = success;
        Data = data;
    }
}
public class Data
{
    public String Cpf { get; set; }
    public IEnumerable<BeneficiosValueObject> Beneficios { get; set; }
}

public class BeneficiosValueObject
{
    public String Numero_beneficio { get; set; }
    public String Codigo_tipo_beneficio { get; set; }
}

