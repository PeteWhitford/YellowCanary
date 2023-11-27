namespace YellowCanary.Model;

public class Disbursement
{
    public Disbursement(decimal scgAmount, DateTime paymentMade, DateTime payPeriodFrom, DateTime payPeriodTo)
    {
        ScgAmount = scgAmount;
        PaymentMade = paymentMade;
        PayPeriodFrom = payPeriodFrom;
        PayPeriodTo = payPeriodTo;
    }

    public decimal ScgAmount { get; }
    public DateTime PaymentMade { get; }
    public DateTime PayPeriodFrom { get; }
    public DateTime PayPeriodTo { get; }
}