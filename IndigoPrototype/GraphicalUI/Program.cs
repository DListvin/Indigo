using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IndigoEngine;
using IndigoEngine.Agents;
using NLog;
using TextUI;
using System.Threading;

namespace GraphicalUI
{
    static class Program
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IObservableModel model = new Model();
            GraphicalUIShell.Model = model;
            TextUIShell.Model = model;

            model.Initialise();

            Thread TextThread = new Thread(TextUIShell.Run);
            TextThread.Start();            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new GrapgicalUIForm());
        }
    }
}
