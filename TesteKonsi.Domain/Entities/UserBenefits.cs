namespace TesteKonsi.Domain.Entities;

public class UserBenefits
{
    public String Cpf { get; set; }
    public Benefits Benefits { get; set; }

    public UserBenefits(string cpf, Benefits benefits)
    {
        Cpf = cpf;
        Benefits = benefits;
    }

    public static UserBenefits Create(String cpf, Benefits benefits) 
        => new(cpf, benefits);
}