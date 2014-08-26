using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmVictory : Form
    {
        public frmVictory()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            frmStats show = new frmStats();

            PublicVariables.client.Close();
            PublicVariables.player1.UpdateFile();
            show.Show();
            this.Close();
        }

        private void frmVictory_Load(object sender, EventArgs e)
        {

            if (PublicVariables.gameStatus == true)
            {
                lblInfo.Text = "YOU WON!" + Environment.NewLine;
                this.Text = "VICTORY!";
            }
            else
            {
                lblInfo.Text = "You lost :(" + Environment.NewLine;
                this.Text = "You lost :(";
            }

            lblInfo.Text += "User: " + PublicVariables.player1.Username + Environment.NewLine;
            if (PublicVariables.gameStatus == false)
                lblInfo.Text += "Coins earned: 0" + Environment.NewLine;
            else
                lblInfo.Text += "Coins earned: 2000" + Environment.NewLine;
            lblInfo.Text += "Wins: " + PublicVariables.player1.Wins + Environment.NewLine;
            lblInfo.Text += "Loss: " + PublicVariables.player1.Losses + Environment.NewLine;
        }
    }
}
