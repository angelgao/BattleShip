using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GaoA_frmShipDeployment
{
    public partial class frmDeployment : Form
    {
        public frmDeployment()
        {
            InitializeComponent();
        }

        private void frmDeployment_Load(object sender, EventArgs e)
        {
            PictureBox[,] battlefield = new PictureBox[10, 10];
            int intX = 60;
            int intY = 110;


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    battlefield[i, j] = new PictureBox();
                    battlefield[i, j].Location = new Point(intX, intY);
                    battlefield[i, j].BackColor = System.Drawing.Color.Transparent;
                    battlefield[i, j].BorderStyle = BorderStyle.Fixed3D;
                    battlefield[i, j].Size = new Size(40, 40);
                    this.Controls.Add(battlefield[i, j]);
                    intX += 40;
                }
                intX = 60;
                intY += 40;
            }
        }
    }
}
