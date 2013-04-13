using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using IndigoEngine;
using IndigoEngine.Agents;
using NLog;

namespace TextUI
{
    class Program
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main(string[] args)
        { 
            IObservableModel model = new Model();

            TextUIShell.Model = model;

            TextUIShell.Run();
        }
    }
}
