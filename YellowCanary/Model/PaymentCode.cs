namespace YellowCanary.Model;

public class PaymentCode
{
    public PaymentCode(string code, string oteTreatment)
    {
        Code = code;
        OteTreatment = oteTreatment;
    }

    public string Code { get; init; }
    public string OteTreatment { get; init; }
    public bool IsOte => OteTreatment == "OTE";
    public bool IsSuper => Code == "P001 - Co. Super 9.5%";
}