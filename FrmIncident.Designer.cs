namespace Help_Desk
{
    partial class FrmIncident
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIncident = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRequestType = new System.Windows.Forms.Label();
            this.cmbRequest1 = new System.Windows.Forms.ComboBox();
            this.Subject = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblRequestDetail = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblAttachments = new System.Windows.Forms.Label();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbRequest2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(209)))));
            this.panel1.Controls.Add(this.lblIncident);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 53);
            this.panel1.TabIndex = 0;
            // 
            // lblIncident
            // 
            this.lblIncident.AutoSize = true;
            this.lblIncident.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncident.ForeColor = System.Drawing.SystemColors.Control;
            this.lblIncident.Location = new System.Drawing.Point(3, 9);
            this.lblIncident.Name = "lblIncident";
            this.lblIncident.Size = new System.Drawing.Size(98, 25);
            this.lblIncident.TabIndex = 0;
            this.lblIncident.Text = "File Ticket";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(72)))), ((int)(((byte)(94)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(147, 397);
            this.panel2.TabIndex = 1;
            // 
            // lblRequestType
            // 
            this.lblRequestType.AutoSize = true;
            this.lblRequestType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestType.Location = new System.Drawing.Point(181, 102);
            this.lblRequestType.Name = "lblRequestType";
            this.lblRequestType.Size = new System.Drawing.Size(97, 17);
            this.lblRequestType.TabIndex = 2;
            this.lblRequestType.Text = "Request Type";
            // 
            // cmbRequest1
            // 
            this.cmbRequest1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequest1.FormattingEnabled = true;
            this.cmbRequest1.Items.AddRange(new object[] {
            "Hardware",
            "Software",
            "Network",
            "Other"});
            this.cmbRequest1.Location = new System.Drawing.Point(346, 102);
            this.cmbRequest1.Name = "cmbRequest1";
            this.cmbRequest1.Size = new System.Drawing.Size(135, 21);
            this.cmbRequest1.TabIndex = 3;
            this.cmbRequest1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Subject
            // 
            this.Subject.AutoSize = true;
            this.Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Subject.Location = new System.Drawing.Point(181, 177);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(55, 17);
            this.Subject.TabIndex = 5;
            this.Subject.Text = "Subject";
            this.Subject.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(346, 177);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 20);
            this.textBox1.TabIndex = 6;
            // 
            // lblRequestDetail
            // 
            this.lblRequestDetail.AutoSize = true;
            this.lblRequestDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestDetail.Location = new System.Drawing.Point(181, 248);
            this.lblRequestDetail.Name = "lblRequestDetail";
            this.lblRequestDetail.Size = new System.Drawing.Size(108, 17);
            this.lblRequestDetail.TabIndex = 7;
            this.lblRequestDetail.Text = "Request Details";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(346, 248);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(332, 77);
            this.textBox2.TabIndex = 8;
            // 
            // lblAttachments
            // 
            this.lblAttachments.AutoSize = true;
            this.lblAttachments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttachments.Location = new System.Drawing.Point(190, 360);
            this.lblAttachments.Name = "lblAttachments";
            this.lblAttachments.Size = new System.Drawing.Size(86, 17);
            this.lblAttachments.TabIndex = 9;
            this.lblAttachments.Text = "Attachments";
            this.lblAttachments.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(346, 360);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 10;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(346, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(473, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbRequest2
            // 
            this.cmbRequest2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequest2.FormattingEnabled = true;
            this.cmbRequest2.Items.AddRange(new object[] {
            "Computer won\'t turn on",
            "Monitor won\'t display an image",
            "Printer won\'t print",
            "Keyboard or mouse isn\'t working",
            "Application won\'t launch",
            "Error message appears when using an application",
            "File won\'t open or save",
            "Application is running slowly",
            "Unable to connect to the internet",
            "Slow internet speeds",
            "Can\'t access network resources",
            "Firewall is blocking access to a website or application",
            "Account locked out or password reset needed",
            "Request for new software to be installed",
            "General IT questions or inquiries",
            "Request for new hardware to be purchased"});
            this.cmbRequest2.Location = new System.Drawing.Point(527, 102);
            this.cmbRequest2.Name = "cmbRequest2";
            this.cmbRequest2.Size = new System.Drawing.Size(135, 21);
            this.cmbRequest2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(470, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(545, 356);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(158, 20);
            this.dateTimePicker1.TabIndex = 12;
            // 
            // FrmIncident
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.lblAttachments);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lblRequestDetail);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Subject);
            this.Controls.Add(this.cmbRequest2);
            this.Controls.Add(this.cmbRequest1);
            this.Controls.Add(this.lblRequestType);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmIncident";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIncident;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRequestType;
        private System.Windows.Forms.ComboBox cmbRequest1;
        private System.Windows.Forms.Label Subject;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblRequestDetail;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblAttachments;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbRequest2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

