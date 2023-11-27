namespace YellowCanary.Model;

public class Payslip
{
    public Payslip(Guid id, DateTime end, List<PayslipEntry> entries)
    {
        Id = id;
        End = end;
        Entries = entries;
    }

    public Guid Id { get; init; }
    public DateTime End { get; init; }

    public List<PayslipEntry> Entries { get; }
    public decimal OteAmount => Entries.Where(x => x.Code.IsOte).Sum(x => x.Amount);
    public decimal NonOteAmount => Entries.Where(x => !x.Code.IsOte).Sum(x => x.Amount);
    public decimal SuperPaidAmount => Entries.Where(x => x.Code.IsSuper).Sum(x => x.Amount);
    public decimal AccruedSuper => Math.Round(Convert.ToDecimal((double)OteAmount * 0.095), 2, MidpointRounding.AwayFromZero);

}