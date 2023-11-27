namespace YellowCanary.Model;

public record Disbursement(decimal ScgAmount, DateTime PaymentMade, DateTime PayPeriodFrom, DateTime PayPeriodTo);