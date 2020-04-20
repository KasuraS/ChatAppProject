namespace ClientGUI
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.InputPwdLogin = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.InputUserLogin = new System.Windows.Forms.RichTextBox();
            this.LinkToRegister = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // InputPwdLogin
            // 
            this.InputPwdLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InputPwdLogin.Location = new System.Drawing.Point(30, 123);
            this.InputPwdLogin.Name = "InputPwdLogin";
            this.InputPwdLogin.Size = new System.Drawing.Size(320, 26);
            this.InputPwdLogin.TabIndex = 0;
            this.InputPwdLogin.Text = "";
            this.InputPwdLogin.TextChanged += new System.EventHandler(this.InputPwdLogin_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter your username";
            // 
            // LoginButton
            // 
            this.LoginButton.Enabled = false;
            this.LoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(134, 178);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(118, 41);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(26, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter your password";
            // 
            // InputUserLogin
            // 
            this.InputUserLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputUserLogin.Location = new System.Drawing.Point(30, 51);
            this.InputUserLogin.Name = "InputUserLogin";
            this.InputUserLogin.Size = new System.Drawing.Size(320, 26);
            this.InputUserLogin.TabIndex = 4;
            this.InputUserLogin.Text = "";
            this.InputUserLogin.TextChanged += new System.EventHandler(this.InputUserLogin_TextChanged);
            // 
            // LinkToRegister
            // 
            this.LinkToRegister.AutoSize = true;
            this.LinkToRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkToRegister.Location = new System.Drawing.Point(141, 231);
            this.LinkToRegister.Name = "LinkToRegister";
            this.LinkToRegister.Size = new System.Drawing.Size(102, 15);
            this.LinkToRegister.TabIndex = 5;
            this.LinkToRegister.TabStop = true;
            this.LinkToRegister.Text = "Not registered yet";
            this.LinkToRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkToRegister_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(382, 264);
            this.Controls.Add(this.LinkToRegister);
            this.Controls.Add(this.InputUserLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputPwdLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chatbox - Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox InputPwdLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox InputUserLogin;
        private System.Windows.Forms.LinkLabel LinkToRegister;
    }
}

