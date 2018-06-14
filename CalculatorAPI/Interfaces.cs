using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace CalculatorAPI
{
    #region Interfaces


    public interface IOperation
    {
        int Operate(int left, int right);
    }

    public interface IOperationData
    {
        Char Symbol { get; }
    }


    public interface ICalculator
    {
        String Calculate(String input);
    }
    #endregion

    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Interfaces nie registrieren, nur implementierende Typen
            /*builder.RegisterType<IOperation>().AsSelf();
            builder.RegisterType<IOperationData>().AsSelf();
            builder.RegisterType<ICalculator>().AsSelf();*/
        }
    }
}
