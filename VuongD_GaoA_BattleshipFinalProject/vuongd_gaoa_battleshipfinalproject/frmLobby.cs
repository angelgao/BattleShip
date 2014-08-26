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
using System.Diagnostics;
using System.Threading;

namespace VuongD_GaoA_BattleshipFinalProject
{
    public partial class frmLobby : Form
    {
        public frmLobby()
        {
            InitializeComponent();
        }
        
        IPEndPoint address = new IPEndPoint(IPAddress.Any, 11111);
        UdpClient searchServer;
        byte[] receiveBuffer;
        string[] serverData = new string[1];
        int count = 0;
        string selectedServer;
        bool dataReceieved = false;
        Thread checkReceieve;

        private void frmLobby_Load(object sender, EventArgs e)
        {
            searchServer = new UdpClient(address);
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                if (ni.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    foreach (UnicastIPAddressInformation i in ni.GetIPProperties().UnicastAddresses)
                        if (i.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            PublicVariables.player1.LocalIPv4Address = i.Address.ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmStats show = new frmStats();
            searchServer.Close();

            show.Show();
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmNewServer show = new frmNewServer();

            searchServer.Close();

            show.Show();
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            searchServer.EnableBroadcast = true;
            btnJoin.Enabled = true;
            checkReceieve = new Thread(ReceiveData);
            
            checkReceieve.Start();

            btnRefresh.Enabled = false;
            tmrCheck.Enabled = true;
            tmrCheck.Start();
        }

        void ReceiveData()
        {
            try
            {
                receiveBuffer = searchServer.Receive(ref address);
                dataReceieved = true;
            }
            catch { }
        }

        private void tmrCheck_Tick(object sender, EventArgs e)
        {
            string data;
            bool checkForDuplicates = false;
            string[] output;
            ListViewItem print;

            tmrCheck.Stop();
            tmrCheck.Enabled = false;

            if (dataReceieved == true)
            {
                data = Encoding.ASCII.GetString(receiveBuffer);

                for (int i = 0; i < serverData.Length; i++)
                    if (data == serverData[i])
                    {
                        checkForDuplicates = true;
                        break;
                    }

                if (checkForDuplicates == false)
                {
                    serverData[count] = data;
                    count++;
                    Array.Resize(ref serverData, count + 1);

                    output = data.Split(' ');
                    print = new ListViewItem(output);

                    if (lsvPrint.InvokeRequired)
                        lsvPrint.BeginInvoke(new MethodInvoker(() => lsvPrint.Items.Add(print)));
                    else
                        lsvPrint.Items.Add(print);

                    checkForDuplicates = false;
                }
                dataReceieved = false;
                checkReceieve.Abort();
                checkReceieve = new Thread(ReceiveData);
                checkReceieve.Start();
                
            }

            tmrCheck.Enabled = true;
            tmrCheck.Start();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {

            PublicVariables.client = new TcpClient();
            PublicVariables.client.Connect(PublicVariables.oppIp);

            searchServer.Close();
            frmDeployment show = new frmDeployment();

            show.Show();
            this.Close();
        }

        private void lsvPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedServer = lsvPrint.SelectedItems[0].SubItems[2].Text;
            PublicVariables.oppIp = new IPEndPoint(IPAddress.Parse(selectedServer), 11111);
            PublicVariables.oppUsername = lsvPrint.SelectedItems[0].SubItems[1].Text;
        }

    }
}