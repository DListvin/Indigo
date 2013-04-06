using System;
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

            Form GUIForm = new GrapgicalUIForm();
            SetDoubleBuffered(GUIForm);
            Application.Run(GUIForm);
        }

        /// <summary>
        /// Shaitan-magic from Kolya
        /// </summary>
        /// <param name="c"></param>
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                return;
            }
            System.Reflection.PropertyInfo aProp =
               typeof(System.Windows.Forms.Control).GetProperty(
               "DoubleBuffered",
               System.Reflection.BindingFlags.NonPublic |
               System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
    }
}
