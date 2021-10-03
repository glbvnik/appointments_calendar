using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace AppointmentsCalendar
{
    [Serializable()]
    public abstract class Person : INotifyPropertyChanged
    {
        private int _index;
        private string _name;
        private string _surname;
        private string _phone;
        private string _email;

        public int Index 
        {
            get
            {
                return _index;
            }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Surname 
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FullName
        {
            get
            {
                return _name + " " + _surname;
            }
        }

        protected Person(int index, string name, string surname, string phone, string email)
        {
            Index = index;
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
        }

        //Oznámí dgv, že se vlastnost instance změnila
        public virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable()]
    public class Doctor : Person
    {
        private string _specialization;

        public string Specialization 
        {
            get
            {
                return _specialization;
            }
            set
            {
                if (_specialization != value)
                {
                    _specialization = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Doctor(int index, string name, string surname, string specialization, string phone, string email) : base(index, name, surname, phone, email)
        {
            Specialization = specialization;
        }

        public override void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.NotifyPropertyChanged();
        }
    }

    [Serializable()]
    public class Patient : Person
    {
        private string _dateBirth;
        private string _country;
        private string _address;
        private string _pin;

        public string DateBirth
        {
            get
            {
                return _dateBirth;
            }
            set
            {
                if (_dateBirth != value)
                {
                    _dateBirth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string PIN
        {
            get
            {
                return _pin;
            }
            set
            {
                if (_pin != value)
                {
                    _pin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Patient(int index, string name, string surname, string dateBirth, string country, string address, string pin, string phone, string email) : base(index, name, surname, phone, email)
        {
            DateBirth = dateBirth;
            Country = country;
            Address = address;
            PIN = pin;
        }

        public override void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.NotifyPropertyChanged();
        }
    }
}
