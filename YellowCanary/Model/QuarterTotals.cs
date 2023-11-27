namespace YellowCanary.Model;

public class QuarterTotals
{
    public int Year { get; set; }
    public Quarter Quarter { get; set; } = default!;
    public decimal Ote { get; set; }
    public decimal NonOte { get; set; }
    public decimal SuperPaid { get; set; }
    public decimal DisbursementTotal { get; set; }
    public decimal SuperAccrued { get; set; }
    public decimal SuperPaidByDueDate { get; set; }

    public static List<QuarterTotals> SumTotals(List<Payslip> payslips, List<Disbursement> disbursements)
    {
        var payslipYears = payslips.Select(x => x.End.Year);
        var disbursementYears = disbursements.Select(x => x.PaymentMade.Year);
        var years = payslipYears.Concat(disbursementYears).Distinct();

        return years.SelectMany(y =>
            YearlyQuarters.All.Select(quarter =>
            {
                var quarterPayslips = payslips.Where(x => x.End.Year == y && quarter.InRange(x.End)).ToList();

                return new QuarterTotals
                {
                    Year = y,
                    Quarter = quarter,
                    DisbursementTotal = disbursements.Where(x => x.PaymentMade.Year == y && quarter.InRange(x.PaymentMade)).Sum(x => x.ScgAmount),
                    Ote = quarterPayslips.Sum(x => x.OteAmount),
                    NonOte = quarterPayslips.Sum(x => x.NonOteAmount),
                    SuperAccrued = quarterPayslips.Sum(x => x.AccruedSuper),
                    SuperPaid = quarterPayslips.Sum(x => x.SuperPaidAmount),
                    SuperPaidByDueDate = disbursements.Where(x => x.PaymentMade.Year == y && quarter.InFirst28Days(x.PaymentMade)).Sum(x => x.ScgAmount)
                };
            })).ToList();
    }

}
