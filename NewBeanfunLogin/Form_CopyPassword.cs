using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBeanfunLogin
{
    public partial class Form_CopyPassword : Form
    {
        public Form_CopyPassword(string acc, string pwd)
        {
            InitializeComponent();
            txtAccount.Text = acc;
            txtPassword.Text = pwd;
            txtAccount.GotFocus += new EventHandler(TextBox_GotFocus);
            txtPassword.GotFocus += new EventHandler(TextBox_GotFocus);
        }

        void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox Sender = sender as TextBox;
            if (Sender != null)
                Sender.SelectAll();
        }

        private void btnCopyAcc_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtAccount.Text);
        }

        private void btnCopyPwd_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPassword.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_CopyPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}

