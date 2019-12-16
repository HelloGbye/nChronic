using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class BasicExpressionsTests : ParsingTestsBase
    {
        private DateTime _now = DateTime.Now;
        protected override DateTime Now()
        {
            return _now;
        }
        
        [Fact]
        public void dayname_week()
        {
            var xx = Parse(Now().DayOfWeek.ToString());
                xx.AssertStartsAt(Now());
        }
        [Fact]
        public void next_dayname_week()
        {
            Parse("next " + Now().DayOfWeek.ToString()).AssertStartsAt(Now().Date.AddDays(7));
        }

        [Fact]
        public void today_is_parsed_correctly()
        {
            Parse("today").AssertStartsAt(DateTime.Now.Date);
        }

        [Fact]
        public void uppercase_today_is_parsed_correctly()
        {
            Parse("TODAY").AssertStartsAt(DateTime.Now.Date);
        }

        [Fact]
        public void first_letter_uppercase_today_is_parsed_correctly()
        {
            Parse("Today").AssertStartsAt(DateTime.Now.Date);
        }

        [Fact]
        public void yesterday_is_parsed_correctly()
        {
            Parse("yesterday").AssertStartsAt(DateTime.Now.Date.AddDays(-1));
        }

        [Fact]
        public void tomorrow_is_parsed_correctly()
        {
            Parse("tomorrow").AssertStartsAt(DateTime.Now.Date.AddDays(1));
        }

        [Fact]
        public void day_after_tomorrow_is_parsed_correctly()
        {
            Parse("Day after tomorrow").AssertStartsAt(DateTime.Now.Date.AddDays(2));
        }
        [Fact]
        public void next_week_is_parsed_correctly()
        {
            Parse("next week").AssertStartsAt(NextWeek(DateTime.Today));
        }
        [Fact]
        public void week_after_next_week_is_parsed_correctly()
        {
			Parse("week after next week").AssertStartsAt(NextWeek(DateTime.Today).AddDays(7));
        }
        [Fact]
        public void week_after_next_is_parsed_correctly()
        {
			Parse("week after next").AssertStartsAt(NextWeek(DateTime.Today).AddDays(7));
        }
        [Fact]
        public void week_after_next_day_is_parsed_correctly()
        {
            Parse("week after next day").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DateTime.Today.DayOfWeek).AddDays(1).AddDays(7));
        }

        [Fact]
        public void week_after_friday_is_parsed_correctly()
        {
            Parse("week after friday").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Friday).AddDays( 7));
        }

        [Fact]
        public void week_after_next_friday_is_parsed_correctly()
        {
            Parse("week after next friday").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Friday).AddDays(2*7));
        }
		public static DateTime NextWeek(DateTime start) {
			return GetFirstDateOfWeekday(start.DayOfWeek == DayOfWeek.Sunday ? start.AddDays(1) : start, DayOfWeek.Sunday);
		}
        public static DateTime GetFirstDateOfWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;

			return start.AddDays(daysToAdd);
        }

		[Fact]
		public void day_after_date_is_parsed_correctly()
		{
			Parse("1 day after 11/15/2015").AssertStartsAt(new DateTime(2015, 11, 16));
		}

		[Fact]
		public void next_friday_or_weekend_is_parsed_correctly()
		{
			Parse("next friday").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Friday).AddDays(7));
			Parse("next week friday").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Friday).AddDays(7));
			Parse("next weekend").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Saturday).AddDays(7));
			Parse("next week weekend").AssertStartsAt(GetFirstDateOfWeekday(DateTime.Today, DayOfWeek.Saturday).AddDays(7));
		}
    }
}