using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    #region Klassen

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '+')]
    class Add : IOperation
    {
        public Add()
        {

        }
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }


    [Export(typeof(ICalculator))]
    class MySimpleCalculator : MarshalByRefObject, ICalculator
    {
        /*public MySimpleCalculator(IEnumerable<Lazy<IOperation, IOperationData>> operations)
        {
            this.operations = operations;
        }*/

        [ImportMany]
        IEnumerable<Lazy<IOperation, IOperationData>> operations;

        private int FindFirstNonDigit(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }


        public string Calculate(string input)
        {
            int left;
            int right;
            Char operation;
            int fn = FindFirstNonDigit(input); //finds the operator
            if (fn < 0) return "Could not parse command.";

            try
            {
                //separate out the operands
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch
            {
                return "Could not parse command.";
            }

            operation = input[fn];

            foreach (Lazy<IOperation, IOperationData> i in operations)
            {
                if (i.Metadata.Symbol.Equals(operation)) return i.Value.Operate(left, right).ToString();
            }
            return "Operation Not Found!";

        }
    }
    #endregion
}
