using YellowCanary.Model;

namespace YellowCanary.Tests;

public class ParsingTests
{
    [Fact]
    public void missing_columns_throw_exception()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        var dataFilePath = $"{projectDirectory ?? "./"}/Super Data Missing Columns.xlsx";

        var reader = new SuperDataReader();
        Assert.Throws<Exception>(() => reader.Read(dataFilePath));
    }
}