using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AppointmentsCalendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dgvCalendar.AutoGenerateColumns = false;
            dgvCalendar.EnableHeadersVisualStyles = false;

            LoadCalendar();
            EditDgv();
            SetCellsValues();
            ReCheckCellsValues();

            this.AutoSize = true;
        }

        //Proměnné na vytváření císel dnů ve záhlavích slopcích
        int date = 0;

        private void CreateColumns(DayOfWeek dayOfWeek)
        {
            string day = date.ToString() + " " + dayOfWeek.ToString();
            int columnIndex = dgvCalendar.Columns.Add(day, day);

            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                dgvCalendar.Columns[columnIndex].DefaultCellStyle.BackColor = ControlPaint.Light(Color.DarkOrange);
                dgvCalendar.Columns[columnIndex].HeaderCell.Style.BackColor = ControlPaint.Light(Color.DarkOrange);
            }
        }

        //Vytváří tabulku
        private void LoadCalendar()
        {
            date = 0;
            dgvCalendar.Rows.Clear();
            dgvCalendar.Columns.Clear();

            Program.date = calCalendar.SelectionRange.Start; //uloží vybrané datum z kalendáře

            LoadMonth();
            EditDgv();

            lblCurrentMonth.Text = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat.GetMonthName(Program.date.Month);
            lblYear.Text = Program.date.Year.ToString();
        }

        public void LoadMonth()
        {
            int numDays = DateTime.DaysInMonth(Program.date.Year, Program.date.Month);  //počet dnů ve vybraném měsíci

            DateTime firstDay = new DateTime(Program.date.Year, Program.date.Month, 1);

            for (int i = 0; i < numDays; i++) //vytváří slouopce
            {
                date++;
                CreateColumns(firstDay.DayOfWeek);
                firstDay = firstDay.AddDays(1);
            }

            dgvCalendar.Rows.Add(11);

            for (int i = 0; i <= 11; i++) //přejmenovuje záhlaví řádků
            {
                int time = i + 7;
                dgvCalendar.Rows[i].HeaderCell.Value = time.ToString() + " hrs";
            }
        }

        private void EditDgv()
        {
            dgvCalendar.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 8);
            dgvCalendar.RowHeadersDefaultCellStyle.Font = new Font("Verdana", 7);
            dgvCalendar.DefaultCellStyle.Font = new Font("Geneva", 7);

            dgvCalendar.ForeColor = ControlPaint.Light(Color.White);
            dgvCalendar.RowsDefaultCellStyle.SelectionBackColor = ControlPaint.Light(Color.DodgerBlue);

            foreach (DataGridViewColumn column in dgvCalendar.Columns)
            {
                column.Width = 115;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (DataGridViewRow row in dgvCalendar.Rows)
            {
                row.Height = 45;
            }

            for (int i = 0; i < dgvCalendar.ColumnCount; i++)
            {
                dgvCalendar.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //Nastaví hodnoty buněk na 0
        private void SetCellsValues()
        {
            int numDays = DateTime.DaysInMonth(Program.date.Year, Program.date.Month) - 1; //celkový počet dnů

            for (int i = 11; i >= 0; i--)
            {
                for (int j = numDays; j >= 0; j--)
                {
                    dgvCalendar.Rows[i].Cells[j].Value = "Appt: 0";
                }
            }
        }


        //Počítá počet udělaných zápisů k lékaři za jednotlivou hodinu v buňce
        private void CountNumberOfAppts()
        {
            Date d = new Date();
            Date.CreateDate(ref d);
            int numAppt = d.AppointmentsTime.Count(t => t.Hrs == Program.hour); //počet zápisů ve vybraném dnu, kde se hodina zápisu rovná hodině ve záhlaví vybraného řádku

            if (numAppt != 0)
            {
                dgvCalendar.Rows[dgvCalendar.CurrentCell.RowIndex].Cells[dgvCalendar.CurrentCell.ColumnIndex].Value = "Appt: " + numAppt;
                dgvCalendar.Rows[dgvCalendar.CurrentCell.RowIndex].Cells[dgvCalendar.CurrentCell.ColumnIndex].Style.BackColor = ControlPaint.Dark(Color.Blue);
            }

            CheckCellsValues();
        }

        //Kontroluje hodnoty buněk u všech řadku vybraného data 
        private void CheckCellsValues()
        {

            Date d = new Date();
            Date.CreateDate(ref d);
            for (int i = 11; i >= 0; i--) //11 - počet řádku
            {
                string strRowHour = dgvCalendar.Rows[i].HeaderCell.Value.ToString(); //hodnota záhlaví řádku
                int rowHour = Convert.ToInt32(Regex.Replace(strRowHour, "[^0-9]", ""));
                string strCellValue = dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Value.ToString(); //hodnota buňky ve vybraném sloupci
                int cellValue = Convert.ToInt32(Regex.Replace(strCellValue, "[^0-9]", ""));

                //Jestli se hodnota buňky nerovná počtu zápisů ve vybraném sloupci/datu, kde se hodnota záhlaví řádku rovná hodnotě vlastnosi "Hour" data (záhlaví == hodina)
                if (cellValue != d.AppointmentsTime.Count(t => t.Hrs == rowHour))
                {
                    //Jestli tento počet nenulový, udělá: hodnota buňky = tento počet
                    if (d.AppointmentsTime.Count(t => t.Hrs == rowHour) != 0)
                    {
                        dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Value = "Appt: " +
                        d.AppointmentsTime.Count(t => t.Hrs == rowHour);
                        dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Style.BackColor = ControlPaint.Dark(Color.Blue);
                    }
                    else
                    {
                        dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Value = "Appt: 0"; //jinak počet je nula
                        if (dgvCalendar.Columns[dgvCalendar.CurrentCell.ColumnIndex].HeaderCell.Style.BackColor == ControlPaint.Light(Color.DarkOrange))
                        {
                            dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Style.BackColor = ControlPaint.Light(Color.DarkOrange);
                        }
                        else
                        {
                            dgvCalendar.Rows[i].Cells[dgvCalendar.CurrentCell.ColumnIndex].Style.BackColor = ControlPaint.Light(Color.White);
                        }
                    }
                }
            }
        }

        //Nastaví správné hodnoty buněk u existujících dat při spuštění programu a při změně měsíce v kalendáře
        private void ReCheckCellsValues()
        {
            if (Database.Dates.Count != 0)
            {
                for (int i = Database.Dates.Count - 1; i >= 0; i--)
                {
                    for (int j = 11; j >= 0; j--)
                    {
                        string strHour = dgvCalendar.Rows[j].HeaderCell.Value.ToString();
                        int hour = Convert.ToInt32(Regex.Replace(strHour, "[^0-9]", ""));
                        if (Database.Dates[i].Year == Program.date.Year && Database.Dates[i].Month == Program.date.Month) //jestli tento měsíc a rok je příslušný vybranému z kalendáře
                        {
                            if (Convert.ToInt32(Database.Dates[i].AppointmentsTime.Count(d => d.Hrs == hour)) != 0) //jestli existují k tomuto datu zápisy
                            {
                                dgvCalendar.Rows[j].Cells[Database.Dates[i].Day - 1].Value = "Appt: " + Convert.ToInt32(Database.Dates[i].AppointmentsTime.Count(d => d.Hrs == hour));
                                dgvCalendar.Rows[j].Cells[Database.Dates[i].Day - 1].Style.BackColor = ControlPaint.Dark(Color.Blue);
                            }
                        }
                    }
                }
            }
        }

        //Otevří dialog zápisu
        private void dgvCalendar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string rowHour = dgvCalendar.Rows[dgvCalendar.CurrentCell.RowIndex].HeaderCell.Value.ToString();
            Program.hour = Convert.ToInt32(Regex.Replace(rowHour, "[^0-9]", ""));

            int month = Program.date.Month;
            string strDay = dgvCalendar.Columns[dgvCalendar.CurrentCell.ColumnIndex].HeaderCell.Value.ToString();
            Program.day = Convert.ToInt32(Regex.Replace(strDay, "[^0-9]", ""));
            int year = Program.date.Year;

            Date d = new Date(year, month, Program.day);
            Database.Dates.Add(d);

            AppointmentDialog appointmentDialog = new AppointmentDialog();
            appointmentDialog.ShowDialog();

            CountNumberOfAppts();
        }

        //Změní měsíc tabulky
        private void calCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadCalendar();
            SetCellsValues();
            ReCheckCellsValues();
        }

        //Změní měsíc/den tabulky
        private void calCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            LoadCalendar();
            SetCellsValues();
            ReCheckCellsValues();

            dgvCalendar.Rows[0].Cells[Program.date.Day - 1].Selected = true;
        }

        //Smaže všechny data s jejich zápisy, které jdou do dnešního data
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Date d = new Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            for (int i = Database.Dates.Count - 1; i >= 0; i--)
            {
                if (Database.Dates[i].CompareTo(d) == -1)
                {
                    Database.Dates[i].AppointmentsTime.Clear();
                    Database.Dates.RemoveAt(i);
                }
            }
            LoadCalendar();
            SetCellsValues();
            ReCheckCellsValues();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            PatientDialog patientDialog = new PatientDialog();
            patientDialog.Show();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            DoctorDialog doctorDialog = new DoctorDialog();
            doctorDialog.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.Serializuj();
        }
    }
}
