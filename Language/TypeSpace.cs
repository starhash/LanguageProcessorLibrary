using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language.Processing;

namespace Language
{
    public class TypeSpace : Processor
    {
        public void AddType(DataType d)
        {
            TypeLister.AddType(d);
        }

        public void AddOperator(Operator op, DataType d)
        {
            TypeLister.AddOperator(op, d);
        }
    }
}
