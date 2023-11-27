using YellowCanary.Model.Import;

namespace YellowCanary.Model;

public record SuperData(List<PayslipData> PayslipData, List<DisbursementData> DisbursementData,
    Dictionary<string, string> PaymentCodeData);
