using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CalculatorAPI;
using Autofac;
using Autofac.Integration.Mef;
using System.IO;
using System.Security.Policy;

namespace SimpleCalculator
{


    #region Programm


    class Program
    {

        #region FileSystemWatcher

        /// <summary>
        /// Überwachung des DLL Verzeichnisses auf neue DLL
        /// </summary>
        private static FileSystemWatcher watcher;

        /// <summary>
        /// Starte den Watcher
        /// </summary>
        private static void StartDLLWatcher()
        {
            watcher = new FileSystemWatcher { Path = pluginPath, Filter = "*.dll" };
            watcher.Created += dllChanged;
            watcher.Deleted += dllChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static void dllChanged(object sender, FileSystemEventArgs e)
        {
            AppDomain.Unload(domain);
            SetupDomain();
            //runner.BuildIoC();
        }

        #endregion

        private static AppDomain domain;

        private static String pluginPath = @"D:\temp\extensions";
        private static String cachePath = Path.Combine(pluginPath, "ShadowCopyCache");
        private static Runner runner { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            CreateDirectorysIfNotExists();
            SetupDomain();
            StartDLLWatcher();

            Console.WriteLine("The main AppDomain is: {0}", AppDomain.CurrentDomain.FriendlyName);


            String s;
            Console.WriteLine("Enter Command:");
            while (true)
            {
                s = Console.ReadLine();

                if (s.Equals("q"))
                {
                    // Clean up.
                    AppDomain.Unload(domain);
                    return;
                }

                Console.WriteLine(runner.calculator.Calculate(s));
            }

        }

        private static void SetupDomain()
        {
            // This creates a ShadowCopy of the MEF DLL's (and any other DLL's in the ShadowCopyDirectories)
            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath
            };


            // Create a new AppDomain then create an new instance of this application in the new AppDomain.
            // This bypasses the Main method as it's not executing it.
            domain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);
            runner = (Runner)domain.CreateInstanceAndUnwrap(typeof(Runner).Assembly.FullName, typeof(Runner).FullName);
        }

        private static void CreateDirectorysIfNotExists()
        {
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            if (!Directory.Exists(pluginPath))
            {
                Directory.CreateDirectory(pluginPath);
            }
        }
    }
    #endregion

}
