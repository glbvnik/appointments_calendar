using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AppointmentsCalendar
{
    public static class Database
    {
        public static BindingList<Patient> Patients { get; private set; } = new BindingList<Patient>();
        public static BindingList<Doctor> Doctors { get; private set; } = new BindingList<Doctor>();
        public static BindingList<Date> Dates { get; private set; } = new BindingList<Date>();
        public static BindingList<AppointmentTime> ApptsTime { get; private set; } = new BindingList<AppointmentTime>();

        static Database()
        {
            /*Patients.Add(new Patient(0000, "Jan", "Novák", "1975.10.25", "Česko", "Rybalkova 293/39", "123456/1234", "+420000000001", "xxxxxxxxxxxx@gmail.com"));
            Patients.Add(new Patient(0001, "Anna", "Procházková", "1980.08.18", "Rusko", "Sokolská 62", "333333/3333", "+420000000002", "yyyyyyyyyyyy@gmail.com"));
            Patients.Add(new Patient(0002, "Eva", "Svobodová", "2005.10.15", "Česko", "Pod sídlištěm 1800/9", "555555/5555", "+420000000003", "zzzzzzzzzzzz@gmail.com"));
            Patients.Add(new Patient(0003, "Jaroslav", "Dvořák", "1995.05.19", "Ukrajina", "Štěpánská 619/28", "131313/1313", "+420000000004", "aaaaaaaaaaaa@gmail.com"));
            Patients.Add(new Patient(0004, "Karel", "Novotný", "1970.07.17", "Česko", "Vodičkova 681/18", "151515/1515", "+420000000005", "bbbbbbbbbbbb@gmail.com"));
            Doctors.Add(new Doctor(0000, "Petr", "Kučera", "chirurgie", "+420000000011", "xxxxxxxxxxx1@gmail.com"));
            Doctors.Add(new Doctor(0001, "Tomáš", "Černý", "traumatologie", "+420000000022", "yyyyyyyyyyy2@gmail.com"));
            Doctors.Add(new Doctor(0002, "Karel", "Veselý", "ortopedie", "+380000000033", "zzzzzzzzzzz3@gmail.com"));
            Doctors.Add(new Doctor(0003, "Josef", "Svoboda", "stomatologie", "+420000000044", "aaaaaaaaaaa4@gmail.com"));
            Doctors.Add(new Doctor(0004, "Pavel", "Horák", "pediatrie", "+420000000055", "bbbbbbbbbbb5@gmail.com"));*/
            Deserializuj();
        }

        public static void Serializuj()
        {
            Serializuj(Dates, "dates.bin");
            Serializuj(Patients, "patients.bin");
            Serializuj(Doctors, "doctors.bin");
            Serializuj(ApptsTime, "appts.bin");
        }
        public static void Serializuj<T>(BindingList<T> list, string soubor)
        {
            using (Stream s = File.Open(soubor, FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(s, list);
            }
        }
        public static void Deserializuj()
        {
            Dates = Deserializuj<Date>("dates.bin");
            Patients = Deserializuj<Patient>("patients.bin");
            Doctors = Deserializuj<Doctor>("doctors.bin");
            ApptsTime = Deserializuj<AppointmentTime>("appts.bin");
        }
        public static BindingList<T> Deserializuj<T>(string soubor)
        {
            using (Stream s = File.Open(soubor, FileMode.Open))
            {
                BinaryFormatter b = new BinaryFormatter();
                return (BindingList<T>)b.Deserialize(s);
            }
        }
    }
}
