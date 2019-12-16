using System;
using Chronic;
using Chronic.Tags.Repeaters;

namespace Chronic.Tags.Repeaters
{
    public class RepeaterMonth : RepeaterUnit
    {

        private static readonly int MONTH_SECONDS = 2592000; // 30 * 24 * 60 * 60

        private DateTime? _start;


        public RepeaterMonth()
            : base(UnitName.Month)
        {

        }
        public override int GetWidth()
        {
            return RepeaterMonth.MONTH_SECONDS;
        }

        protected override Span NextSpan(Pointer.Type pointer)
        {
            var now = Now.Value;

            int direction = (pointer == Pointer.Type.Future) ? 1 : -1;
            if (_start == null)
            {
                _start = Time.New(now.Year, now.Month).AddMonths(direction * 1);
            }
            else
            {
                _start = _start.Value.AddMonths(direction * 1);
            }

            return new Span(_start.Value, _start.Value.AddMonths(1));

        }

        protected override Span CurrentSpan(Pointer.Type pointer)
        {
            DateTime monthStart;
            DateTime monthEnd;
            var now = Now.Value;

            if (pointer == Pointer.Type.Future)
            {
                monthStart = now.Date.AddDays(1);
                monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
            }
            else if (pointer == Pointer.Type.Past)
            {
                monthStart = Time.New(now.Year, now.Month);
                monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
            }
            else if (pointer == Pointer.Type.None)
            {
                monthStart = Time.New(now.Year, now.Month);
                monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
            }
            else
            {
                throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
            }
            return new Span(monthStart, monthEnd);
        }
		public static DateTime AddMonthsWithFraction(DateTime date, decimal amount)
		{
			var day_excess = amount - (int)amount;
			date = date.AddMonths((int)amount);

			date = date.AddDays((double)(Time.DaysInMonth(date.Year, date.Month) * day_excess));
			return date;
		}

        public override Span GetOffset(Span span, decimal amount, Pointer.Type pointer)
        {
            int direction = (pointer == Pointer.Type.Future) ? 1 : -1;
			amount *= direction;

			return  new Span(
				AddMonthsWithFraction(span.Start.Value,amount),
				AddMonthsWithFraction(span.End.Value, amount)
            );
        }


        public override string ToString()
        {
            return base.ToString() + "-month";
        }
    }
}