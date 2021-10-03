using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentsCalendar
{
    [Serializable()]
    public class AppointmentTime : IComparable
    {
        public int Hrs { get; set; }
        public int MinFrom { get; set; }
        public int MinTo { get; set; }
        public int PatientIndex { get; set; }
        public string PatientFullName { get; set; }
        public int DoctorIndex { get; set; }
        public string DoctorFullName { get; set; }
        public string Specialization { get; set; }
        public string FullTime
        {
            get
            {
                //udělá spravný format zobrazování času
                if (MinFrom < 10 && MinTo < 10)
                {
                    return Hrs.ToString() + ":0" + MinFrom.ToString() + " - " + Hrs.ToString() + ":0" + MinTo.ToString();
                }
                else if (MinFrom < 10 && MinTo >= 10)
                {
                    return Hrs.ToString() + ":0" + MinFrom.ToString() + " - " + Hrs.ToString() + ":" + MinTo.ToString();
                }
                else if (MinFrom >= 10 && MinTo < 10)
                {
                    return Hrs.ToString() + ":" + MinFrom.ToString() + " - " + Hrs.ToString() + ":0" + MinTo.ToString();
                }
                else
                {
                    return Hrs.ToString() + ":" + MinFrom.ToString() + " - " + Hrs.ToString() + ":" + MinTo.ToString();
                }
            }
        }

        public AppointmentTime(int hrs, int minFrom, int minTo, int patientIndex, string patientFullName, int doctorIndex, string doctorFullName, string specialization)
        {
            Hrs = hrs;
            MinFrom = minFrom;
            MinTo = minTo;
            PatientIndex = patientIndex;
            PatientFullName = patientFullName;
            DoctorIndex = doctorIndex;
            DoctorFullName = doctorFullName;
            Specialization = specialization;
        }

        //Zjistí jestli nová instance zápisu není v intervalu už existujících zápisu
        public int CompareTo(object obj)
        {
            AppointmentTime at = obj as AppointmentTime;
            if(at != null)
            {
                if(this.Hrs == at.Hrs && this.MinFrom < at.MinFrom && this.MinTo > at.MinTo && this.MinFrom < at.MinTo && this.MinTo > at.MinFrom && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex) ||
                    this.Hrs == at.Hrs && this.MinFrom < at.MinFrom && this.MinTo < at.MinTo && this.MinFrom < at.MinTo && this.MinTo > at.MinFrom && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex) ||
                    this.Hrs == at.Hrs && this.MinFrom > at.MinFrom && this.MinTo > at.MinTo && this.MinFrom < at.MinTo && this.MinTo > at.MinFrom && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex) ||
                    this.Hrs == at.Hrs && this.MinFrom > at.MinFrom && this.MinTo < at.MinTo && this.MinFrom < at.MinTo && this.MinTo > at.MinFrom && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex))
                {
                    return -1;
                }
                else if(this.Hrs == at.Hrs && this.MinFrom == at.MinFrom && this.MinTo == at.MinTo && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex) ||
                    this.Hrs == at.Hrs && this.MinFrom == at.MinFrom && (this.MinTo <= at.MinTo || this.MinTo >= at.MinTo) && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex) ||
                    this.Hrs == at.Hrs && (this.MinFrom <= at.MinFrom || this.MinFrom >= at.MinFrom) && this.MinTo == at.MinTo && (this.PatientIndex == at.PatientIndex || this.DoctorIndex == at.DoctorIndex))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
