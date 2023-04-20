namespace WalletApp_Backend.Common.Extensions
{
    public static class DateExtensions
    {
        public static List<int> CountDayFromTodaySeasons(this DateTime date)
        {
            var monthOfStartSeasons = new[] { 0, 3, 6, 9 };
            var todaySeasons = monthOfStartSeasons.LastOrDefault(x => date.Month >= x);

            var firstDayOfCurrentSeason = todaySeasons == 0 ? new DateTime(date.Year - 1, 12, 1) : new DateTime(date.Year, todaySeasons, 1);

            var counts = new List<int>();
            while (true)
            {

                if (firstDayOfCurrentSeason > date ||firstDayOfCurrentSeason.Month==date.Month)
                {
                var differenceDate = date.Subtract(firstDayOfCurrentSeason);
                    counts.Add(differenceDate.Days);
                    break;
                }

                counts.Add(DateTime.DaysInMonth(firstDayOfCurrentSeason.Year,firstDayOfCurrentSeason.Month));
                firstDayOfCurrentSeason = firstDayOfCurrentSeason.AddMonths(1);
            }

            return counts;
        }
    }
}