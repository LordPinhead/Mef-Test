using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorAPI;

namespace Multiplicator
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '*')]
    public class Multiplicator : IOperation
    {
        public int Operate(int left, int right)
        {
            return left * right;
        }
    }
}
