using System.Globalization;
using YellowCanary.Model;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var reader = new SuperDataReader();
var superData = reader.Read();

var employees = superData.PayslipData.GroupBy(x => x.EmployeeCode).Select(grouping =>
{
    var paySlips = grouping.GroupBy(x => x.PayslipId).Select(ps =>
    {
        return new Payslip(
            Guid.Parse(ps.Key),
            ps.First().End,
            ps.Select(e => new PayslipEntry(
                new PaymentCode(e.Code, superData.PaymentCodeData[e.Code]),
                (decimal)e.Amount)
            ).ToList());
    }).ToList();

    var disbursements = superData.DisbursementData
        .Where(x => x.EmployeeCode.ToString(CultureInfo.InvariantCulture) == grouping.Key)
        .Select(x => new Disbursement(
            (decimal)x.SgcAmount,
            DateTime.Parse(x.PaymentMade),
            DateTime.Parse(x.PayPeriodFrom),
            DateTime.Parse(x.PayPeriodTo)
            )).ToList();


    var quarterTotals = QuarterTotals.SumTotals(paySlips, disbursements);

    return new Employee(grouping.Key, paySlips, disbursements, quarterTotals);

}).ToList();

employees.ForEach(x =>
{
    Console.WriteLine($"Employee {x.Code}");
    var quarters = x.QuarterTotals.OrderBy(t => t.Year).ToList();

    // Assume super starts up to date
    decimal totalDue = 0;
    decimal totalPaid = 0;

    foreach (var qt in quarters)
    {
        var paidByDueDate = totalPaid + qt.DisbursementsByDueDate;
        Console.WriteLine($"Year: {qt.Year}, {qt.Quarter.Name} Quarter");
        Console.WriteLine($"Accrued: {qt.SuperAccrued}");
        Console.WriteLine($"Payslip: {qt.SuperPaid}");
        Console.WriteLine($"Diff: {qt.SuperAccrued - qt.SuperPaid}");
        Console.WriteLine($"Cumulative Total Due {totalDue}");
        Console.WriteLine($"Cumulative Paid By 28 day cutoff {paidByDueDate}");
        Console.WriteLine($"Cumulative Variance {paidByDueDate - totalDue}");
        totalDue += qt.SuperAccrued;
        totalPaid += qt.Disbursement;
        Console.Write(Environment.NewLine);

    }

    Console.WriteLine($"\n=================================================");

});

Console.ReadKey();