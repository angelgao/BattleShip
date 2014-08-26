using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmHomePage : Form
    {
        public frmHomePage()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            frmLogIn show = new frmLogIn();
            show.Show();
            this.Hide();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BLAH");//temp code
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            frmInstructions show = new frmInstructions();
            show.Show();
            this.Hide();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreateNew show = new frmCreateNew();
            show.Show();
            this.Close();
        }
    }
}
