namespace YellowCanary.Model;

public class QuarterTotals
{
    public int Year { get; set; }
    public Quarter Quarter { get; set; } = default!;
    public decimal Ote { get; set; }
    public decimal NonOte { get; set; }
    public decimal SuperPaid { get; set; }
    public decimal Disbursement { get; set; }
    public decimal SuperAccrued { get; set; }
    public decimal DisbursementsByDueDate { get; set; }

    public static List<QuarterTotals> SumTotals(List<Payslip> payslips, List<Disbursement> disbursements)
    {
        var payslipYears = payslips.Select(x => x.End.Year);
        var disbursementYears = disbursements.Select(x => x.PaymentMade.Year);
        var years = payslipYears.Concat(disbursementYears).Distinct();

        return years.SelectMany(y =>
        {
            return YearlyQuarters.All.Select(quarter =>
            {
                var entries = payslips.Where(x => x.End.Year == y && quarter.InRange(x.End)).SelectMany(x => x.Entries).ToList();

                return new QuarterTotals
                {
                    Year = y,
                    Quarter = quarter,
                    Disbursement = disbursements.Where(x => x.PaymentMade.Year == y && quarter.InRange(x.PaymentMade)).Sum(x => x.ScgAmount),
                    Ote = entries.Where(x => x.Code.IsOte).Sum(x => x.Amount),
                    NonOte = entries.Where(x => !x.Code.IsOte).Sum(x => x.Amount),
                    SuperAccrued = payslips.Where(x => x.End.Year == y && quarter.InRange(x.End)).Sum(x => x.AccruedSuper),
                    SuperPaid = entries.Where(x => x.Code.IsSuper).Sum(x => x.Amount),
                    DisbursementsByDueDate = disbursements.Where(x => x.PaymentMade.Year == y && quarter.InFirst28Days(x.PaymentMade)).Sum(x => x.ScgAmount)
                };
            });
        }).ToList();
    }

}
