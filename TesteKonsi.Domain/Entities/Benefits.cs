namespace TesteKonsi.Domain.Entities;

public class Benefits
{
    public String BenefitNumer { get; set; }
    public String BenefitTypeCode { get; set; }

    public Benefits(string benefitNumer, string benefitTypeCode)
    {
        BenefitNumer = benefitNumer;
        BenefitTypeCode = benefitTypeCode;
    }

    public static Benefits Create(String benefitNumber, String benefitTypeCode) 
        => new(benefitNumber, benefitTypeCode);
}