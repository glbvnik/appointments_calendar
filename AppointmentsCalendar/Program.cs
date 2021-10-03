using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentsCalendar
{
    static class Program
    {
        public static DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //vytvoření data příslušného sloupci tabulky
        public static int day; //den příslušný vybranému sloupci tabulky
        public static int hour; //hodina příslušná vybranému řádku tabulky
        public static int rowPatient; //získá index pacienta, aby bylo možné mu správně upravovat údaje
        public static int rowDoctor; //získá index lékaře, aby bylo možné mu správně upravovat údaje

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
