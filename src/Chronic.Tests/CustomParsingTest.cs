using System;
using Xunit;

namespace Chronic.Tests
{
    public class CustomParsingTest : ParsingTestsBase
    {

        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void may_28_at_5_32_19pm()
        {
            Parse("may 28 at 5:32:19pm", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 17, 32, 19));
        }

        [Fact]
        public void _7_days_from_now()
        {
            Parse(" 7 days from now")
                .AssertEquals(Time.New(2006, 8, 23, 14));
        }

		[Fact]
		public void _in_7_days_from_now()
		{
			Parse("in 7 days from now")
				.AssertEquals(Time.New(2006, 8, 23, 14));
		}

		[Fact]
		public void _in_7_hours_from_now() {
			Parse("in 7 hours from now")
				.AssertEquals(Now().AddHours(7));
		}
		[Fact]
		public void _in_7_pt_5_hours_from_now() {
			Parse("in 7.5 hours from now")
				.AssertEquals(Now().AddHours(7.5));
		}
		[Fact]
        public void _in_7_days_from_dotdotdot()
        {
            Parse("in 7 days from").AssertEquals(Time.New(2006, 8, 23, 14));
        }

        [Fact]
        public void _7_days_from_now_at_midnight()
        {
            Parse(" 7 days from now at midnight")
                .AssertEquals(Time.New(2006, 8, 24));
        }

        [Fact]
        public void seven_days_from_now_at_midnight()
        {
            Parse(" seven days from now at midnight")
                .AssertEquals(Time.New(2006, 8, 24));
        }

        [Fact]
        public void _2_weeks_ago()
        {
            Parse("2 weeks ago")
                .AssertEquals(Time.New(2006, 8, 02, 14));
        }

        [Fact]
        public void two_weeks_ago()
        {
            Parse("two weeks ago")
                .AssertEquals(Time.New(2006, 8, 02, 14));
        }


        public class WeekOrDayXinMonth : ParsingTestsBase
        {
            protected override DateTime Now()
            {
                return Time.New(2017, 05, 03, 14, 34, 13);
            }

            [Fact]
            public void first_week_january_is_parsed_correctly()
            {
                Parse("first week january").AssertStartsAt(Time.New(2018, 1, 1));
                Parse("first week january")  .AssertEndsAt(Time.New(2018, 1, 7));
            }
            [Fact]
            public void secondWord_week_january_is_parsed_correctly()
            {
                Parse("second week january").AssertStartsAt(Time.New(2018, 1, 7));
                Parse("second week january")  .AssertEndsAt(Time.New(2018, 1, 14));
                Parse("2nd week january").AssertStartsAt(Time.New(2018, 1, 7));
                Parse("2nd week january")  .AssertEndsAt(Time.New(2018, 1, 14));
            }
            [Fact]
            public void week3_january_is_parsed_correctly()
            {
                Parse("third week january").AssertStartsAt(Time.New(2018, 1, 14));
                Parse("third week january")  .AssertEndsAt(Time.New(2018, 1, 21));
            }
            [Fact]
            public void week4_january_is_parsed_correctly()
            {
                Parse("fourth week january").AssertStartsAt(Time.New(2018, 1, 21));
                Parse("fourth week january")  .AssertEndsAt(Time.New(2018, 1, 28));
            }
            [Fact]
            public void lastWeek_january_is_parsed_correctly()
            {
                Parse("last week january").AssertStartsAt(Time.New(2018, 1, 28));
                Parse("last week january")  .AssertEndsAt(Time.New(2018, 2, 1));
            }
            [Fact]
            public void lastWeek_inJanuary_is_parsed_correctly()
            {
                Parse("last week in january").AssertStartsAt(Time.New(2018, 1, 28));
                Parse("last week in january")  .AssertEndsAt(Time.New(2018, 2, 1));
            }

            [Fact]
            public void first_wednesday_in_october()
            {
                Parse("first wednesday in october").AssertEquals(Time.New(2017, 10, 4, 12));
            }
            [Fact]
            public void last_wednesday_in_october()
            {
                Parse("last wednesday in october").AssertEquals(Time.New(2017, 10, 25, 12));
            }
        }

