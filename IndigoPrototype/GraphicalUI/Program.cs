﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using IndigoEngine;
using IndigoEngine.Agents;

namespace GraphicalUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IObservableModel model = new Model();

            GraphicalUIShell.Model = model;

            Application.Run(new GrapgicalUIForm());
        }
    }
}