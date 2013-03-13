﻿namespace Client
{
    /// <summary>
    /// The designer for ClientConfirm. This controls how things are initilized, set, named etc
    /// <author> Lacey Townes </author>
    /// </summary>
    partial class ClientConfirm
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
            this.label1 = new System.Windows.Forms.Label();
            this.LCName = new System.Windows.Forms.Label();
            this.Dname = new System.Windows.Forms.Label();
            this.LCShipType = new System.Windows.Forms.Label();
            this.DShiptype = new System.Windows.Forms.Label();
            this.LCIP = new System.Windows.Forms.Label();
            this.DIP = new System.Windows.Forms.Label();
            this.InfoTrue = new System.Windows.Forms.Button();
            this.InfoFalse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlText;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Is this the informaton you wanted to start with?";
            // 
            // LCName
            // 
            this.LCName.AutoSize = true;
            this.LCName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LCName.Location = new System.Drawing.Point(16, 43);
            this.LCName.Name = "LCName";
            this.LCName.Size = new System.Drawing.Size(41, 13);
            this.LCName.TabIndex = 1;
            this.LCName.Text = "Name: ";
            // 
            // Dname
            // 
            this.Dname.AutoSize = true;
            this.Dname.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Dname.Location = new System.Drawing.Point(97, 43);
            this.Dname.Name = "Dname";
            this.Dname.Size = new System.Drawing.Size(35, 13);
            this.Dname.TabIndex = 2;
            this.Dname.Text = "label2";
            // 
            // LCShipType
            // 
            this.LCShipType.AutoSize = true;
            this.LCShipType.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LCShipType.Location = new System.Drawing.Point(16, 71);
            this.LCShipType.Name = "LCShipType";
            this.LCShipType.Size = new System.Drawing.Size(58, 13);
            this.LCShipType.TabIndex = 3;
            this.LCShipType.Text = "Ship Type:";
            // 
            // DShiptype
            // 
            this.DShiptype.AutoSize = true;
            this.DShiptype.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.DShiptype.Location = new System.Drawing.Point(97, 71);
            this.DShiptype.Name = "DShiptype";
            this.DShiptype.Size = new System.Drawing.Size(35, 13);
            this.DShiptype.TabIndex = 4;
            this.DShiptype.Text = "label3";
            // 
            // LCIP
            // 
            this.LCIP.AutoSize = true;
            this.LCIP.BackColor = System.Drawing.SystemColors.ControlText;
            this.LCIP.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LCIP.Location = new System.Drawing.Point(19, 100);
            this.LCIP.Name = "LCIP";
            this.LCIP.Size = new System.Drawing.Size(61, 13);
            this.LCIP.TabIndex = 5;
            this.LCIP.Text = "IP Addess: ";
            // 
            // DIP
            // 
            this.DIP.AutoSize = true;
            this.DIP.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.DIP.Location = new System.Drawing.Point(97, 100);
            this.DIP.Name = "DIP";
            this.DIP.Size = new System.Drawing.Size(35, 13);
            this.DIP.TabIndex = 6;
            this.DIP.Text = "label4";
            // 
            // InfoTrue
            // 
            this.InfoTrue.Location = new System.Drawing.Point(245, 7);
            this.InfoTrue.Name = "InfoTrue";
            this.InfoTrue.Size = new System.Drawing.Size(37, 23);
            this.InfoTrue.TabIndex = 7;
            this.InfoTrue.Text = "Yes";
            this.InfoTrue.UseVisualStyleBackColor = true;
            this.InfoTrue.Click += new System.EventHandler(this.InfoTrue_Click);
            // 
            // InfoFalse
            // 
            this.InfoFalse.Location = new System.Drawing.Point(288, 7);
            this.InfoFalse.Name = "InfoFalse";
            this.InfoFalse.Size = new System.Drawing.Size(37, 23);
            this.InfoFalse.TabIndex = 8;
            this.InfoFalse.Text = "No";
            this.InfoFalse.UseVisualStyleBackColor = true;
            this.InfoFalse.Click += new System.EventHandler(this.InfoFalse_Click);
            // 
            // ClientConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(335, 133);
            this.Controls.Add(this.InfoFalse);
            this.Controls.Add(this.InfoTrue);
            this.Controls.Add(this.DIP);
            this.Controls.Add(this.LCIP);
            this.Controls.Add(this.DShiptype);
            this.Controls.Add(this.LCShipType);
            this.Controls.Add(this.Dname);
            this.Controls.Add(this.LCName);
            this.Controls.Add(this.label1);
            this.Name = "ClientConfirm";
            this.Text = "Super Space X";
            this.Load += new System.EventHandler(this.ClientConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LCName;
        private System.Windows.Forms.Label Dname;
        private System.Windows.Forms.Label LCShipType;
        private System.Windows.Forms.Label DShiptype;
        private System.Windows.Forms.Label LCIP;
        private System.Windows.Forms.Label DIP;
        private System.Windows.Forms.Button InfoTrue;
        private System.Windows.Forms.Button InfoFalse;
    }
}