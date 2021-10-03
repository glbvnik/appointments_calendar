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

namespace AppointmentsCalendar
{
    public enum Specialization { chirurgie, traumatologie, ortopedie, oftalmologie, stomatologie, kardiologie, pneumologie, gastroenterologie, pediatrie, neurologie, dermatologie}
    public enum ActionType1 { New, Edit }
    public partial class DoctorAddEditDialog : Form
    {
        public ActionType1 Action { get; set; } = ActionType1.New;
        public Doctor DoctorInstance { get; set; }
        public DoctorAddEditDialog()
        {
            InitializeComponent();

            this.AutoSize = true;

            cmbSpecialization.DataSource = Enum.GetValues(typeof(Specialization));
        }

        private void DoctorAddEditDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (Action == ActionType1.Edit)
            {
                this.Text = "Edit";
                txtIndex.Text = DoctorInstance.Index.ToString();
                txtName.Text = DoctorInstance.Name;
                txtSurname.Text = DoctorInstance.Surname;
                cmbSpecialization.Text = DoctorInstance.Specialization;
                txtPhone.Text = DoctorInstance.Phone;
                txtEmail.Text = DoctorInstance.Email;
            }
            else
            {
                this.Text = "Add";
                int index = Database.Doctors.Select(p => p.Index).Max() + 1;
                txtIndex.Text = index.ToString(); //Pole bude zobrazovat následující volný index
                txtName.Text = "";
                txtSurname.Text = "";
                cmbSpecialization.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string index = txtIndex.Text;
            switch (Action)
            {
                case ActionType1.New:
                    if (string.IsNullOrEmpty(txtIndex.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Type index, name and surname!");
                    }
                    else
                    {
                        if (Regex.IsMatch(index, @"^\d+$"))
                        {
                            Doctor l = new Doctor(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, cmbSpecialization.Text, txtPhone.Text, txtEmail.Text);

                            //Index každého lékaře musí být unikátní
                            if (Database.Doctors.Count(l0 => l0.Index == l.Index) != 0)
                            {
                                this.DialogResult = DialogResult.None;
                                MessageBox.Show("Change the index number!");
                            }
                            else
                            {
                                DoctorInstance = new Doctor(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, cmbSpecialization.Text, txtPhone.Text, txtEmail.Text);
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show("Index must be numerical!");
                        }
                    }
                    break;

                case ActionType1.Edit:
                    if (string.IsNullOrEmpty(txtIndex.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Type index, name and surname!");
                    }
                    else
                    {
                        if (Regex.IsMatch(index, @"^\d+$"))
                        {
                            Doctor l = new Doctor(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, cmbSpecialization.Text, txtPhone.Text, txtEmail.Text);

                            //Index každého lékaře musí být unikátní
                            if (Database.Doctors.Count(l0 => l0.Index == l.Index) != 0 && Database.Doctors[Program.rowDoctor].Index != l.Index)
                            {
                                this.DialogResult = DialogResult.None;
                                MessageBox.Show("Change the index number!");
                            }
                            else
                            {
                                DoctorInstance.Index = Convert.ToInt32(txtIndex.Text);
                                DoctorInstance.Name = txtName.Text;
                                DoctorInstance.Surname = txtSurname.Text;
                                DoctorInstance.Specialization = cmbSpecialization.Text;
                                DoctorInstance.Phone = txtPhone.Text;
                                DoctorInstance.Email = txtEmail.Text;
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show("Index must be numerical!");
                        }
                    }
                    break;
            }
        }
    }
}
