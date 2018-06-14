using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorAPI;

namespace Modulator
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '%')]
    public class Mod : IOperation
    {
        public Mod ()
        {

        }
        public int Operate(int left, int right)
        {
            return left/right;
        }
    }

}
