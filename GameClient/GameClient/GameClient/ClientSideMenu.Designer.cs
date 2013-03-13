namespace Client
{
    /// <summary>
    /// <author>Lacey Townes</author>
    /// </summary>

    partial class ClientSideMenu
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
            this.LPlayerName = new System.Windows.Forms.Label();
            this.IName = new System.Windows.Forms.TextBox();
            this.LShipType = new System.Windows.Forms.Label();
            this.IShiptype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IIPAddresses = new System.Windows.Forms.ComboBox();
            this.BStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LPlayerName
            // 
            this.LPlayerName.AutoSize = true;
            this.LPlayerName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LPlayerName.Location = new System.Drawing.Point(13, 13);
            this.LPlayerName.Name = "LPlayerName";
            this.LPlayerName.Size = new System.Drawing.Size(121, 13);
            this.LPlayerName.TabIndex = 0;
            this.LPlayerName.Text = "Please enter your name:";
            // 
            // IName
            // 
            this.IName.Location = new System.Drawing.Point(265, 13);
            this.IName.Name = "IName";
            this.IName.Size = new System.Drawing.Size(200, 20);
            this.IName.TabIndex = 1;
            this.IName.TextChanged += new System.EventHandler(this.IName_TextChanged);
            // 
            // LShipType
            // 
            this.LShipType.AutoSize = true;
            this.LShipType.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LShipType.Location = new System.Drawing.Point(13, 50);
            this.LShipType.Name = "LShipType";
            this.LShipType.Size = new System.Drawing.Size(176, 13);
            this.LShipType.TabIndex = 2;
            this.LShipType.Text = "What type of ship will you be using?";
            // 
            // IShiptype
            // 
            this.IShiptype.FormattingEnabled = true;
            this.IShiptype.Items.AddRange(new object[] {
            "Alpha",
            "Beta"});
            this.IShiptype.Location = new System.Drawing.Point(265, 50);
            this.IShiptype.Name = "IShiptype";
            this.IShiptype.Size = new System.Drawing.Size(200, 21);
            this.IShiptype.TabIndex = 3;
            this.IShiptype.Text = "Ship Type";
            this.IShiptype.SelectedIndexChanged += new System.EventHandler(this.IShiptype_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(13, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Testing: Ip Addresses";
            // 
            // IIPAddresses
            // 
            this.IIPAddresses.FormattingEnabled = true;
            this.IIPAddresses.Items.AddRange(new object[] {
            "00000000",
            "11111111"});
            this.IIPAddresses.Location = new System.Drawing.Point(265, 90);
            this.IIPAddresses.Name = "IIPAddresses";
            this.IIPAddresses.Size = new System.Drawing.Size(200, 21);
            this.IIPAddresses.TabIndex = 5;
            this.IIPAddresses.Text = "The IP Addresses";
            this.IIPAddresses.SelectedIndexChanged += new System.EventHandler(this.IIPAddresses_SelectedIndexChanged);
            // 
            // BStart
            // 
            this.BStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BStart.Location = new System.Drawing.Point(265, 146);
            this.BStart.Name = "BStart";
            this.BStart.Size = new System.Drawing.Size(200, 23);
            this.BStart.TabIndex = 6;
            this.BStart.Text = "Start Game";
            this.BStart.UseVisualStyleBackColor = true;
            this.BStart.Click += new System.EventHandler(this.BStart_Click);
            // 
            // ClientSideMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(492, 192);
            this.Controls.Add(this.BStart);
            this.Controls.Add(this.IIPAddresses);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IShiptype);
            this.Controls.Add(this.LShipType);
            this.Controls.Add(this.IName);
            this.Controls.Add(this.LPlayerName);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "ClientSideMenu";
            this.Text = "Super Space Ship X";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LPlayerName;
        private System.Windows.Forms.TextBox IName;
        private System.Windows.Forms.Label LShipType;
        private System.Windows.Forms.ComboBox IShiptype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox IIPAddresses;
        private System.Windows.Forms.Button BStart;
    }
}

