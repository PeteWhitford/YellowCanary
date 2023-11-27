using YellowCanary.Model;

namespace YellowCanary.Tests
{
    public class QuarterTotalsTests
    {
        public static TheoryData<DateTime, string> DateData = new()
        {
            { new DateTime(2023, 1, 1), "First" },
            { new DateTime(2023, 2, 15), "First" },
            { new DateTime(2023, 3, 31), "First" },
            { new DateTime(2023, 4, 1), "Second" },
            { new DateTime(2023, 5, 15), "Second" },
            { new DateTime(2023, 6, 30), "Second" },
            { new DateTime(2023, 7, 1), "Third" },
            { new DateTime(2023, 8, 15), "Third" },
            { new DateTime(2023, 9, 30), "Third" },
            { new DateTime(2023, 10, 1), "Fourth" },
            { new DateTime(2023, 11, 15), "Fourth" },
            { new DateTime(2023, 12, 31), "Fourth" },
        };

        [Theory, MemberData(nameof(DateData))]
        public void dates_are_put_in_correct_quarters(DateTime date, string quarterName)
        {
            var quarters = YearlyQuarters.All;

            var found = false;
            foreach (var quarter in quarters)
            {
                if (!quarter.InRange(date)) continue;
                Assert.Equal(quarterName, quarter.Name);
                found = true;
            }

            Assert.True(found);
        }
    }
}
