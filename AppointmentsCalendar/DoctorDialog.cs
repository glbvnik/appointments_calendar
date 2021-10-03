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
    public partial class DoctorDialog : Form
    {
        DoctorAddEditDialog doctorDialog = new DoctorAddEditDialog();
        public ObservableCollection<Doctor> SaveSearch { get; private set; } = new ObservableCollection<Doctor>();
        public BindingList<Doctor> SearchedDoctor { get; private set; } = new BindingList<Doctor>();
        public DoctorDialog()
        {
            InitializeComponent();

            dgvDoctors.DataSource = Database.Doctors;
            EditDgv();
            this.AutoSize = true;

            btnDelete.Enabled = (dgvDoctors.Rows.Count > 0);
        }

        public void EditDgv()
        {
            dgvDoctors.Columns["Index"].DisplayIndex = 0;
            dgvDoctors.Columns["Name"].DisplayIndex = 1;
            dgvDoctors.Columns["Surname"].DisplayIndex = 2;
            dgvDoctors.Columns[6].Visible = false;
            dgvDoctors.Columns[1].Width = 35;
            dgvDoctors.Columns[5].Width = 175;
            dgvDoctors.Columns[1].HeaderText = "Id";
            dgvDoctors.Columns[5].HeaderText = "E-mail";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            doctorDialog.Action = ActionType1.New;
            if (doctorDialog.ShowDialog() == DialogResult.OK)
            {
                Database.Doctors.Add(doctorDialog.DoctorInstance);
                btnDelete.Enabled = (dgvDoctors.Rows.Count > 0);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Program.rowDoctor = dgvDoctors.CurrentCell.RowIndex;
            doctorDialog.Action = ActionType1.Edit;
            doctorDialog.DoctorInstance = (Doctor)dgvDoctors.CurrentRow.DataBoundItem;
            doctorDialog.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Doctor doc = (Doctor)dgvDoctors.CurrentRow.DataBoundItem;
            Database.Doctors.Remove(doc);

            if (SaveSearch.Count != 0)
            {
                SearchedDoctor.Remove(doc);
            }

            btnDelete.Enabled = (dgvDoctors.Rows.Count > 0);
        }

        //Hledá lékaře zadáním jména čí příjmení (musí začínat z velkého písmena)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgvDoctors.DataSource = Database.Doctors;
            }
            else
            {
                SaveSearch = Database.Doctors.Where(p => p.FullName.Contains(search)).ToObservableCollection<Doctor>();
                SearchedDoctor = SaveSearch.ToBindingList();
                dgvDoctors.DataSource = SearchedDoctor;
            }

            btnDelete.Enabled = (dgvDoctors.Rows.Count > 0);
        }

        //Proměnné, aby program věděl, jakým způsobem třídit lékaře (asc nebo desc)
        int num0 = 0;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        //Třídí lékaře
        private void dgvDoctors_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && num0 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Specialization).ThenBy(doc => doc.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num0 = 1;
                }
            }
            else if (e.ColumnIndex == 0 && num0 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Specialization).ThenBy(doc => doc.Index).ThenBy(doc => doc.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num0 = 0;
                }
            }
            if (e.ColumnIndex == 1 && num1 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Index).ThenBy(doc => doc.Name).ThenBy(doc => doc.Surname).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num1 = 1;
                }
            }
            else if (e.ColumnIndex == 1 && num1 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Index).ThenBy(doc => doc.Name).ThenBy(doc => doc.Surname).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num1 = 0;
                }
            }
            if (e.ColumnIndex == 2 && num2 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Name).ThenBy(doc => doc.Index).ThenBy(p => p.Surname).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num2 = 1;
                }
            }
            else if (e.ColumnIndex == 2 && num2 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Name).ThenBy(doc => doc.Index).ThenBy(doc => doc.Surname).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num2 = 0;
                }
            }
            if (e.ColumnIndex == 3 && num3 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Surname).ThenBy(doc => doc.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num3 = 1;
                }
            }
            else if (e.ColumnIndex == 3 && num3 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Surname).ThenBy(doc => doc.Index).ThenBy(doc => doc.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num3 = 0;
                }
            }
            if (e.ColumnIndex == 4 && num4 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Phone).ThenBy(doc => doc.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num4 = 1;
                }
            }
            else if (e.ColumnIndex == 4 && num4 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Phone).ThenBy(doc => doc.Index).ThenBy(doc => doc.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num4 = 0;
                }
            }
            if (e.ColumnIndex == 5 && num5 == 0)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderBy(doc => doc.Email).ThenBy(doc => doc.Index).ThenBy(p => p.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num5 = 1;
                }
            }
            else if (e.ColumnIndex == 5 && num5 == 1)
            {
                var sortedList = new BindingList<Doctor>(Database.Doctors.OrderByDescending(doc => doc.Email).ThenBy(doc => doc.Index).ThenBy(doc => doc.Name).ToList());
                for (int i = Database.Doctors.Count - 1; i >= 0; i--)
                {
                    Database.Doctors[i] = sortedList[i];
                    num5 = 0;
                }
            }
        }
    }
}
