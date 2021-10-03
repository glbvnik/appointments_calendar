
namespace AppointmentsCalendar
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCalendar = new System.Windows.Forms.DataGridView();
            this.btnPatients = new System.Windows.Forms.Button();
            this.btnDoctors = new System.Windows.Forms.Button();
            this.lblCurrentMonth = new System.Windows.Forms.Label();
            this.calCalendar = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCalendar
            // 
            this.dgvCalendar.AllowUserToResizeColumns = false;
            this.dgvCalendar.AllowUserToResizeRows = false;
            this.dgvCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalendar.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCalendar.Location = new System.Drawing.Point(355, 50);
            this.dgvCalendar.MultiSelect = false;
            this.dgvCalendar.Name = "dgvCalendar";
            this.dgvCalendar.RowHeadersWidth = 88;
            this.dgvCalendar.RowTemplate.Height = 42;
            this.dgvCalendar.Size = new System.Drawing.Size(1790, 1109);
            this.dgvCalendar.TabIndex = 0;
            this.dgvCalendar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCalendar_CellDoubleClick);
            // 
            // btnPatients
            // 
            this.btnPatients.Location = new System.Drawing.Point(18, 879);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Size = new System.Drawing.Size(330, 130);
            this.btnPatients.TabIndex = 4;
            this.btnPatients.Text = "Patients";
            this.btnPatients.UseVisualStyleBackColor = true;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // btnDoctors
            // 
            this.btnDoctors.Location = new System.Drawing.Point(18, 1029);
            this.btnDoctors.Name = "btnDoctors";
            this.btnDoctors.Size = new System.Drawing.Size(330, 130);
            this.btnDoctors.TabIndex = 5;
            this.btnDoctors.Text = "Doctors";
            this.btnDoctors.UseVisualStyleBackColor = true;
            this.btnDoctors.Click += new System.EventHandler(this.btnDoctors_Click);
            // 
            // lblCurrentMonth
            // 
            this.lblCurrentMonth.AutoSize = true;
            this.lblCurrentMonth.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentMonth.Location = new System.Drawing.Point(1000, 8);
            this.lblCurrentMonth.Name = "lblCurrentMonth";
            this.lblCurrentMonth.Size = new System.Drawing.Size(93, 33);
            this.lblCurrentMonth.TabIndex = 6;
            this.lblCurrentMonth.Text = "label1";
            // 
            // calCalendar
            // 
            this.calCalendar.Font = new System.Drawing.Font("Verdana", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calCalendar.Location = new System.Drawing.Point(18, 50);
            this.calCalendar.Name = "calCalendar";
            this.calCalendar.TabIndex = 9;
            this.calCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calCalendar_DateChanged);
            this.calCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calCalendar_DateSelected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select date";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblYear.Location = new System.Drawing.Point(1350, 8);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(93, 33);
            this.lblYear.TabIndex = 11;
            this.lblYear.Text = "label1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(18, 729);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(330, 130);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2164, 1179);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calCalendar);
            this.Controls.Add(this.lblCurrentMonth);
            this.Controls.Add(this.btnDoctors);
            this.Controls.Add(this.btnPatients);
            this.Controls.Add(this.dgvCalendar);
            this.Name = "Form1";
            this.Text = "Calendar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnDoctors;
        private System.Windows.Forms.Label lblCurrentMonth;
        private System.Windows.Forms.DataGridView dgvCalendar;
        private System.Windows.Forms.MonthCalendar calCalendar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Button btnRefresh;
    }
}

