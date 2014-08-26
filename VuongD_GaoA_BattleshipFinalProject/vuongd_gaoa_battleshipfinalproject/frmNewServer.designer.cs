namespace VuongD_GaoA_BattleshipFinalProject
{
    partial class frmNewServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblIPAdd = new System.Windows.Forms.Label();
            this.txtIPAdd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblServerName = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.tmrReceive = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblIPAdd
            // 
            this.lblIPAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIPAdd.ForeColor = System.Drawing.Color.White;
            this.lblIPAdd.Location = new System.Drawing.Point(228, 9);
            this.lblIPAdd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblIPAdd.Name = "lblIPAdd";
            this.lblIPAdd.Size = new System.Drawing.Size(123, 27);
            this.lblIPAdd.TabIndex = 0;
            this.lblIPAdd.Text = "IP Address:";
            this.lblIPAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIPAdd
            // 
            this.txtIPAdd.BackColor = System.Drawing.Color.Black;
            this.txtIPAdd.Enabled = false;
            this.txtIPAdd.ForeColor = System.Drawing.Color.White;
            this.txtIPAdd.Location = new System.Drawing.Point(360, 9);
            this.txtIPAdd.Name = "txtIPAdd";
            this.txtIPAdd.Size = new System.Drawing.Size(195, 27);
            this.txtIPAdd.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.Black;
            this.txtUserName.Enabled = false;
            this.txtUserName.ForeColor = System.Drawing.Color.White;
            this.txtUserName.Location = new System.Drawing.Point(360, 45);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(195, 27);
            this.txtUserName.TabIndex = 3;
            // 
            // lblUserName
            // 
            this.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(228, 45);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(123, 27);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Username:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerName
            // 
            this.txtServerName.BackColor = System.Drawing.Color.Black;
            this.txtServerName.ForeColor = System.Drawing.Color.White;
            this.txtServerName.Location = new System.Drawing.Point(360, 81);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(195, 27);
            this.txtServerName.TabIndex = 5;
            // 
            // lblServerName
            // 
            this.lblServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerName.ForeColor = System.Drawing.Color.White;
            this.lblServerName.Location = new System.Drawing.Point(212, 81);
            this.lblServerName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(139, 27);
            this.lblServerName.TabIndex = 4;
            this.lblServerName.Text = "Server Name:";
            this.lblServerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.Black;
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Location = new System.Drawing.Point(228, 123);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(156, 52);
            this.btnStartServer.TabIndex = 6;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // lblWait
            // 
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWait.ForeColor = System.Drawing.Color.Transparent;
            this.lblWait.Location = new System.Drawing.Point(228, 190);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(327, 98);
            this.lblWait.TabIndex = 7;
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Black;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(399, 123);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(156, 52);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tmrReceive
            // 
            this.tmrReceive.Tick += new System.EventHandler(this.tmrReceive_Tick);
            // 
            // frmNewServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::VuongD_GaoA_BattleshipFinalProject.Properties.Resources.bigpreview_Universal_Battleship_Twitter_Background1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(568, 303);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtIPAdd);
            this.Controls.Add(this.lblIPAdd);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "frmNewServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Server";
            this.Load += new System.EventHandler(this.frmNewServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIPAdd;
        private System.Windows.Forms.TextBox txtIPAdd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Timer tmrReceive;
    }
}