        public class OverflowTests : ParsingTestsBase
        {
            protected override DateTime Now()
            {
                return Time.New(2016, 3, 9, 14, 34, 13);
            }

            [Fact]
            public void day4_last_week_shouldBe_from_sunday()
            {
                Parse("4th day last week").AssertEquals(Time.New(2016, 3, 2, 12));
            }
        }

        public class CanExtractTimeSpanFromSpan : ParsingTestsBase
        {
            protected override DateTime Now()
            {
                return Time.New(2006, 8, 16, 14, 34, 13);
            }

            [Fact]
            public void may_28_at_5_32_19pm()
            {
                Parse("7 days and two hours ago", new { Context = Pointer.Type.Past })
                    .AssertEquals(Time.New(2006, 8, 09, 12, 34, 13));
            }

            [Fact]
            public void friday_9_oct()
            {
                Parse("friday 9 oct")
                    .AssertEquals(Time.New(2006, 10, 09, 12));
            }

            [Fact]
            public void second_week_in_january_is_parsed_correctly()
            {
                Parse("2nd week in january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
            [Fact]
            public void second_week_january_is_parsed_correctly()
            {
                Parse("2nd week january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
            [Fact]
            public void second_week_of_january_is_parsed_correctly()
            {
                Parse("2nd week of january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
            [Fact]
            public void secondWord_week_january_is_parsed_correctly()
            {
                Parse("second week january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
            [Fact]
            public void secondWord_week_of_january_is_parsed_correctly()
            {
                Parse("second week of january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
            [Fact]
            public void secondWord_week_in_january_is_parsed_correctly()
            {
                Parse("second week in january").AssertEquals(Time.New(2007, 1, 10, 12));
            }

            [Fact]
            public void third_week_of_month_test_num()
            {
                Parse("3rd week in january").AssertEquals(Time.New(2007, 1, 17, 12));
            }
            [Fact]
            public void third_week_of_month_test()
            {
                Parse("third week in january").AssertEquals(Time.New(2007, 1, 17, 12));
            }
            [Fact]
            public void third_week_of_month_test2()
            {
                Parse("third week january").AssertEquals(Time.New(2007, 1, 17, 12));
            }
            [Fact]
            public void third_week_of_month_test3()
            {
                Parse("third week of january").AssertEquals(Time.New(2007, 1, 17, 12));
            }

            [Fact]
            public void fourth_week_of_month_test_num()
            {
                Parse("4th week in january").AssertEquals(Time.New(2007, 1, 24, 12));
            }
            [Fact]
            public void fourth_week_of_month_test()
            {
                Parse("fourth week in january").AssertEquals(Time.New(2007, 1, 24, 12));
            }
            [Fact]
            public void fourth_week_of_month_test2()
            {
                Parse("fourth week january").AssertEquals(Time.New(2007, 1, 24, 12));
            }
            [Fact]
            public void fourth_week_of_month_test3()
            {
                Parse("fourth week of january").AssertEquals(Time.New(2007, 1, 24, 12));
            }

            [Fact]
            public void last_week_of_month_test()
            {
                Parse("last week in january").AssertStartsAt(Time.New(2007, 1, 28));
                Parse("last week in january").  AssertEndsAt(Time.New(2007, 2, 1));
            }
            [Fact]
            public void last_week_of_month_test2()
            {
                Parse("last week january").AssertStartsAt(Time.New(2007, 1, 28));
                Parse("last week january").AssertEndsAt(Time.New(2007, 2, 1));
            }
            [Fact]
            public void last_week_of_month_test3()
            {
                Parse("last week of january").AssertStartsAt(Time.New(2007, 1, 28));
                Parse("last week of january").AssertEndsAt(Time.New(2007, 2, 1));
            }
        }

        public class StrangeTimesTest : ParsingTestsBase
        {
            protected override DateTime Now()
            {
                return Time.New(2017, 1, 1, 0, 0, 0);
            }

            [Fact]
            public void Feb30()
            {
                Parse("Feb 30").AssertStartsAt(Time.New(2017, 2, 30));
            }
            [Fact]
            public void April31()
            {
                Parse("April 31").AssertStartsAt(Time.New(2017, 4, 31));
            }
        }
    }
}
