using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmCreateNew : Form
    {
        public frmCreateNew()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmLogIn show = new frmLogIn();
            show.Show();
            this.Close();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            frmStats show = new frmStats();
            string inputUsername, inputPass, inputConfirmPass;
            string test = "^[a-zA-Z0-9 ]*$";

            inputUsername = txtUsername.Text;
            inputPass = txtPassword.Text;
            inputConfirmPass = txtConfirmPass.Text;

            if (inputUsername.Length < 5)
            {
                MessageBox.Show("Username must be longer than 5 characters!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtUsername.Focus();
                return; 
            }
                

            if (System.Text.RegularExpressions.Regex.IsMatch(inputUsername, test) == false || inputUsername.Contains(" ") == true)
            {
                MessageBox.Show("Username must not contain special characters or spaces!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtUsername.Focus();
                return;
            }

            if (Exists(inputUsername) == true)
            {
                MessageBox.Show("Sorry, username is taken!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtUsername.Focus();
                return;
            }
                 
            if (inputPass.Length < 6)
            {
                MessageBox.Show("Username must be longer than 6 characters!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                txtPassword.Focus();
                return;
            }


            if (Regex.IsMatch(inputPass, test) == false || inputPass.Contains(" ") == true)
            {
                MessageBox.Show("Password must not contain special characters or spaces!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                txtPassword.Focus();
                return;
            }

            if (inputPass != inputConfirmPass)
            {
                MessageBox.Show("Password and confirm password do not match!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConfirmPass.Text = "";
                txtPassword.Focus();
                return;
            }
            
            // format in file: username, pass, coins, numloss, numwin, weapons

            PublicVariables.player1 = new Player(); 

            PublicVariables.player1.Username = inputUsername;
            PublicVariables.player1.Password = inputPass;

            PublicVariables.player1.WriteToFile();

            MessageBox.Show("New account has been created!", "Create New", MessageBoxButtons.OK, MessageBoxIcon.Information);

            show.Show();
            this.Close();
        }

        public bool Exists(string tryUsername) 
        {
            bool exist = false;
            string line;
            string[] arayInfo;

            if (File.Exists("playerInfo.txt"))
            {
                bool flag = false;

                StreamReader inFile = new StreamReader("playerInfo.txt");


                line = inFile.ReadLine();
                if (line != null)
                {

                    flag = true;
                }

                
                inFile.Close();

                if (flag == true)
                {
                    FileEncryption.DecryptFile("playerInfo.txt", "temp.txt");
                }


                inFile = new StreamReader("temp.txt");

                while ((line = inFile.ReadLine()) != null)
                {
                    arayInfo = line.Split(',');

                    if (tryUsername.ToUpper() == arayInfo[0].ToUpper())
                        exist = true;
                }

                inFile.Close();
            }

            StreamWriter outFile = new StreamWriter("temp.txt");
            outFile.Write("");
            outFile.Close();

            return exist;
        }
    }
}
