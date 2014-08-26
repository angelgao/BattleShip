/******************************************************************
PROGRAMME	:	VuongD_GaoA_BattleshipFinalProject
  
OUTLINE		:	This programme replicates the board game
                "Battleship". The main goal of the game is
                to destroy the opponents ship before they
                destroy yours. Players will play over a
                network, and will also have special weapons
                if they purchase them from the shop.
                If player wins games, they will earn
                coins to purchase more weapons.

PROGRAMMER	:	David Vuong & Angel Gao

DATE		:	June 7, 2013
******************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            prgLoad.Maximum = 100;
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(PublicVariables.secretKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(PublicVariables.secretKey);
            PublicVariables.desKey = DES.Key;
            PublicVariables.desIV = DES.IV;
            tmrLoad.Start();
        }

        private void tmrLoad_Tick(object sender, EventArgs e)
        {
            frmLogIn show = new frmLogIn();

            if (prgLoad.Value < 100)
                prgLoad.Value++;
            else
            {
                tmrLoad.Stop();

                show.Show();
                this.Hide();
            }
        }
    }
}
