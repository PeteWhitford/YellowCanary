using System.Globalization;
using YellowCanary.Model.Import;

namespace YellowCanary.Model;

public class SuperData
{
    public SuperData(List<PayslipData> payslipData, List<DisbursementData> disbursementData,
        Dictionary<string, string> paymentCodeData)
    {
        PayslipData = payslipData;
        DisbursementData = disbursementData;
        PaymentCodeData = paymentCodeData;
    }

    public List<PayslipData> PayslipData { get; init; }
    public List<DisbursementData> DisbursementData { get; init; }
    public Dictionary<string, string> PaymentCodeData { get; init; }

    public List<Employee> GetEmployees()
    {
        return PayslipData.GroupBy(x => x.EmployeeCode).Select(grouping =>
        {
            var paySlips = GetPayslips(grouping);
            var disbursements = GetDisbursements(grouping.Key);
            var quarterTotals = QuarterTotals.SumTotals(paySlips, disbursements);

            return new Employee(grouping.Key, paySlips, disbursements, quarterTotals);

        }).ToList();
    }

    private List<Disbursement> GetDisbursements(string employeeCode)
    {
        return DisbursementData
            .Where(x => x.EmployeeCode.ToString(CultureInfo.InvariantCulture) == employeeCode)
            .Select(x => new Disbursement(
                (decimal)x.SgcAmount,
                DateTime.Parse(x.PaymentMade),
                DateTime.Parse(x.PayPeriodFrom),
                DateTime.Parse(x.PayPeriodTo)
            )).ToList();
    }

    private List<Payslip> GetPayslips(IEnumerable<PayslipData> payslipsData)
    {
        return payslipsData.GroupBy(x => x.PayslipId).Select(ps =>
        {
            return new Payslip(
                Guid.Parse(ps.Key),
                ps.First().End,
                ps.Select(e => new PayslipEntry(
                    new PaymentCode(e.Code, PaymentCodeData[e.Code]),
                    (decimal)e.Amount)
                ).ToList());
        }).ToList();
    }
}
