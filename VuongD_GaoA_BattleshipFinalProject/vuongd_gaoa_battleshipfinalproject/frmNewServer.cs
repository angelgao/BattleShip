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
using System.Net.NetworkInformation;
using System.Threading;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmNewServer : Form
    {
        public frmNewServer()
        {
            InitializeComponent();
        }

        TcpListener listenForClient = new TcpListener(IPAddress.Any, 11111);
        UdpClient broadcastServer = new UdpClient();
        byte[] sendBuffer;
        byte[] receiveBuffer = new byte[8192];
        IPEndPoint ipAddress = new IPEndPoint(IPAddress.Broadcast, 11111);
        Thread startTimer,listen;
        bool isSet = false, remoteConnected = false;
        int counter;

        void StartBroadcast() 
        {
            for (counter = 0; counter < 60; counter++)
            {
                try
                {
                    broadcastServer.Send(sendBuffer, sendBuffer.Length, ipAddress);
                    Thread.Sleep(1000);
                }
                catch{}
            }
        }

        void Listen()
        {
            PublicVariables.client = new TcpClient();
            isSet = true;
            listenForClient.Start();
            PublicVariables.client = listenForClient.AcceptTcpClient();
            listenForClient.Stop();
            PublicVariables.oppIp = (IPEndPoint)(PublicVariables.client.Client.RemoteEndPoint);
            remoteConnected = true;
        }
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            btnStartServer.Enabled = false;
            btnBack.Enabled = false;

            sendBuffer = Encoding.ASCII.GetBytes(txtServerName.Text + " " + txtUserName.Text + " " + txtIPAdd.Text);
            startTimer = new Thread(StartBroadcast);
            listen = new Thread(Listen);

            broadcastServer.EnableBroadcast = true;
            startTimer.Start();

            listen.Start();

            tmrReceive.Enabled = true;
            tmrReceive.Start();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmLobby show = new frmLobby();
            if (isSet == true)
            {
                PublicVariables.client.Close();
                broadcastServer.Close();

                listen.Abort();
                startTimer.Abort();

                listenForClient.Stop();
                PublicVariables.client.Close();
            }
            show.Show();
            this.Close();
        }

        private void frmNewServer_Load(object sender, EventArgs e)
        {
            txtIPAdd.Text = PublicVariables.player1.LocalIPv4Address;
            txtUserName.Text = PublicVariables.player1.Username;
        }

        private void tmrReceive_Tick(object sender, EventArgs e)
        {
            if (counter < 60)
            {
                lblWait.Text = "Awaiting for connection...Time: " + counter.ToString() + " seconds";
                if (remoteConnected == true)
                {
                    frmDeployment show = new frmDeployment();
                    startTimer.Abort();
                    PublicVariables.player1.IsServer = true;
                    show.Show();
                    this.Close();
                }
            }
            else
            {
                btnBack.Enabled = true;
                btnStartServer.Enabled = true;
                lblWait.Text = "Connection timed out.";
                listen.Abort();
                startTimer.Abort();
                tmrReceive.Stop();
                tmrReceive.Enabled = false;
            }
        }

    }
}