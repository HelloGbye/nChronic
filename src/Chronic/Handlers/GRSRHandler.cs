using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronic.Handlers
{
    public class GRSRHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
            // last week of may
            int offset = 0;
            if (tokens.Count < 4) offset = 1;
            var outerSpan = new List<Token> { tokens[3 - offset] }.GetAnchor(options);
            var result = Utils.HandleGRR(tokens.Take(2).ToList(), outerSpan);
            if (result != null)
                Utils.RestrictSpan(result, outerSpan);
            return result;
        }
    }
}
