using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language
{
    public class TypeCast
    {
        public string From { get; set; }
        public string To { get; set; }

        public delegate Variable CastEvalualtor(Variable variable);
        public event CastEvalualtor CastVariable;
        public event CastEvalualtor ReverseCastvariable;

        public TypeCast(string from, string to)
        {
            From = from;
            To = to;
        }

        public Variable Cast(Variable variable)
        {
            if (!variable.Type.Name.Equals(From))
            {
                throw new ArgumentException("The variable specified is not supported by the cast requested. Please resolve this problem.");
            }
            return CastVariable(variable);
        }

        public Variable ReverseCast(Variable variable)
        {
            if (!variable.Type.Name.Equals(To))
            {
                throw new ArgumentException("The variable specified is not supported by the cast requested. Please resolve this problem.");
            }
            return ReverseCastvariable(variable);
        }
    }
}
