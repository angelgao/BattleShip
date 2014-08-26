namespace GaoA_frmShipDeployment
{
    partial class frmDeployment
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
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.lblVersus = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.grpShips = new System.Windows.Forms.GroupBox();
            this.btnDeployShip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPlayer1.Location = new System.Drawing.Point(108, 9);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(219, 42);
            this.lblPlayer1.TabIndex = 0;
            this.lblPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPlayer2.Location = new System.Drawing.Point(424, 9);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(219, 42);
            this.lblPlayer2.TabIndex = 1;
            this.lblPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersus
            // 
            this.lblVersus.Location = new System.Drawing.Point(357, 24);
            this.lblVersus.Name = "lblVersus";
            this.lblVersus.Size = new System.Drawing.Size(40, 27);
            this.lblVersus.TabIndex = 2;
            this.lblVersus.Text = "VS.";
            this.lblVersus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInstructions
            // 
            this.lblInstructions.Location = new System.Drawing.Point(56, 68);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(491, 30);
            this.lblInstructions.TabIndex = 3;
            this.lblInstructions.Text = "Place your ships on the battlefield. Choose wisely!";
            // 
            // grpShips
            // 
            this.grpShips.Location = new System.Drawing.Point(501, 101);
            this.grpShips.Name = "grpShips";
            this.grpShips.Size = new System.Drawing.Size(225, 378);
            this.grpShips.TabIndex = 4;
            this.grpShips.TabStop = false;
            this.grpShips.Text = "Ships for Deployment";
            // 
            // btnDeployShip
            // 
            this.btnDeployShip.BackColor = System.Drawing.Color.Black;
            this.btnDeployShip.Location = new System.Drawing.Point(501, 499);
            this.btnDeployShip.Name = "btnDeployShip";
            this.btnDeployShip.Size = new System.Drawing.Size(225, 36);
            this.btnDeployShip.TabIndex = 5;
            this.btnDeployShip.Text = "Deploy Ship";
            this.btnDeployShip.UseVisualStyleBackColor = false;
            // 
            // frmDeployment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(738, 547);
            this.Controls.Add(this.btnDeployShip);
            this.Controls.Add(this.grpShips);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblVersus);
            this.Controls.Add(this.lblPlayer2);
            this.Controls.Add(this.lblPlayer1);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "frmDeployment";
            this.Text = "Ship Deployment";
            this.Load += new System.EventHandler(this.frmDeployment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label lblVersus;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.GroupBox grpShips;
        private System.Windows.Forms.Button btnDeployShip;
    }
}

