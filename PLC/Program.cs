using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PLC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Lambert:2017.09.08
        /// </summary>
        [STAThread]
        static void Main()
        {
            int j = 10;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
