namespace Spring2.Core.PostageService.StampsDemo {
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
	    this.lblUsername = new System.Windows.Forms.Label();
	    this.lblPassword = new System.Windows.Forms.Label();
	    this.txbUsername = new System.Windows.Forms.TextBox();
	    this.txbPassword = new System.Windows.Forms.TextBox();
	    this.btnAuth = new System.Windows.Forms.Button();
	    this.tbxOut = new System.Windows.Forms.TextBox();
	    this.btnGAI = new System.Windows.Forms.Button();
	    this.button1 = new System.Windows.Forms.Button();
	    this.button2 = new System.Windows.Forms.Button();
	    this.button3 = new System.Windows.Forms.Button();
	    this.pictureBox1 = new System.Windows.Forms.PictureBox();
	    this.button4 = new System.Windows.Forms.Button();
	    this.button5 = new System.Windows.Forms.Button();
	    this.button6 = new System.Windows.Forms.Button();
	    ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
	    this.SuspendLayout();
	    // 
	    // lblUsername
	    // 
	    this.lblUsername.AutoSize = true;
	    this.lblUsername.Location = new System.Drawing.Point(35, 72);
	    this.lblUsername.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
	    this.lblUsername.Name = "lblUsername";
	    this.lblUsername.Size = new System.Drawing.Size(108, 25);
	    this.lblUsername.TabIndex = 0;
	    this.lblUsername.Text = "Username:";
	    // 
	    // lblPassword
	    // 
	    this.lblPassword.AutoSize = true;
	    this.lblPassword.Location = new System.Drawing.Point(35, 120);
	    this.lblPassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
	    this.lblPassword.Name = "lblPassword";
	    this.lblPassword.Size = new System.Drawing.Size(104, 25);
	    this.lblPassword.TabIndex = 1;
	    this.lblPassword.Text = "Password:";
	    // 
	    // txbUsername
	    // 
	    this.txbUsername.Location = new System.Drawing.Point(209, 66);
	    this.txbUsername.Margin = new System.Windows.Forms.Padding(6);
	    this.txbUsername.Name = "txbUsername";
	    this.txbUsername.Size = new System.Drawing.Size(600, 29);
	    this.txbUsername.TabIndex = 2;
	    this.txbUsername.Text = "Spring2-01";
	    // 
	    // txbPassword
	    // 
	    this.txbPassword.Location = new System.Drawing.Point(209, 114);
	    this.txbPassword.Margin = new System.Windows.Forms.Padding(6);
	    this.txbPassword.Name = "txbPassword";
	    this.txbPassword.Size = new System.Drawing.Size(600, 29);
	    this.txbPassword.TabIndex = 3;
	    this.txbPassword.Text = "postage12";
	    this.txbPassword.TextChanged += new System.EventHandler(this.txbPassword_TextChanged);
	    // 
	    // btnAuth
	    // 
	    this.btnAuth.Location = new System.Drawing.Point(830, 18);
	    this.btnAuth.Margin = new System.Windows.Forms.Padding(6);
	    this.btnAuth.Name = "btnAuth";
	    this.btnAuth.Size = new System.Drawing.Size(91, 132);
	    this.btnAuth.TabIndex = 4;
	    this.btnAuth.Text = "AuthenticateUser";
	    this.btnAuth.UseVisualStyleBackColor = true;
	    this.btnAuth.Click += new System.EventHandler(this.button1_Click);
	    // 
	    // tbxOut
	    // 
	    this.tbxOut.Location = new System.Drawing.Point(40, 192);
	    this.tbxOut.Margin = new System.Windows.Forms.Padding(6);
	    this.tbxOut.Multiline = true;
	    this.tbxOut.Name = "tbxOut";
	    this.tbxOut.ReadOnly = true;
	    this.tbxOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
	    this.tbxOut.Size = new System.Drawing.Size(1368, 306);
	    this.tbxOut.TabIndex = 5;
	    // 
	    // btnGAI
	    // 
	    this.btnGAI.Location = new System.Drawing.Point(931, 18);
	    this.btnGAI.Margin = new System.Windows.Forms.Padding(6);
	    this.btnGAI.Name = "btnGAI";
	    this.btnGAI.Size = new System.Drawing.Size(81, 132);
	    this.btnGAI.TabIndex = 6;
	    this.btnGAI.Text = "GetAccountInfo";
	    this.btnGAI.UseVisualStyleBackColor = true;
	    this.btnGAI.Click += new System.EventHandler(this.btnGAI_Click);
	    // 
	    // button1
	    // 
	    this.button1.Location = new System.Drawing.Point(1023, 18);
	    this.button1.Margin = new System.Windows.Forms.Padding(6);
	    this.button1.Name = "button1";
	    this.button1.Size = new System.Drawing.Size(81, 132);
	    this.button1.TabIndex = 9;
	    this.button1.Text = "Verify Address";
	    this.button1.UseVisualStyleBackColor = true;
	    this.button1.Click += new System.EventHandler(this.button1_Click_1);
	    // 
	    // button2
	    // 
	    this.button2.Location = new System.Drawing.Point(1206, 18);
	    this.button2.Margin = new System.Windows.Forms.Padding(6);
	    this.button2.Name = "button2";
	    this.button2.Size = new System.Drawing.Size(81, 132);
	    this.button2.TabIndex = 10;
	    this.button2.Text = "FirstClass";
	    this.button2.UseVisualStyleBackColor = true;
	    this.button2.Click += new System.EventHandler(this.button2_Click);
	    // 
	    // button3
	    // 
	    this.button3.Location = new System.Drawing.Point(1115, 17);
	    this.button3.Margin = new System.Windows.Forms.Padding(6);
	    this.button3.Name = "button3";
	    this.button3.Size = new System.Drawing.Size(81, 132);
	    this.button3.TabIndex = 11;
	    this.button3.Text = "GetRates";
	    this.button3.UseVisualStyleBackColor = true;
	    this.button3.Click += new System.EventHandler(this.button3_Click);
	    // 
	    // pictureBox1
	    // 
	    this.pictureBox1.Location = new System.Drawing.Point(39, 541);
	    this.pictureBox1.Name = "pictureBox1";
	    this.pictureBox1.Size = new System.Drawing.Size(1369, 616);
	    this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
	    this.pictureBox1.TabIndex = 12;
	    this.pictureBox1.TabStop = false;
	    // 
	    // button4
	    // 
	    this.button4.Location = new System.Drawing.Point(1298, 17);
	    this.button4.Margin = new System.Windows.Forms.Padding(6);
	    this.button4.Name = "button4";
	    this.button4.Size = new System.Drawing.Size(81, 132);
	    this.button4.TabIndex = 13;
	    this.button4.Text = "Buy Postage";
	    this.button4.UseVisualStyleBackColor = true;
	    this.button4.Click += new System.EventHandler(this.button4_Click);
	    // 
	    // button5
	    // 
	    this.button5.Location = new System.Drawing.Point(1391, 18);
	    this.button5.Margin = new System.Windows.Forms.Padding(6);
	    this.button5.Name = "button5";
	    this.button5.Size = new System.Drawing.Size(81, 132);
	    this.button5.TabIndex = 14;
	    this.button5.Text = "Cancel Label";
	    this.button5.UseVisualStyleBackColor = true;
	    this.button5.Click += new System.EventHandler(this.button5_Click);
	    // 
	    // button6
	    // 
	    this.button6.Location = new System.Drawing.Point(1484, 18);
	    this.button6.Margin = new System.Windows.Forms.Padding(6);
	    this.button6.Name = "button6";
	    this.button6.Size = new System.Drawing.Size(81, 132);
	    this.button6.TabIndex = 15;
	    this.button6.Text = "SCAN Form";
	    this.button6.UseVisualStyleBackColor = true;
	    this.button6.Click += new System.EventHandler(this.button6_Click);
	    // 
	    // Form1
	    // 
	    this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
	    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	    this.ClientSize = new System.Drawing.Size(1576, 1195);
	    this.Controls.Add(this.button6);
	    this.Controls.Add(this.button5);
	    this.Controls.Add(this.button4);
	    this.Controls.Add(this.pictureBox1);
	    this.Controls.Add(this.button3);
	    this.Controls.Add(this.button2);
	    this.Controls.Add(this.button1);
	    this.Controls.Add(this.btnGAI);
	    this.Controls.Add(this.tbxOut);
	    this.Controls.Add(this.btnAuth);
	    this.Controls.Add(this.txbPassword);
	    this.Controls.Add(this.txbUsername);
	    this.Controls.Add(this.lblPassword);
	    this.Controls.Add(this.lblUsername);
	    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
	    this.Margin = new System.Windows.Forms.Padding(6);
	    this.MaximizeBox = false;
	    this.Name = "Form1";
	    this.Text = "SWS Tester";
	    ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
	    this.ResumeLayout(false);
	    this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txbUsername;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Button btnAuth;
        private System.Windows.Forms.TextBox tbxOut;
        private System.Windows.Forms.Button btnGAI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
	private System.Windows.Forms.Button button5;
	private System.Windows.Forms.Button button6;
    }
}

