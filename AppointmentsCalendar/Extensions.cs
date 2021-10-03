using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsCalendar
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> collection)
        {
            return new BindingList<T>(collection.ToList());
        }
    }
}