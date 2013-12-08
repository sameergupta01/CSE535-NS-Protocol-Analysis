namespace ClientAppliaction
{
    partial class Sender
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
            this.connect = new System.Windows.Forms.Button();
            this.rtb_output = new System.Windows.Forms.RichTextBox();
            this.txtSenderUser = new System.Windows.Forms.TextBox();
            this.txtRecUser = new System.Windows.Forms.TextBox();
            this.txtSenderPass = new System.Windows.Forms.TextBox();
            this.lblSenderUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker_txtAreaUpdate = new System.ComponentModel.BackgroundWorker();
            this.cmbEncType = new System.Windows.Forms.ComboBox();
            this.cmbBoxVersion = new System.Windows.Forms.ComboBox();
            this.lblEncryptionType = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(516, 57);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(75, 23);
            this.connect.TabIndex = 4;
            this.connect.Text = "Authenticate";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // rtb_output
            // 
            this.rtb_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_output.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rtb_output.Location = new System.Drawing.Point(13, 13);
            this.rtb_output.Name = "rtb_output";
            this.rtb_output.Size = new System.Drawing.Size(737, 363);
            this.rtb_output.TabIndex = 5;
            this.rtb_output.Text = "";
            // 
            // txtSenderUser
            // 
            this.txtSenderUser.Location = new System.Drawing.Point(110, 18);
            this.txtSenderUser.Name = "txtSenderUser";
            this.txtSenderUser.Size = new System.Drawing.Size(129, 20);
            this.txtSenderUser.TabIndex = 1;
            this.txtSenderUser.Text = "samgupta";
            // 
            // txtRecUser
            // 
            this.txtRecUser.Location = new System.Drawing.Point(597, 15);
            this.txtRecUser.Name = "txtRecUser";
            this.txtRecUser.Size = new System.Drawing.Size(115, 20);
            this.txtRecUser.TabIndex = 3;
            this.txtRecUser.Text = "sbu";
            // 
            // txtSenderPass
            // 
            this.txtSenderPass.Location = new System.Drawing.Point(356, 18);
            this.txtSenderPass.Name = "txtSenderPass";
            this.txtSenderPass.PasswordChar = '*';
            this.txtSenderPass.Size = new System.Drawing.Size(115, 20);
            this.txtSenderPass.TabIndex = 2;
            this.txtSenderPass.Text = "12345";
            // 
            // lblSenderUser
            // 
            this.lblSenderUser.AutoSize = true;
            this.lblSenderUser.Location = new System.Drawing.Point(3, 25);
            this.lblSenderUser.Name = "lblSenderUser";
            this.lblSenderUser.Size = new System.Drawing.Size(94, 13);
            this.lblSenderUser.TabIndex = 5;
            this.lblSenderUser.Text = "Sender UserName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sender Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(488, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Receiver UserName";
            // 
            // backgroundWorker_txtAreaUpdate
            // 
            this.backgroundWorker_txtAreaUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_txtAreaUpdate_DoWork);
            this.backgroundWorker_txtAreaUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_txtAreaUpdate_ProgressChanged);
            // 
            // cmbEncType
            // 
            this.cmbEncType.FormattingEnabled = true;
            this.cmbEncType.Items.AddRange(new object[] {
            "AES (256 bit key)",
            "DES (64 bit key)"});
            this.cmbEncType.Location = new System.Drawing.Point(356, 59);
            this.cmbEncType.Name = "cmbEncType";
            this.cmbEncType.Size = new System.Drawing.Size(121, 21);
            this.cmbEncType.TabIndex = 8;
            this.cmbEncType.SelectedIndexChanged += new System.EventHandler(this.cmbEncType_SelectedIndexChanged);
            // 
            // cmbBoxVersion
            // 
            this.cmbBoxVersion.FormattingEnabled = true;
            this.cmbBoxVersion.Items.AddRange(new object[] {
            "Flawed",
            "Fixed"});
            this.cmbBoxVersion.Location = new System.Drawing.Point(123, 59);
            this.cmbBoxVersion.Name = "cmbBoxVersion";
            this.cmbBoxVersion.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxVersion.TabIndex = 9;
            this.cmbBoxVersion.SelectedIndexChanged += new System.EventHandler(this.cmbBoxVersion_SelectedIndexChanged);
            // 
            // lblEncryptionType
            // 
            this.lblEncryptionType.AutoSize = true;
            this.lblEncryptionType.Location = new System.Drawing.Point(266, 67);
            this.lblEncryptionType.Name = "lblEncryptionType";
            this.lblEncryptionType.Size = new System.Drawing.Size(84, 13);
            this.lblEncryptionType.TabIndex = 10;
            this.lblEncryptionType.Text = "Encryption Type";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(3, 67);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(114, 13);
            this.lblVersion.TabIndex = 11;
            this.lblVersion.Text = "N S Symmetric Version";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.connect);
            this.panel1.Controls.Add(this.lblSenderUser);
            this.panel1.Controls.Add(this.lblEncryptionType);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.cmbEncType);
            this.panel1.Controls.Add(this.txtSenderUser);
            this.panel1.Controls.Add(this.txtRecUser);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSenderPass);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbBoxVersion);
            this.panel1.Location = new System.Drawing.Point(13, 385);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 100);
            this.panel1.TabIndex = 12;
            // 
            // Sender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 497);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtb_output);
            this.Name = "Sender";
            this.Text = "NS Symmetric Protocol ";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.RichTextBox rtb_output;
        private System.Windows.Forms.TextBox txtSenderUser;
        private System.Windows.Forms.TextBox txtRecUser;
        private System.Windows.Forms.TextBox txtSenderPass;
        private System.Windows.Forms.Label lblSenderUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker_txtAreaUpdate;
        private System.Windows.Forms.ComboBox cmbEncType;
        private System.Windows.Forms.ComboBox cmbBoxVersion;
        private System.Windows.Forms.Label lblEncryptionType;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel panel1;
    }
}

