namespace YellowCanary.Model.Import;

public record PayslipData(string PayslipId, DateTime End, string EmployeeCode, string Code, double Amount);