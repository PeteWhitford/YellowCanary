namespace YellowCanary.Model;

public class Quarter
{
    public string Name { get; set; } = default!;
    public int StartDay { get; set; }
    public int StartMonth { get; set; }
    public int EndDay { get; set; }
    public int EndMonth { get; set; }
    public bool InRange(DateTime date) => date.Month >= StartMonth && date.Month <= EndMonth && date.Day > StartDay && date.Day <= EndDay;
    public bool InFirst28Days(DateTime date) => date.Month == StartMonth && date.Day <= StartDay + 28;
}