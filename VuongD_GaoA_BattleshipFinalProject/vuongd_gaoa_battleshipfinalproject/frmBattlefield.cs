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
using System.Diagnostics;
using System.Threading;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmBattlefield : Form
    {
        public frmBattlefield()
        {
            InitializeComponent();
        }
        byte[] receiveBuffer = new byte[1024];
        byte[] sendBuffer;
        bool[] specWeap = new bool[3];
        PictureBox selectedImg;
        PictureBox[,] battlefield1 = new PictureBox[10, 10];
        PictureBox[,] battlefield2 = new PictureBox[10, 10];
        int oppShipsDestroyed = 0, myShipsDestroyed = 0;
        Thread receiveBattlefieldData, checkGameStatus;
        NetworkStream netRead, netWrite;
        PictureBox[] shipImg = new PictureBox[6];
        string coordToSend;
        bool over = false;

        void RecieveBattlefieldData()
        {
            string battlefieldData;
            string[] temp;
            byte[] formatted;
            int bytesRead;

            netRead = PublicVariables.client.GetStream();
            bytesRead = netRead.Read(receiveBuffer, 0, receiveBuffer.Length);
            formatted = new byte[bytesRead];
            for (int i = 0; i < bytesRead; i++)
                formatted[i] = receiveBuffer[i];

            battlefieldData = Encoding.ASCII.GetString(formatted);

            temp = battlefieldData.Split(' ');
            PublicVariables.oppCoordinates = new string[temp.Length];
            PublicVariables.oppCoordinates = temp;
        }

        void CheckForGameStatus()
        {
            byte[] formatted;
            int bytesRead;
            string[] remoteData, coord;

            bytesRead = netRead.Read(receiveBuffer, 0, receiveBuffer.Length);

            formatted = new byte[bytesRead];
            for (int z = 0; z < bytesRead; z++)
                formatted[z] = receiveBuffer[z];

            remoteData = Encoding.ASCII.GetString(formatted).Split(' ');

            for (int q = 0; q < remoteData.Length; q++)
            {
                coord = remoteData[q].Split(',');
                if (coord[0] == "YOURTURN")
                    if (myShipsDestroyed < 21)
                    {
                        string[] canEnable;
                        for (int i = 0; i < 10; i++)
                            for (int j = 0; j < 10; j++)
                            {
                                canEnable = (battlefield2[i, j].Tag.ToString()).Split(',');
                                if (canEnable[2] == "F")
                                    Invoke(new Action(() => battlefield2[i, j].Enabled = true));
                            }

                        Invoke(new Action(() => picWeapon1.Enabled = true));
                        Invoke(new Action(() => picWeapon2.Enabled = true));
                        Invoke(new Action(() => picWeapon3.Enabled = true));
                        Invoke(new Action(() => lblMessage.Text = "Your turn!"));
                    }
                    else
                        over = true;
                else
                {
                    if (coord[2] == "T")
                    {
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].BackColor = Color.Red));
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].Parent.Controls.SetChildIndex(battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])], 0)));
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].BringToFront()));
                        myShipsDestroyed++;
                    }
                    else
                    {
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].BackColor = Color.Blue));
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].Parent.Controls.SetChildIndex(battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])], 0)));
                        Invoke(new Action(() => battlefield1[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])].BringToFront()));
                    }
                }
            }
        }

        private void frmBattlefield_Load(object sender, EventArgs e)
        {

            int intX = 70;
            int intY = 208;
            int count = 0;
            string[] temp;
            Point movePictureBox;
            string sendCoord = "", currentCoord = "";

            receiveBattlefieldData = new Thread(RecieveBattlefieldData);
            receiveBattlefieldData.Start();
            //array assignments
            shipImg[0] = picShip1;
            shipImg[1] = picShip2;
            shipImg[2] = picShip3;
            shipImg[3] = picShip4;
            shipImg[4] = picShip5;
            shipImg[5] = picShip6;

            for (int i = 0; i < shipImg.Length; i++)
                shipImg[i].SendToBack();

            for (int i = 0; i > specWeap.Length; i++)
                specWeap[i] = false;

            //displaying coordinate grid for player
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    battlefield1[i, j] = new PictureBox();
                    battlefield1[i, j].Location = new Point(intX, intY);
                    battlefield1[i, j].BackColor = System.Drawing.Color.Transparent;
                    battlefield1[i, j].BorderStyle = BorderStyle.Fixed3D;
                    battlefield1[i, j].Size = new Size(50, 50);
                    battlefield1[i, j].Tag = j.ToString() + "," + i.ToString();
                    this.Controls.Add(battlefield1[i, j]);
                    intX += 50;
                }
                intX = 70;
                intY += 50;
            }

            intX = 700;
            intY = 208;

            //displaying coordinate grid for opponent
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    battlefield2[i, j] = new PictureBox();
                    battlefield2[i, j].Location = new Point(intX, intY);
                    battlefield2[i, j].BackColor = System.Drawing.Color.Transparent;
                    battlefield2[i, j].BorderStyle = BorderStyle.Fixed3D;
                    battlefield2[i, j].Size = new Size(50, 50);
                    battlefield2[i, j].Tag = j.ToString() + "," + i.ToString() + ",F";
                    battlefield2[i, j].Click += new EventHandler(OpponentBattleField_Click);
                    this.Controls.Add(battlefield2[i, j]);
                    intX += 50;
                    if (PublicVariables.player1.IsServer == false)
                        battlefield2[i, j].Enabled = false;
                }
                intX = 700;
                intY += 50;
            }
            intX = 70;
            intY = 208;

            //coordinates: temp name; rename after implementing angels code
            //taking the data from PublicVariables.coordinates and moving ships onto grid
            for (int i = 0; i < PublicVariables.coordinates.Length; i++)
            {
                intX = 70;
                intY = 208;
                temp = PublicVariables.coordinates[i].Split(',');

                intX = int.Parse(temp[0]);
                intY = int.Parse(temp[1]);

                intX = intX * 50 + 70;
                intY = intY * 50 + 208;

                movePictureBox = new Point(intX, intY);
                shipImg[count].Location = movePictureBox;
                if (temp[2] == "v ")
                {
                    int oldHeight = shipImg[count].Height;
                    int oldWidth = shipImg[count].Width;

                    shipImg[count].Height = oldWidth;
                    shipImg[count].Width = oldHeight;

                    shipImg[count].Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                }

                switch (shipImg[count].Tag.ToString())
                {
                    case "2": currentCoord = AddOneToCoordinate(1, temp); break;
                    case "3": currentCoord = AddOneToCoordinate(2, temp); break;
                    case "4": currentCoord = AddOneToCoordinate(3, temp); break;
                    case "5": currentCoord = AddOneToCoordinate(4, temp); break;
                    case "6": currentCoord = AddOneToCoordinate(5, temp); break;
                    default: currentCoord = AddOneToCoordinate(0, temp); break;
                }
                sendCoord += currentCoord;
                currentCoord = "";
                count++;
            }

            //updating weapons purchased earlier by the player
            lblWeapon1.Text = PublicVariables.player1.Weapon1.ToString();
            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();

            if (PublicVariables.player1.Weapon1 == 0)
                picWeapon1.Enabled = false;
            else if (PublicVariables.player1.Weapon2 == 0)
                picWeapon2.Enabled = false;
            else if (PublicVariables.player1.Weapon3 == 0)
                picWeapon3.Enabled = false;

            netWrite = PublicVariables.client.GetStream();
            sendBuffer = Encoding.ASCII.GetBytes(sendCoord);
            netWrite.Write(sendBuffer, 0, sendBuffer.Length);

            tmrMoveOn.Enabled = true;
            tmrMoveOn.Start();

            if (PublicVariables.player1.IsServer == true)
            {
                picMessage.Image = Properties.Resources.messageface;
                lblMessage.Text = "Your turn!";
                EnableAll();
            }
            else
            {
                picMessage.Image = Properties.Resources.messageface;
                lblMessage.Text = "Opponent's turn...";

                DisableAll();
                checkGameStatus = new Thread(CheckForGameStatus);
                checkGameStatus.Start();
            }

        }

        void DisableAll()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    battlefield2[i, j].Enabled = false;

            picWeapon1.Enabled = false;
            picWeapon2.Enabled = false;
            picWeapon3.Enabled = false;
        }

        void EnableAll()
        {
            string[] canEnable;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    canEnable = (battlefield2[i, j].Tag.ToString()).Split(',');
                    if (canEnable[2] == "F")
                        battlefield2[i, j].Enabled = true;
                }

            Invoke(new Action(() => picWeapon1.Enabled = true));
            Invoke(new Action(() => picWeapon2.Enabled = true));
            Invoke(new Action(() => picWeapon3.Enabled = true));
        }

        string AddOneToCoordinate(int picNum, string[] coordinate)
        {
            string returnData = coordinate[0] + "," + coordinate[1] + " ";

            if (coordinate[2] == "v ")
                for (int i = 0; i < picNum; i++)
                {
                    coordinate[1] = (Convert.ToInt32(coordinate[1]) + 1).ToString();
                    returnData += coordinate[0] + "," + coordinate[1] + " ";
                }
            else
                for (int i = 0; i < picNum; i++)
                {
                    coordinate[0] = (Convert.ToInt32(coordinate[0]) + 1).ToString();
                    returnData += coordinate[0] + "," + coordinate[1] + " ";
                }

            return returnData;
        }

        //if a special weapon is chosen to be a primary weapon of choice, it will update the weapon array
        private void SpecialWeapon_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            switch (pic.Tag.ToString())
            {
                case "1":
                    {
                        if (PublicVariables.player1.Weapon1 > 0)
                        {
                            specWeap[0] = true; specWeap[1] = false; specWeap[2] = false;
                        }
                        else
                            MessageBox.Show("You do not have this weapon!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                case "2":
                    {
                        if (PublicVariables.player1.Weapon2 > 0)
                        {
                            specWeap[0] = false; specWeap[1] = true; specWeap[2] = false;
                        }
                        else
                            MessageBox.Show("You do not have this weapon!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                    }
                case "3":
                    {
                        if (PublicVariables.player1.Weapon3 > 0)
                        {
                            specWeap[0] = false; specWeap[1] = false; specWeap[2] = true;
                        }
                        else
                            MessageBox.Show("You do not have this weapon!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                default: specWeap[0] = false; specWeap[1] = false; specWeap[2] = false; break;
            }
        }

        //when it is the users turn, the user can select a picturebox on the opponent battlefield
        //it will then check whether or not a ship is placed.
        void OpponentBattleField_Click(object sender, EventArgs e)
        {
            string[] selectedCoord, temp;
            int weaponNum = 0;

            selectedImg = (PictureBox)sender;

            if (CheckForShip(selectedImg.Tag.ToString()) == true)
            {

                selectedImg.Image = Properties.Resources.taken;
                for (int j = 0; j < specWeap.Length; j++)
                    if (specWeap[j] == true)
                        weaponNum = j + 1;

                selectedCoord = selectedImg.Tag.ToString().Split(',');
                selectedCoord[2] = "T";
                selectedImg.Tag = selectedCoord[0] + "," + selectedCoord[1] + "," + selectedCoord[2];
                coordToSend = selectedImg.Tag.ToString() + " ";

                switch (weaponNum.ToString())
                {
                    case "1":
                        {
                            DisplayChosenCoord(1, selectedCoord);
                            PublicVariables.player1.Weapon1 -= 1;
                            lblWeapon1.Text = PublicVariables.player1.Weapon1.ToString();
                            oppShipsDestroyed++;
                            break;
                        }
                    case "2":
                        {
                            DisplayChosenCoord(2, selectedCoord);
                            PublicVariables.player1.Weapon2 -= 1;
                            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
                            oppShipsDestroyed++;
                            break;
                        }
                    case "3":
                        {
                            DisplayChosenCoord(3, selectedCoord);
                            PublicVariables.player1.Weapon3 -= 1;
                            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
                            oppShipsDestroyed++;
                            break;
                        }
                    default:
                        {
                            oppShipsDestroyed++;
                            break;
                        }
                }
            }
            else
            {
                temp = selectedImg.Tag.ToString().Split(',');
                battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].BackColor = Color.Blue;
                battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Enabled = false;

                for (int j = 0; j < specWeap.Length; j++)
                    if (specWeap[j] == true)
                        weaponNum = j + 1;

                selectedCoord = selectedImg.Tag.ToString().Split(',');
                selectedCoord[2] = "F";
                selectedImg.Tag = selectedCoord[0] + "," + selectedCoord[1] + "," + selectedCoord[2];
                coordToSend = selectedImg.Tag.ToString() + " ";

                switch (weaponNum.ToString())
                {
                    case "1":
                        {
                            DisplayChosenCoord(1, selectedCoord);
                            PublicVariables.player1.Weapon1 -= 1;
                            lblWeapon1.Text = PublicVariables.player1.Weapon1.ToString();
                            break;
                        }
                    case "2":
                        {
                            DisplayChosenCoord(2, selectedCoord);
                            PublicVariables.player1.Weapon2 -= 1;
                            lblWeapon2.Text = PublicVariables.player1.Weapon2.ToString();
                            break;
                        }
                    case "3":
                        {
                            DisplayChosenCoord(3, selectedCoord);
                            PublicVariables.player1.Weapon3 -= 1;
                            lblWeapon3.Text = PublicVariables.player1.Weapon3.ToString();
                            break;
                        }
                }
            }

            coordToSend += "YOURTURN";
            sendBuffer = Encoding.ASCII.GetBytes(coordToSend);
            netWrite.Write(sendBuffer, 0, sendBuffer.Length);
            Thread.Sleep(100);

            if (oppShipsDestroyed == 21)
            {
                frmVictory show = new frmVictory();
                netRead.Close();
                netWrite.Close();
                checkGameStatus.Abort();
                receiveBattlefieldData.Abort();
                tmrMoveOn.Stop();
                PublicVariables.player1.Wins += 1;
                PublicVariables.player1.Coins += 2000;//to be negotiated
                PublicVariables.gameStatus = true;
                show.Show();
                this.Close();
            }
            else
            {
                for (int i = 0; i < specWeap.Length; i++)
                    specWeap[i] = false;

                lblMessage.Text = "Opponent's turn...";
                DisableAll();
                checkGameStatus = new Thread(CheckForGameStatus);
                checkGameStatus.Start();
            }

        }
        //this method determines if the coordinate contains a ship placed on top of it
        bool CheckForShip(string coord)
        {
            bool returnVal = false;
            string[] data = coord.Split(',');
            string coordinate = data[0] + "," + data[1];
            for (int z = 0; z < PublicVariables.oppCoordinates.Length; z++)
                if (coordinate == PublicVariables.oppCoordinates[z])
                {
                    returnVal = true;
                    break;
                }
            return returnVal;
        }

        //this method displays whether there is a ship placed on top of the coordinate
        void DisplayChosenCoord(int weapon, string[] temp)
        {
            Random rand = new Random();
            int generateRandomNum = rand.Next(1, 3);

            for (int q = 0; q < weapon; q++)
            {
                if (Convert.ToInt32(temp[0]) < 9 && Convert.ToInt32(temp[0]) > 0)
                    if (generateRandomNum == 1)
                        temp[0] = (Convert.ToInt32(temp[0]) + 1).ToString();
                    else
                        temp[0] = (Convert.ToInt32(temp[0]) - 1).ToString();
                else if (Convert.ToInt32(temp[1]) < 9 && Convert.ToInt32(temp[1]) > 0)
                    if (generateRandomNum == 1)
                        temp[1] = (Convert.ToInt32(temp[1]) + 1).ToString();
                    else
                        temp[1] = (Convert.ToInt32(temp[1]) - 1).ToString();
                else if (Convert.ToInt32(temp[0]) == 9)
                    temp[0] = (Convert.ToInt32(temp[0]) - 1).ToString();
                else if (Convert.ToInt32(temp[0]) == 0)
                    temp[0] = (Convert.ToInt32(temp[0]) + 1).ToString();
                else if (Convert.ToInt32(temp[1]) == 9)
                    temp[1] = (Convert.ToInt32(temp[1]) - 1).ToString();
                else
                    temp[1] = (Convert.ToInt32(temp[1]) + 1).ToString();

                if (CheckForShip(temp[0] + "," + temp[1]) == true)
                {
                    battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Image = Properties.Resources.taken;
                    battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Tag = temp[0] + "," + temp[1] + ",T";
                    coordToSend += battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Tag.ToString() + " ";
                    oppShipsDestroyed++;
                }
                else
                {
                    battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].BackColor = Color.Blue;
                    battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Tag = temp[0] + "," + temp[1] + ",F";
                    coordToSend += battlefield2[Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])].Tag.ToString() + " ";
                }
            }

        }

        private void tmrMoveOn_Tick(object sender, EventArgs e)
        {
            if (over == true)
            {
                netWrite.Close();
                netRead.Close();
                checkGameStatus.Abort();
                receiveBattlefieldData.Abort();

                frmVictory show = new frmVictory();
                PublicVariables.player1.Losses += 1;
                show.Show();
                PublicVariables.gameStatus = false;
                Invoke(new Action(() => this.Close()));
            }
        }
    }
}