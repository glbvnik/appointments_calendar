using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsCalendar
{
    [Serializable()]
    public class Date : IComparable
    {
        public int Year { get; set; } = 0;
        public int Month { get; set; } = 0;
        public int Day { get; set; } = 0;

        public Date()
        {
            
        }
        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        //Vytváří příslušné datum pro vybraný den/sloupec z tabulky
        public static void CreateDate(ref Date d)
        {
            for (int i = Database.Dates.Count - 1; i >= 0; i--)
            {
                if (Database.Dates[i].Year == Program.date.Year && Database.Dates[i].Month == Program.date.Month && Database.Dates[i].Day == Program.day)
                {
                    d = Database.Dates[i];
                }
            }
        }

        //Srovnáva všechny instance dat s dnešním datem
        public int CompareTo(object obj)
        {
            Date d = obj as Date;
            if (d != null)
            {
                if(this.Year <= d.Year && this.Month <= d.Month && this.Day < d.Day)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public BindingList<AppointmentTime> AppointmentsTime { get; private set; } = new BindingList<AppointmentTime>();
    }
}
