namespace YellowCanary.Model;

public record Employee(string Code, List<Payslip> Payslips, List<Disbursement> Disbursements,
    List<QuarterTotals> QuarterTotals);
