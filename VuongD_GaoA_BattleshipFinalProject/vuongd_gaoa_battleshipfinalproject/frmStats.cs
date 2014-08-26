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
    public partial class frmStats : Form
    {
        public frmStats()
        {
            InitializeComponent();
        }
        
        private void frmStats_Load(object sender, EventArgs e)
        {
            txtUserName.Text = PublicVariables.player1.Username;
            txtCoins.Text = PublicVariables.player1.Coins.ToString();
            txtWins.Text = PublicVariables.player1.Wins.ToString();
            txtLoss.Text = PublicVariables.player1.Losses.ToString();
            lblWeapons1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                frmLogIn show = new frmLogIn();
                show.Show();
                this.Close();
            }
        }

        private void btnLobby_Click(object sender, EventArgs e)
        {
            frmLobby show = new frmLobby();
            show.Show();
            this.Close();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            frmShop show = new frmShop();
            show.Show();
            this.Close();
        }
    }
}
