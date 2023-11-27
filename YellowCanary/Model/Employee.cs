namespace YellowCanary.Model;

public class Employee
{
    public Employee(string code, List<Payslip> payslips, List<Disbursement> disbursements,
        List<QuarterTotals> quarterTotals)
    {
        Code = code;
        Payslips = payslips;
        Disbursements = disbursements;
        QuarterTotals = quarterTotals;
    }

    public string Code { get; init; }
    public List<Payslip> Payslips { get; init; }
    public List<Disbursement> Disbursements { get; init; }
    public List<QuarterTotals> QuarterTotals { get; init; }

    public void PrintReport()
    {
        Console.WriteLine($"Employee {Code}");
        var quarters = QuarterTotals.OrderBy(t => t.Year).ToList();

        // Assume super starts up to date
        decimal totalDue = 0;
        decimal totalPaid = 0;

        foreach (var qt in quarters)
        {
            var paidByDueDate = totalPaid + qt.SuperPaidByDueDate;
            Console.WriteLine($"Year: {qt.Year}, {qt.Quarter.Name} Quarter");
            Console.WriteLine($"Accrued: {qt.SuperAccrued}");
            Console.WriteLine($"Payslip: {qt.SuperPaid}");
            Console.WriteLine($"Diff: {qt.SuperAccrued - qt.SuperPaid}");
            Console.WriteLine($"Cumulative Total Due {totalDue}");
            Console.WriteLine($"Cumulative Paid By 28 day cutoff {paidByDueDate}");
            Console.WriteLine($"Cumulative Variance {paidByDueDate - totalDue}");
            totalDue += qt.SuperAccrued;
            totalPaid += qt.DisbursementTotal;
            Console.Write(Environment.NewLine);

        }

        Console.WriteLine("\n=================================================");
    }
}
