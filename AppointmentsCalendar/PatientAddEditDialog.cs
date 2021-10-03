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
    public enum ActionType { New, Edit }
    public partial class PatientAddEditDialog : Form
    {
        public ActionType Action { get; set; } = ActionType.New;
        public Patient PatientInstance { get; set; }
        public PatientAddEditDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string index = txtIndex.Text;
            switch (Action)
            {
                case ActionType.New:
                    if (string.IsNullOrEmpty(txtIndex.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Type index, name and surname!");
                    }
                    else
                    {
                        if (Regex.IsMatch(index, @"^\d+$"))
                        {
                            Patient p = new Patient(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, txtDate.Text, txtCountry.Text, txtAddress.Text, txtPIN.Text, txtPhone.Text, txtEmail.Text);

                            //Index každého pacienta musí být unikátní
                            if (Database.Patients.Count(p0 => p0.Index == p.Index) != 0)
                            {
                                this.DialogResult = DialogResult.None;
                                MessageBox.Show("Change the index number!");
                            }
                            else
                            {
                                PatientInstance = new Patient(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, txtDate.Text, txtCountry.Text, txtAddress.Text, txtPIN.Text, txtPhone.Text, txtEmail.Text);
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show("Index must be numerical!");
                        }
                    }
                    break;

                case ActionType.Edit:
                    if (string.IsNullOrEmpty(txtIndex.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text))
                    {
                        this.DialogResult = DialogResult.None;
                        MessageBox.Show("Type index, name and surname!");
                    }
                    else
                    {
                        if (Regex.IsMatch(index, @"^\d+$"))
                        {
                            Patient p = new Patient(Convert.ToInt32(txtIndex.Text), txtName.Text, txtSurname.Text, txtDate.Text, txtCountry.Text, txtAddress.Text, txtPIN.Text, txtPhone.Text, txtEmail.Text);

                            //Index každého pacienta musí být unikátní
                            if (Database.Patients.Count(p0 => p0.Index == p.Index) != 0 && Database.Patients[Program.rowPatient].Index != p.Index)
                            {
                                this.DialogResult = DialogResult.None;
                                MessageBox.Show("Change the index number!");
                            }
                            else
                            {
                                PatientInstance.Index = Convert.ToInt32(txtIndex.Text);
                                PatientInstance.Name = txtName.Text;
                                PatientInstance.Surname = txtSurname.Text;
                                PatientInstance.DateBirth = txtDate.Text;
                                PatientInstance.Country = txtCountry.Text;
                                PatientInstance.Address = txtAddress.Text;
                                PatientInstance.PIN = txtPIN.Text;
                                PatientInstance.Phone = txtPhone.Text;
                                PatientInstance.Email = txtEmail.Text;
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

        private void PatientAddEditDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (Action == ActionType.Edit)
            {
                this.Text = "Edit";
                txtIndex.Text = PatientInstance.Index.ToString();
                txtName.Text = PatientInstance.Name;
                txtSurname.Text = PatientInstance.Surname;
                txtDate.Text = PatientInstance.DateBirth.ToString();
                txtCountry.Text = PatientInstance.Country;
                txtAddress.Text = PatientInstance.Address;
                txtPIN.Text = PatientInstance.PIN;
                txtPhone.Text = PatientInstance.Phone;
                txtEmail.Text = PatientInstance.Email;
            }
            else
            {
                this.Text = "Add";
                int index = Database.Patients.Select(p => p.Index).Max() + 1;
                txtIndex.Text = index.ToString(); //Pole bude zobrazovat následující volný index
                txtName.Text = "";
                txtSurname.Text = "";
                txtDate.Text = "";
                txtCountry.Text = "";
                txtAddress.Text = "";
                txtPIN.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
            }
        }
    }
}
