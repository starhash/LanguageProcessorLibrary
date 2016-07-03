using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Types
{
    public class Bool : DataType
    {
        public Bool()
        {
            Name = "Boolean";
            Type = DataTypeSpecifer.Linear;
        }

        public override Variable Parse(string s)
        {
            return new Variable(this, bool.Parse(s));
        }
    }
}
