using ExcelDataReader;
using System.Data;
using System.Globalization;
using YellowCanary.Model.Import;

namespace YellowCanary.Model
{
    public class SuperDataReader
    {
        private static string? ProjectDirectory =>
            Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        private static string DataFilePath => $"{ProjectDirectory ?? "./"}/Sample Super Data.xlsx";

        public SuperData Read()
        {
            using var stream = File.Open(DataFilePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var data = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                UseColumnDataType = true,
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true,

                }
            });

            return new SuperData(
                GetPayslips(data.Tables["Payslips"]),
                GetDisbursements(data.Tables["Disbursements"]),
                GetPaymentCodes(data.Tables["PayCodes"])
            );
        }

        private static Dictionary<string, string> GetPaymentCodes(DataTable? paymentCodes)
        {
            return paymentCodes?.Rows.Cast<DataRow>()
                            .ToDictionary(
                                row => row.Field<string>("pay_code")!,
                                row => row.Field<string>("ote_treament")!)
                   ?? new Dictionary<string, string>();
        }

        private static List<PayslipData> GetPayslips(DataTable? paySlips)
        {
            return paySlips?.Rows.Cast<DataRow>()
                    .Select(row =>
                        new PayslipData(row.Field<string>("payslip_id")!,
                            row.Field<DateTime>("end"),
                            row.Field<double>("employee_code").ToString(CultureInfo.InvariantCulture),
                            row.Field<string>("code")!,
                            row.Field<double>("amount"))).ToList()
                   ?? new List<PayslipData>();
        }

        private static List<DisbursementData> GetDisbursements(DataTable? disbursements)
        {
            return disbursements?.Rows.Cast<DataRow>()
                            .Select(row =>
                                new DisbursementData(row.Field<double>("sgc_amount"),
                                    row.Field<string>("payment_made")!,
                                    row.Field<string>("pay_period_from")!,
                                    row.Field<string>("pay_period_to")!,
                                    row.Field<double>("employee_code"))).ToList()
                   ?? new List<DisbursementData>();
        }

    }
}
