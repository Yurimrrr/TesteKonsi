namespace TesteKonsi.Domain.ValueObjects;

public class BenefitsCommand: CommandBase
{
    public BenefitsCommand(string cpf)
    {
        Cpf = cpf;
    }
    public String Cpf { get; set; }
}