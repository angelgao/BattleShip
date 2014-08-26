using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string nameInput, passInput;
            frmCreateNew check = new frmCreateNew();

            nameInput = txtUsername.Text;
            passInput = txtPassword.Text;

            if (nameInput == "" && passInput == "")
            {
                MessageBox.Show("Please enter your username and password!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
            }
            else if (nameInput == "")
            {
                MessageBox.Show("Please enter your username!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
            }
            else if (passInput == "")
            {
                MessageBox.Show("Please enter your password!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
            }
            else if (check.Exists(nameInput) == true)
            {
                PublicVariables.player1 = new Player(nameInput);
                if (PublicVariables.player1.CheckLogin(nameInput, passInput) == true)
                {
                    frmStats show = new frmStats();
                    MessageBox.Show("Login Successful!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect password!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }

            }
            else
            {
                MessageBox.Show("Username does not exist. Click 'Back' to create a new account.", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

           frmCreateNew show = new frmCreateNew();

            show.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
