using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mef;
using CalculatorAPI;

namespace SimpleCalculator
{
    public sealed class Runner : MarshalByRefObject
    {
        [Import(typeof(ICalculator))]
        public ICalculator calculator;

        private static String dllpath = @"D:\temp\extensions";

        public IContainer container { get; set; }

        

        public void BuildIoC()
        {
            Thread.Sleep(1000);
            var builder = new ContainerBuilder();
            var pluginCatalog = new DirectoryCatalog(dllpath);

            builder.RegisterMetadataRegistrationSources();
            builder.RegisterModule<ApiModule>();
            builder.RegisterComposablePartCatalog(pluginCatalog);
            builder.RegisterComposablePartCatalog(new AssemblyCatalog(typeof(Runner).Assembly));
            pluginCatalog.Refresh();
            container = builder.Build();
            var operations = container.Resolve<IEnumerable<IOperation>>();
            calculator = container.Resolve<ICalculator>();
        }

        
        public Runner()
        {
            BuildIoC();
        }
    }
}
