using YellowCanary.Model;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

try
{
    var reader = new SuperDataReader();
    var superData = reader.Read();

    var employees = superData.GetEmployees();

    employees.ForEach(x => x.PrintReport());
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}

Console.ReadKey();