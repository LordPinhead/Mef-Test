using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorAPI;

namespace NegativeOperator
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    class Subtract : IOperation
    {
        public Subtract()
        {

        }
        public int Operate(int left, int right)
        {
            return left - right;
        }
    }

}
