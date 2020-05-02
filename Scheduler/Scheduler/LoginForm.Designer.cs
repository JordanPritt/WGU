namespace Scheduler
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
            this.UserNameLbl = new System.Windows.Forms.Label();
            this.PasswordLbl = new System.Windows.Forms.Label();
            this.UserNameText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserNameLbl
            // 
            this.UserNameLbl.AutoSize = true;
            this.UserNameLbl.Location = new System.Drawing.Point(11, 35);
            this.UserNameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UserNameLbl.Name = "UserNameLbl";
            this.UserNameLbl.Size = new System.Drawing.Size(60, 13);
            this.UserNameLbl.TabIndex = 0;
            this.UserNameLbl.Text = "User Name";
            // 
            // PasswordLbl
            // 
            this.PasswordLbl.AutoSize = true;
            this.PasswordLbl.Location = new System.Drawing.Point(11, 75);
            this.PasswordLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PasswordLbl.Name = "PasswordLbl";
            this.PasswordLbl.Size = new System.Drawing.Size(53, 13);
            this.PasswordLbl.TabIndex = 1;
            this.PasswordLbl.Text = "Password";
            // 
            // UserNameText
            // 
            this.UserNameText.Location = new System.Drawing.Point(129, 32);
            this.UserNameText.Margin = new System.Windows.Forms.Padding(2);
            this.UserNameText.Name = "UserNameText";
            this.UserNameText.Size = new System.Drawing.Size(206, 20);
            this.UserNameText.TabIndex = 2;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(129, 72);
            this.PasswordText.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.PasswordChar = '*';
            this.PasswordText.Size = new System.Drawing.Size(206, 20);
            this.PasswordText.TabIndex = 3;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(129, 129);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(206, 33);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 173);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.UserNameText);
            this.Controls.Add(this.PasswordLbl);
            this.Controls.Add(this.UserNameLbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameLbl;
        private System.Windows.Forms.Label PasswordLbl;
        private System.Windows.Forms.TextBox UserNameText;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.Button LoginButton;
    }
}

