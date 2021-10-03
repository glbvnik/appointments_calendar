using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppointmentsCalendar
{
    public partial class PatientDialog : Form
    {
        PatientAddEditDialog patientDialog = new PatientAddEditDialog();
        public ObservableCollection<Patient> SaveSearch { get; private set; } = new ObservableCollection<Patient>();
        public BindingList<Patient> SearchedPatient { get; private set; } = new BindingList<Patient>();

        public PatientDialog()
        {
            InitializeComponent();
            dgvPatients.DataSource = Database.Patients;
            EditDgv();
            this.AutoSize = true;

            btnDelete.Enabled = (dgvPatients.Rows.Count > 0);
        }

        private void EditDgv()
        {
            dgvPatients.Columns["Index"].DisplayIndex = 0;
            dgvPatients.Columns["Name"].DisplayIndex = 1;
            dgvPatients.Columns["Surname"].DisplayIndex = 2;
            dgvPatients.Columns[9].Visible = false;
            dgvPatients.Columns[2].Width = 175;
            dgvPatients.Columns[4].Width = 35;
            dgvPatients.Columns[8].Width = 175;
            dgvPatients.Columns[0].HeaderText = "Date of birth";
            dgvPatients.Columns[4].HeaderText = "Id";
            dgvPatients.Columns[8].HeaderText = "E-mail";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            patientDialog.Action = ActionType.New;
            if (patientDialog.ShowDialog() == DialogResult.OK)
            {
                Database.Patients.Add(patientDialog.PatientInstance);
            }

            btnDelete.Enabled = (dgvPatients.Rows.Count > 0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Program.rowPatient = dgvPatients.CurrentCell.RowIndex;
            patientDialog.Action = ActionType.Edit;
            patientDialog.PatientInstance = (Patient)dgvPatients.CurrentRow.DataBoundItem;
            patientDialog.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Patient p = (Patient)dgvPatients.CurrentRow.DataBoundItem;
            Database.Patients.Remove(p);

            if (SaveSearch.Count != 0)
            {
                SearchedPatient.Remove(p);
            }

            btnDelete.Enabled = (dgvPatients.Rows.Count > 0);
        }

        //Proměnné, aby program věděl, jakým způsobem třídit patienty (asc nebo desc)
        int num0 = 0;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = 0;
        //Třídí patienty
        private void dgvPatients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && num0 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.DateBirth).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num0 = 1;
                }
            }
            else if (e.ColumnIndex == 0 && num0 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.DateBirth).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num0 = 0;
                }
            }
            if (e.ColumnIndex == 1 && num1 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Country).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num1 = 1;
                }
            }
            else if (e.ColumnIndex == 1 && num1 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Country).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num1 = 0;
                }
            }
            if (e.ColumnIndex == 2 && num2 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Address).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num2 = 1;
                }
            }
            else if (e.ColumnIndex == 2 && num2 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Address).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num2 = 0;
                }
            }
            if (e.ColumnIndex == 3 && num3 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.PIN).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num3 = 1;
                }
            }
            else if (e.ColumnIndex == 3 && num3 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.PIN).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num3 = 0;
                }
            }
            if (e.ColumnIndex == 4 && num4 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Index).ThenBy(p => p.Name).ThenBy(p => p.Surname).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num4 = 1;
                }
            }
            else if (e.ColumnIndex == 4 && num4 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Index).ThenBy(p => p.Name).ThenBy(p => p.Surname).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num4 = 0;
                }
            }
            if (e.ColumnIndex == 5 && num5 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Name).ThenBy(p => p.Index).ThenBy(p => p.Surname).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num5 = 1;
                }
            }
            else if (e.ColumnIndex == 5 && num5 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Name).ThenBy(p => p.Index).ThenBy(p => p.Surname).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num5 = 0;
                }
            }
            if (e.ColumnIndex == 6 && num6 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Surname).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num6 = 1;
                }
            }
            else if (e.ColumnIndex == 6 && num6 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Surname).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num6 = 0;
                }
            }
            if (e.ColumnIndex == 7 && num7 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Phone).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num7 = 1;
                }
            }
            else if (e.ColumnIndex == 7 && num7 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Phone).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num7 = 0;
                }
            }
            if (e.ColumnIndex == 8 && num8 == 0)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderBy(p => p.Email).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num8 = 1;
                }
            }
            else if (e.ColumnIndex == 8 && num8 == 1)
            {
                var sortedList = new BindingList<Patient>(Database.Patients.OrderByDescending(p => p.Email).ThenBy(p => p.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Patients.Count - 1; i >= 0; i--)
                {
                    Database.Patients[i] = sortedList[i];
                    num8 = 0;
                }
            }
        }

        //Hledá pacienta zadáním jména čí příjmení (musí začínat z velkého písmena)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgvPatients.DataSource = Database.Patients;
            }
            else
            {
                SaveSearch = Database.Patients.Where(p => p.FullName.Contains(search)).ToObservableCollection<Patient>();
                SearchedPatient = SaveSearch.ToBindingList();
                dgvPatients.DataSource = SearchedPatient;
            }

            btnDelete.Enabled = (dgvPatients.Rows.Count > 0);
        }
    }
}
