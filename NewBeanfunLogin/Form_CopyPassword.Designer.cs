namespace NewBeanfunLogin
{
    partial class Form_CopyPassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.btnCopyAcc = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCopyPwd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "帳號：";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccount.Location = new System.Drawing.Point(66, 12);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(250, 22);
            this.txtAccount.TabIndex = 1;
            // 
            // btnCopyAcc
            // 
            this.btnCopyAcc.Location = new System.Drawing.Point(322, 12);
            this.btnCopyAcc.Name = "btnCopyAcc";
            this.btnCopyAcc.Size = new System.Drawing.Size(77, 23);
            this.btnCopyAcc.TabIndex = 2;
            this.btnCopyAcc.Text = "複製";
            this.btnCopyAcc.UseVisualStyleBackColor = true;
            this.btnCopyAcc.Click += new System.EventHandler(this.btnCopyAcc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "密碼：";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(66, 41);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(250, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // btnCopyPwd
            // 
            this.btnCopyPwd.Location = new System.Drawing.Point(322, 41);
            this.btnCopyPwd.Name = "btnCopyPwd";
            this.btnCopyPwd.Size = new System.Drawing.Size(77, 23);
            this.btnCopyPwd.TabIndex = 5;
            this.btnCopyPwd.Text = "複製";
            this.btnCopyPwd.UseVisualStyleBackColor = true;
            this.btnCopyPwd.Click += new System.EventHandler(this.btnCopyPwd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(322, 70);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "關閉 (ESC)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "小提示：在楓之谷登入畫面按 Shift + Insert 可以貼上";
            // 
            // Form_CopyPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 109);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCopyPwd);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCopyAcc);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_CopyPassword";
            this.Opacity = 0.8D;
            this.Text = "複製密碼...";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_CopyPassword_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Button btnCopyAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCopyPwd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
    }
}
