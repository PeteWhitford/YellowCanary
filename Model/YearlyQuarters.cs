namespace YellowCanary.Model;

public static class YearlyQuarters
{
    public static Quarter First => new()
    {
        Name = "First",
        StartMonth = 1,
        StartDay = 1,
        EndMonth = 3,
        EndDay = 31
    };
    public static Quarter Second => new()
    {
        Name = "Second",
        StartMonth = 4,
        StartDay = 1,
        EndMonth = 6,
        EndDay = 30
    };
    public static Quarter Third => new()
    {
        Name = "Third",
        StartMonth = 7,
        StartDay = 1,
        EndMonth = 9,
        EndDay = 30
    };
    public static Quarter Fourth => new()
    {
        Name = "Fourth",
        StartMonth = 10,
        StartDay = 1,
        EndMonth = 12,
        EndDay = 31
    };

    public static Quarter Get(DateTime date) => All.Single(x => x.InRange(date));

    public static List<Quarter> All => new() { First, Second, Third, Fourth };

}