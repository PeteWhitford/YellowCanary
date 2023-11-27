using YellowCanary.Model;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

try
{
    var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
    var dataFilePath = $"{projectDirectory ?? "./"}/Sample Super Data.xlsx"; var reader = new SuperDataReader();
    var superData = reader.Read(dataFilePath);

    var employees = superData.GetEmployees();

    employees.ForEach(x => x.PrintReport());
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}

Console.ReadKey();