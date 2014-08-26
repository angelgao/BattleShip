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
    public partial class frmDeployment : Form
    {
        public frmDeployment()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private PictureBox[,] battlefield = new PictureBox[10, 10];
        TcpListener listen = new TcpListener(IPAddress.Any, 11111);
        Thread startGame;
        bool start = false;

        void StartGameClient()
        {
            
            PublicVariables.client.Close();
            PublicVariables.client = new TcpClient();
            ReturnHere:
            try
            {
                PublicVariables.client.Connect(PublicVariables.oppIp);
            }
            catch
            {
                goto ReturnHere;
            }
            start = true;
        }

        void StartGameServer()
        {
            PublicVariables.client.Close();
            PublicVariables.client = new TcpClient();
            listen.Start();
            PublicVariables.client = listen.AcceptTcpClient();
            start = true;
        }

        private void frmDeployment_Load(object sender, EventArgs e)
        {
            int intX = 60;
            int intY = 110;

            // generate 10 x 10 grid
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    battlefield[i, j] = new PictureBox();
                    battlefield[i, j].Location = new Point(intX, intY);
                    battlefield[i, j].BackColor = System.Drawing.Color.Transparent;
                    battlefield[i, j].BorderStyle = BorderStyle.Fixed3D;
                    battlefield[i, j].Size = new Size(60, 60);
                    this.Controls.Add(battlefield[i, j]);
                    intX += 60;
                }
                intX = 60;
                intY += 60;
            }
        }

        //manipulation of picture boxes
        private PictureBox movePic;
        private PictureBox selectedPic;

        private int xOffset, yOffset;

        private void picShip_MouseDown(object sender, MouseEventArgs e)
        {
            int xPos, yPos;
            movePic = (PictureBox)sender;

            movePic.BringToFront();

            xPos = movePic.Location.X;
            yPos = movePic.Location.Y;

            if (PointToClient(System.Windows.Forms.Cursor.Position).X > xPos && PointToClient(System.Windows.Forms.Cursor.Position).X < xPos + movePic.Width && PointToClient(System.Windows.Forms.Cursor.Position).Y > yPos && PointToClient(System.Windows.Forms.Cursor.Position).Y < yPos + movePic.Height)
            {
                xOffset = PointToClient(System.Windows.Forms.Cursor.Position).X - xPos;
                yOffset = PointToClient(System.Windows.Forms.Cursor.Position).Y - yPos;
                tmrMove.Start();
            }
        }

        private void tmrMove_Tick(object sender, EventArgs e)
        {
            movePicBox();
        }

        private void movePicBox()
        {
            int width;
            Point newLocation = new Point();
            width = movePic.Size.Width;
            newLocation.X = PointToClient(System.Windows.Forms.Cursor.Position).X - xOffset;
            newLocation.Y = PointToClient(System.Windows.Forms.Cursor.Position).Y - yOffset;

            if (newLocation.X < 0)
                newLocation.X = 0;
            if (newLocation.X > 1116 - width)
                newLocation.X = 1116 - width;
            if (newLocation.Y < 0)
                newLocation.Y = 0;
            if (newLocation.Y > 659)
                newLocation.Y = 659;

            movePic.Location = newLocation;

            canRotate = false;
        }

        private bool canRotate = false;
        private bool[,] gridTaken = new bool[10, 10];

        private void picShip_MouseUp(object sender, MouseEventArgs e)
        {
            int width, height;
            int num;
            width = movePic.Size.Width;
            height = movePic.Size.Height;
            tmrMove.Stop();

            // Snap to grid

            decimal leftUpPosX, leftUpPosY;
            int newX, newY;

            leftUpPosX = movePic.Location.X - 60;
            leftUpPosY = movePic.Location.Y - 110;

            newX = (int)(Math.Round(leftUpPosX / 60)) * 60;
            newY = (int)(Math.Round(leftUpPosY / 60)) * 60;

            movePic.Location = new Point(newX + 60, newY + 110);

            // Move the image back to its initial pos if it is outside of grid
            if (movePic.Location.X < 60 || movePic.Location.X > 660 - width || movePic.Location.Y < 110 || movePic.Location.Y > 710 - height)
            {
                string[] intPos;
                intPos = movePic.Tag.ToString().Split(',');
                movePic.Location = new Point(int.Parse(intPos[0]), int.Parse(intPos[1]));

                if (width == 60)
                {
                    movePic.Width = height;
                    movePic.Height = width;

                    num = height / 60;

                    switch (num)
                    {
                        case 1:
                            movePic.Image = Properties.Resources.ship1;
                            break;
                        case 2:
                            movePic.Image = Properties.Resources.ship2;
                            break;
                        case 3:
                            movePic.Image = Properties.Resources.ship3;
                            break;
                        case 4:
                            movePic.Image = Properties.Resources.ship4;
                            break;
                        case 5:
                            movePic.Image = Properties.Resources.ship5;
                            break;
                        case 6:
                            movePic.Image = Properties.Resources.ship6;
                            break;
                    }
                    canRotate = false;
                }
                return;
            }
            else
                canRotate = true;
        }


        private void picShip_Click(object sender, EventArgs e)
        {
            if (selectedPic != null)
                selectedPic.BorderStyle = BorderStyle.FixedSingle;

            selectedPic = (PictureBox)sender;
            selectedPic.BorderStyle = BorderStyle.Fixed3D;

            canRotate = false;
        }

        private void frmDeployment_KeyDown(object sender, KeyEventArgs e)
        {
            int oldWidth, oldHeight;

            //rotate image
            if (canRotate == true)
            {
                if (selectedPic != null)
                {

                    if (e.KeyCode == Keys.D)
                    {
                        selectedPic.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        oldHeight = selectedPic.Height;
                        oldWidth = selectedPic.Width;

                        selectedPic.Height = oldWidth;
                        selectedPic.Width = oldHeight;
                    }

                    if (e.KeyCode == Keys.A)
                    {
                        selectedPic.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        oldHeight = selectedPic.Height;
                        oldWidth = selectedPic.Width;

                        selectedPic.Height = oldWidth;
                        selectedPic.Width = oldHeight;
                    }
                }
                else
                {
                    MessageBox.Show("No ship is selected for rotation!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDeployShip.Enabled = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("No rotation outside of grid!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDeployShip.Enabled = true;
                return;
            }


            // if rotation is outside of grid after

            int width = selectedPic.Width;
            int height = selectedPic.Height;

            if (selectedPic.Location.X < 60 || selectedPic.Location.X > 660 - selectedPic.Width || selectedPic.Location.Y < 110 || selectedPic.Location.Y > 710 - selectedPic.Height)
            {
                string[] intPos;
                intPos = movePic.Tag.ToString().Split(',');
                movePic.Location = new Point(int.Parse(intPos[0]), int.Parse(intPos[1]));

                if (width == 60)
                {
                    movePic.Width = height;
                    movePic.Height = width;

                    switch (height / 60)
                    {
                        case 1:
                            movePic.Image = Properties.Resources.ship1;
                            break;
                        case 2:
                            movePic.Image = Properties.Resources.ship2;
                            break;
                        case 3:
                            movePic.Image = Properties.Resources.ship3;
                            break;
                        case 4:
                            movePic.Image = Properties.Resources.ship4;
                            break;
                        case 5:
                            movePic.Image = Properties.Resources.ship5;
                            break;
                        case 6:
                            movePic.Image = Properties.Resources.ship6;
                            break;
                    }
                    canRotate = false;
                }
                return;
            }


        }

        private void frmDeployment_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                case Keys.A:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void btnDeployShip_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure this is your final setup? (NOTE: YOU WILL NOT BE ABLE TO CHANGE ANYTHING AFTERWARDS!)", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                // check if on top of each other
                btnDeployShip.Enabled = false;
                picMessage.Image = Properties.Resources.messageface;
                PictureBox[] ships = new PictureBox[6];
                bool exitCode = false;
                decimal Xpos, Ypos;
                int arayX, arayY;

                ships[0] = picShip1;
                ships[1] = picShip2;
                ships[2] = picShip3;
                ships[3] = picShip4;
                ships[4] = picShip5;
                ships[5] = picShip6;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        gridTaken[i, j] = false;

                    }
                }

                // change to true if the space is taken
                for (int count = 0; count < 6; count++)
                {
                    Xpos = ships[count].Location.X - 60;
                    Ypos = ships[count].Location.Y - 110;

                    arayX = (int)(Math.Round(Xpos / 60));
                    arayY = (int)(Math.Round(Ypos / 60));

                    if (arayX >= 0 && arayX <= 10 && arayY >= 0 && arayY <= 10)
                    {
                        for (int i = 0; i < ships[count].Size.Width / 60; i++)
                        {
                            gridTaken[arayY, arayX] = true;

                            arayX += 1;
                        }


                        arayX = (int)(Math.Round(Xpos / 60));
                        arayY = (int)(Math.Round(Ypos / 60));

                        for (int i = 0; i < ships[count].Size.Height / 60; i++)
                        {
                            gridTaken[arayY, arayX] = true;

                            arayY += 1;
                        }
                    }
                }

                // count if num of squares taken is right
                int num = 0;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (gridTaken[i, j] == true)
                            num += 1;
                    }
                }

                if (num != 21)
                {
                    for (int count = 4; count >= 0; count--)
                    {
                        ships[count].BringToFront();

                    }
                    MessageBox.Show("All ships must be placed on the grid! \n No ships can overlap!", "Deployment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDeployShip.Enabled = true;
                    exitCode = true;
                }

                if (exitCode == true)
                    return;
                else
                {
                    // put into array the position of the ships

                    string orientation;

                    string[] position = new string[6];

                    for (int count = 0; count < 6; count++)
                    {
                        Xpos = ships[count].Location.X - 60;
                        Ypos = ships[count].Location.Y - 110;

                        if (ships[count].Size.Width == 60)
                        {
                            orientation = "v";
                        }
                        else
                        {
                            orientation = "h";
                        }

                        Xpos = Xpos / 60;
                        Ypos = Ypos / 60;

                        PublicVariables.coordinates[count] = Xpos + "," + Ypos + "," + orientation + " ";
                    }

                    lblWait.Visible = true;

                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 10; j++)
                            battlefield[i, j].Enabled = false;

                    picShip1.Enabled = false;
                    picShip2.Enabled = false;
                    picShip3.Enabled = false;
                    picShip4.Enabled = false;
                    picShip5.Enabled = false;
                    picShip6.Enabled = false;
                    if (PublicVariables.player1.IsServer == false)
                    {
                        startGame = new Thread(StartGameClient);
                        startGame.Start();
                    }
                    else
                    {
                        startGame = new Thread(StartGameServer);
                        startGame.Start();
                    }

                    tmrCheck.Enabled = true;
                    tmrCheck.Start();
                }
            }
        }

        private void tmrCheck_Tick(object sender, EventArgs e)
        {
            frmBattlefield show = new frmBattlefield();

            if (start == true)
            {
                tmrCheck.Stop();
                tmrCheck.Enabled = false;
                startGame.Abort();
                show.Show();
                this.Close();
            }
        }

    }
}