/*This class Player is the main class where the player's data is stored during the game.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VuongD_GaoA_BattleshipFinalProject
{
    class Player
    {

        private string userName, password, localIp;
        private int coins, numWin, numLoss, weapon1, weapon2, weapon3;
        private bool serverStatus;
        public Player()
        {
            userName = "";
            password = "";
            weapon1 = 0;
            weapon2 = 0;
            weapon3 = 0;
            coins = 10000;
            numWin = 0;
            numLoss = 0;
        }

        //first check if exists
        public Player(string inputName)
        {
            string line;
            string[] arayInfo;

            FileEncryption.DecryptFile("playerInfo.txt", "temp.txt");

            StreamReader inFile = new StreamReader("temp.txt");
            while ((line = inFile.ReadLine()) != null)
            {
                arayInfo = line.Split(',');

                if (inputName == arayInfo[0])
                {
                    userName = arayInfo[0];
                    password = arayInfo[1];
                    coins = int.Parse(arayInfo[2]);
                    numLoss = int.Parse(arayInfo[3]);
                    numWin = int.Parse(arayInfo[4]);
                    weapon1 = int.Parse(arayInfo[5]);
                    weapon2 = int.Parse(arayInfo[6]);
                    weapon3 = int.Parse(arayInfo[7]);
                }

                serverStatus = false;
            }
            inFile.Close();

            StreamWriter outFile = new StreamWriter("temp.txt");
            outFile.Write("");
            outFile.Close();

            // format in file: username, pass, coins, numloss, numwin, weapon1, weapon2, weapon3
        }

        public void WriteToFile()
        {

            String line;
           
            FileEncryption.DecryptFile("playerInfo.txt", "temp.txt");

            StreamWriter outFile = File.AppendText("temp.txt");

            // format in file: username, pass, coins, numloss, numwin, weapons
            line = userName + "," + password + "," + coins + "," + numLoss + "," + numWin + "," + weapon1 + "," + weapon2 + "," + weapon3;

        
            outFile.WriteLine(line);

            outFile.Close();

            FileEncryption.EncryptFile("temp.txt", "playerInfo.txt");

            StreamWriter tempFile = new StreamWriter("temp.txt");
            tempFile.Write("");
            tempFile.Close();

        }

        public void UpdateFile()
        {

            string line;
            string[] arayInfo;

            //decrypt playerinfo.txt into a temp.txt file
            FileEncryption.DecryptFile("playerInfo.txt", "temp.txt");


            StreamReader inFile = new StreamReader("temp.txt");
            StreamWriter tempFile = new StreamWriter("temp2.txt");

            while ((line = inFile.ReadLine()) != null)
            {
                arayInfo = line.Split(',');

                if (userName == arayInfo[0])
                {
                    continue;
                }

                tempFile.WriteLine(line);
            }

            inFile.Close();
            tempFile.Close();

            StreamReader tempInFile = new StreamReader("temp2.txt");
            StreamWriter newFile = new StreamWriter("temp.txt");

            while ((line = tempInFile.ReadLine()) != null)
            {
                newFile.WriteLine(line);
            }

            tempInFile.Close();
            newFile.Close();

            FileEncryption.EncryptFile("temp.txt", "playerInfo.txt");

            StreamWriter tempOutFile = new StreamWriter("temp2.txt");
            tempOutFile.Write("");
            tempOutFile.Close();


            WriteToFile();

        }

        public bool CheckLogin(string name, string pass)
        {
            bool logIn = false;

            if (name == userName && pass == password)
                logIn = true;

            return logIn;

        }

        public string Username
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public int Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
            }
        }

        public int Wins
        {
            get
            {
                return numWin;
            }
            set
            {
                numWin = value;
            }
        }

        public int Losses
        {
            get
            {
                return numLoss;
            }
            set
            {
                numLoss = value;
            }
        }

        public int Weapon1
        {
            get
            {
                return weapon1;
            }
            set
            {
                weapon1 = value;
            }
        }

        public int Weapon2
        {
            get
            {
                return weapon2;
            }
            set
            {
                weapon2 = value;
            }
        }

        public int Weapon3
        {
            get
            {
                return weapon3;
            }
            set
            {
                weapon3 = value;
            }
        }

        public string LocalIPv4Address
        {
            get
            {
                return localIp;
            }
            set
            {
                localIp = value;
            }
        }

        public bool IsServer
        {
            get
            {
                return serverStatus;
            }

            set
            {
                serverStatus = value;
            }
        }
    }
}

