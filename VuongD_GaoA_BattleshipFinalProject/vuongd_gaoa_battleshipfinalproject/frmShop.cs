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
    public partial class frmShop : Form
    {
        public frmShop()
        {
            InitializeComponent();
        }

        private int coins = PublicVariables.player1.Coins;

        private void frmShop_Load(object sender, EventArgs e)
        {
           lblUser.Text += PublicVariables.player1.Username;
            lblCoinsLeft.Text = coins.ToString();
            
            lblWeapons1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
        }

        private void picWeapon1_Click(object sender, EventArgs e)
        {
            const int price = 1000;

            coins = PublicVariables.player1.Coins;

            if (coins >= price)
            {
                PublicVariables.player1.Coins -= price;
                PublicVariables.player1.Weapon1 += 1;
                coins = PublicVariables.player1.Coins;
                lblCoinsLeft.Text = coins.ToString();
                PublicVariables.player1.UpdateFile();
                MessageBox.Show("One CUTLASS MISSILE bought!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Sorry, not enough coins to make that transaction!");


            lblWeapons1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();

        }

        private void picWeapon2_Click(object sender, EventArgs e)
        {
            const int price = 2000;

            coins = PublicVariables.player1.Coins;

            if (coins >= price)
            {
                PublicVariables.player1.Coins -= price;
                PublicVariables.player1.Weapon2 += 1;
                coins = PublicVariables.player1.Coins;
                lblCoinsLeft.Text = coins.ToString();
                PublicVariables.player1.UpdateFile();
                MessageBox.Show("One RAPIER MISSILE bought!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Sorry, not enough coins to make that transaction!");



            lblWeapons1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
        }

        private void picWeapon3_Click(object sender, EventArgs e)
        {
            const int price = 3000;

            coins = PublicVariables.player1.Coins;

            if (coins >= price)
            {
                PublicVariables.player1.Coins -= price;
                PublicVariables.player1.Weapon3 += 1;
                coins = PublicVariables.player1.Coins;
                lblCoinsLeft.Text = coins.ToString();
                PublicVariables.player1.UpdateFile();
                MessageBox.Show("One RAPIER CANNON bought!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Sorry, not enough coins to make that transaction!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Error);


            lblWeapons1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
        
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmStats show = new frmStats();

            show.Show();
            this.Close();

            PublicVariables.player1.UpdateFile();
        }


    }
}
