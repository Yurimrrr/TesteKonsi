namespace TesteKonsi.Domain.Entities;

public class UserBenefits
{
    public String Cpf { get; set; }
    public IEnumerable<Benefits> Benefits { get; set; }

    public UserBenefits(string cpf, IEnumerable<Benefits>  benefits)
    {
        Cpf = cpf;
        Benefits = benefits;
    }

    public static UserBenefits Create(String cpf, IEnumerable<Benefits> benefits) 
        => new(cpf, benefits);
}