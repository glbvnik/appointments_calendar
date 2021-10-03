using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace AppointmentsCalendar
{
    public partial class AppointmentDialog : Form
    {
        public ObservableCollection<AppointmentTime> SaveSearch { get; private set; } = new ObservableCollection<AppointmentTime>();
        public BindingList<AppointmentTime> SearchedAppts { get; private set; } = new BindingList<AppointmentTime>();

        public AppointmentDialog()
        {
            InitializeComponent();

            cmbPatient.DataSource = Database.Patients;
            cmbPatient.DisplayMember = "FullName";
            cmbDoctor.DataSource = Database.Doctors;
            cmbDoctor.DisplayMember = "FullName";

            txtHrs.Text = Program.hour.ToString();
            txtFrom.Text = "0";
            txtTo.Text = "0";

            //Vytvoření příslušného data
            Date d = new Date();
            Date.CreateDate(ref d); 

            dgvAppts.DataSource = d.AppointmentsTime;
            dgvAppts.Columns[8].DisplayIndex = 0;
            dgvAppts.Columns[0].Visible = false;
            dgvAppts.Columns[1].Visible = false;
            dgvAppts.Columns[2].Visible = false;
            dgvAppts.Columns[3].HeaderText = "Patient index";
            dgvAppts.Columns[4].HeaderText = "Patient full name";
            dgvAppts.Columns[5].HeaderText = "Doctor index";
            dgvAppts.Columns[6].HeaderText = "Doctor full name";
            dgvAppts.Columns[8].HeaderText = "Time";
            dgvAppts.Columns[4].Width = 125;
            dgvAppts.Columns[6].Width = 125;

            this.AutoSize = true;

            btnDelete.Enabled = (dgvAppts.Rows.Count > 0);
        }

        //Zjistí, jestli je možné vytvořit nový zápis na vybraný čas
        //Jestli třeba existuje zápis od 15 min do 30 min, tak mužeme vytvořit nový zápis jen takový, že začína nebo dříve (třeba od 0 do 15) nebo později (třeba od 30 do 45)
        //Jestli zadáno číslo od min nebo do min vchází v interval času už nějakého existijícího zápisu, tak tento nový zápis není možný
        //Výš uvedené neplatí, když u nového zápisu vybíráme novou hodinu nebo nového pacienta a lékaře
        private void CheckApptsDatabase()
        {
            int hrs = Convert.ToInt32(txtHrs.Text);
            int minFrom = Convert.ToInt32(txtFrom.Text);
            int minTo = Convert.ToInt32(txtTo.Text);

            if (hrs >= 7 && hrs <= 18 && minFrom >= 0 && minFrom < 60 && minTo <= 60 && minTo > 0) //spravný format zadíní času zápis
            {
                if (minFrom < minTo)
                {
                    Date d = new Date();

                    Date.CreateDate(ref d);

                    Patient p = (Patient)cmbPatient.SelectedItem;
                    Doctor doc = (Doctor)cmbDoctor.SelectedItem;
                    AppointmentTime at = new AppointmentTime(hrs, minFrom, minTo, p.Index, p.FullName, doc.Index, doc.FullName, doc.Specialization);
                    Database.ApptsTime.Add(at);

                    if (d.AppointmentsTime.Count != 0) //jestli již existují zápisy na toto datum, tak comparator pak zkontroluje, jestli nový zápis není v intervalu času už existujících zápisu
                    {
                        int numOverlap = 0; //počet zásahu času nového zápisu do času již existujících zápisu
                        int row = 0; //řádek zápisu, do kterého nový zápis zasáhne

                        for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                        {
                            if (d.AppointmentsTime[i].CompareTo(at) == -1 || d.AppointmentsTime[i].CompareTo(at) == 0)
                            {
                                numOverlap++;
                                row = i;
                            }
                        }
                        if (numOverlap > 0)
                        {
                            MessageBox.Show("Choose other time of the appointment!");
                            dgvAppts.Rows[row].Selected = true;
                        }
                        else
                        {
                            d.AppointmentsTime.Add(at);
                            Database.ApptsTime.Add(at);
                        }
                    }
                    else
                    {
                        d.AppointmentsTime.Add(at);
                        Database.ApptsTime.Add(at);
                    }
                }
                else
                {
                    MessageBox.Show("Min to must be greater than min from!");
                }
            }
            else
            {
                MessageBox.Show("Change the time!\n\nHours from 7 to 18\nMinutes from 0 to 60");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHrs.Text) || string.IsNullOrEmpty(txtFrom.Text) || string.IsNullOrEmpty(txtTo.Text))
            {
                MessageBox.Show("Type in numbers!");
            }
            else
            {
                string strHour = txtHrs.Text.ToString();
                string strFrom = txtFrom.Text.ToString();
                string strTo = txtTo.Text.ToString();

                if (Regex.IsMatch(strHour, @"^\d+$") && Regex.IsMatch(strFrom, @"^\d+$") && Regex.IsMatch(strTo, @"^\d+$"))
                {
                    CheckApptsDatabase();
                    btnDelete.Enabled = (dgvAppts.Rows.Count > 0);
                }
                else
                {
                    MessageBox.Show("Time must be numerical!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Date d = new Date();
            for (int i = Database.Dates.Count - 1; i >= 0; i--)
            {
                if (Database.Dates[i].Year == Program.date.Year && Database.Dates[i].Month == Program.date.Month && Database.Dates[i].Day == Program.day)
                {
                    d = Database.Dates[i];
                }
            }

            AppointmentTime at = (AppointmentTime)dgvAppts.CurrentRow.DataBoundItem;
            d.AppointmentsTime.Remove(at);

            if(SaveSearch.Count != 0) // smaže zápis i v seznamu na hledání
            {
                SearchedAppts.Remove(at);
            }

            btnDelete.Enabled = (dgvAppts.Rows.Count > 0);
        }

        //Pomáhá zadat interval času zápisu
        private void numApptTime_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFrom.Text))
            {
                MessageBox.Show("Type in min from!");
            }
            else
            {
                string minFrom = txtFrom.Text;

                if (Regex.IsMatch(minFrom, @"^\d+$"))
                {
                    int numValue = Convert.ToInt32(numApptTime.Value);
                    int fromValue = Convert.ToInt32(txtFrom.Text);
                    int newTo = fromValue + numValue;

                    txtTo.Text = newTo.ToString();
                }
                else
                {
                    MessageBox.Show("Min from and min to must be numerical!");
                }
            }
        }

        //Proměnné, aby program věděl, jakým způsobem třídit zápisy (asc nebo desc)
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = 0;
        //Třídí zápisy
        private void dgvAppts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Date d = new Date();
            Date.CreateDate(ref d);
            if (e.ColumnIndex == 3 && num3 == 0) //asc trídění
            {
                //Třídit BindingList přímo není možné, proto přepíšu ho přes List
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.PatientIndex).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num3 = 1;
            }
            else if (e.ColumnIndex == 3 && num3 == 1) //desc třídění
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.PatientIndex).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num3 = 0;
            }
            if (e.ColumnIndex == 4 && num4 == 0)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.PatientFullName).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num4 = 1;
            }
            else if (e.ColumnIndex == 4 && num4 == 1)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.PatientFullName).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num4 = 0;
            }
            if (e.ColumnIndex == 5 && num5 == 0)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.DoctorIndex).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num5 = 1;
            }
            else if (e.ColumnIndex == 5 && num5 == 1)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.DoctorIndex).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num5 = 0;
            }
            if (e.ColumnIndex == 6 && num6 == 0)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.DoctorFullName).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num6 = 1;
            }
            else if (e.ColumnIndex == 6 && num6 == 1)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.DoctorFullName).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num6 = 0;
            }
            if (e.ColumnIndex == 7 && num7 == 0)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.Specialization).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num7 = 1;
            }
            else if (e.ColumnIndex == 7 && num7 == 1)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.Specialization).ThenBy(at => at.Hrs).ThenBy(at => at.MinFrom).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num7 = 0;
            }
            if (e.ColumnIndex == 8 && num8 == 0)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderBy(at => at.Hrs).ThenBy(at => at.MinFrom).ThenBy(at => at.PatientIndex).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num8 = 1;
            }
            else if (e.ColumnIndex == 8 && num8 == 1)
            {
                var sortedList = new BindingList<AppointmentTime>(d.AppointmentsTime.OrderByDescending(at => at.Hrs).ThenBy(at => at.MinFrom).ThenBy(at => at.PatientIndex).ToList());
                for (int i = d.AppointmentsTime.Count - 1; i >= 0; i--)
                {
                    d.AppointmentsTime[i] = sortedList[i];
                }
                num8 = 0;
            }
        }

        //Hledá zápis zadáním jména čí příjmení (musí začínat z velkého písmena)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;

            Date d = new Date();
            Date.CreateDate(ref d);

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgvAppts.DataSource = d.AppointmentsTime;

                btnCreate.Enabled = true;
            }
            else
            {
                SaveSearch = d.AppointmentsTime.Where(at => at.PatientFullName.Contains(search) || at.DoctorFullName.Contains(search)).ToObservableCollection<AppointmentTime>();
                SearchedAppts = SaveSearch.ToBindingList();
                dgvAppts.DataSource = SearchedAppts;

                btnCreate.Enabled = false;
            }

            btnDelete.Enabled = (dgvAppts.Rows.Count > 0);
        }

        //Smaže vytvořenu instanci data, jestli neobsahuje zápisy
        private void AppointmentDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = Database.Dates.Count - 1; i >= 0; i--)
            {
                if (Database.Dates[i].Day == Program.day)
                {
                    if (Database.Dates[i].AppointmentsTime.Count == 0)
                    {
                        Database.Dates.RemoveAt(i);
                    }
                }
            }
        }
    }
}
