using System.Collections.Generic;

namespace Chronic
{
    public class Scalar : Tag<decimal>
    {
        public Scalar(decimal value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return "scalar";
        }
    }

    public class ScalarDay : Scalar
    {
        public ScalarDay(decimal value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "-day-" + Value;
        }
    }

    public class ScalarMonth : Scalar
    {
        public ScalarMonth(decimal value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "-month-" + Value;
        }
    }

    public class ScalarYear : Scalar
    {
        public ScalarYear(decimal value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "-year-" + Value;
        }
    }
}