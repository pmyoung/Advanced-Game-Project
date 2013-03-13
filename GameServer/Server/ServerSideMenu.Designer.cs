namespace Server
{
    partial class ServerSideMenu
    {
        /// <summary>
        /// Required designer variable. This file creates and intializes the various labels, buttons and
        /// forms used.
        /// <Author>Lacey Townes</Author>
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
            this.LPlayerNumber = new System.Windows.Forms.Label();
            this.Lmaxhealth = new System.Windows.Forms.Label();
            this.IPlayerNumbers = new System.Windows.Forms.ComboBox();
            this.LPointsToEnd = new System.Windows.Forms.Label();
            this.IHealth = new System.Windows.Forms.TextBox();
            this.IPoints = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LPlayerNumber
            // 
            this.LPlayerNumber.AutoSize = true;
            this.LPlayerNumber.Location = new System.Drawing.Point(22, 18);
            this.LPlayerNumber.Name = "LPlayerNumber";
            this.LPlayerNumber.Size = new System.Drawing.Size(95, 13);
            this.LPlayerNumber.TabIndex = 0;
            this.LPlayerNumber.Text = "Number of players:";
            // 
            // Lmaxhealth
            // 
            this.Lmaxhealth.AutoSize = true;
            this.Lmaxhealth.Location = new System.Drawing.Point(22, 48);
            this.Lmaxhealth.Name = "Lmaxhealth";
            this.Lmaxhealth.Size = new System.Drawing.Size(89, 13);
            this.Lmaxhealth.TabIndex = 1;
            this.Lmaxhealth.Text = "Set player health:";
            // 
            // IPlayerNumbers
            // 
            this.IPlayerNumbers.FormattingEnabled = true;
            this.IPlayerNumbers.Items.AddRange(new object[] {
            "One",
            "Two ",
            "Three",
            "Four"});
            this.IPlayerNumbers.Location = new System.Drawing.Point(186, 10);
            this.IPlayerNumbers.Name = "IPlayerNumbers";
            this.IPlayerNumbers.Size = new System.Drawing.Size(121, 21);
            this.IPlayerNumbers.TabIndex = 2;
            this.IPlayerNumbers.Text = "Players";
            this.IPlayerNumbers.SelectedIndexChanged += new System.EventHandler(this.IPlayerNumbers_SelectedIndexChanged);
            // 
            // LPointsToEnd
            // 
            this.LPointsToEnd.AutoSize = true;
            this.LPointsToEnd.Location = new System.Drawing.Point(22, 77);
            this.LPointsToEnd.Name = "LPointsToEnd";
            this.LPointsToEnd.Size = new System.Drawing.Size(106, 13);
            this.LPointsToEnd.TabIndex = 3;
            this.LPointsToEnd.Text = "Points needed to win";
            // 
            // IHealth
            // 
            this.IHealth.Location = new System.Drawing.Point(186, 41);
            this.IHealth.Name = "IHealth";
            this.IHealth.Size = new System.Drawing.Size(121, 20);
            this.IHealth.TabIndex = 4;
            this.IHealth.TextChanged += new System.EventHandler(this.IHealth_TextChanged);
            // 
            // IPoints
            // 
            this.IPoints.Location = new System.Drawing.Point(186, 70);
            this.IPoints.Name = "IPoints";
            this.IPoints.Size = new System.Drawing.Size(121, 20);
            this.IPoints.TabIndex = 5;
            this.IPoints.TextChanged += new System.EventHandler(this.IPoints_TextChanged);
            // 
            // Start
            // 
            this.Start.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Start.Location = new System.Drawing.Point(186, 111);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(121, 23);
            this.Start.TabIndex = 6;
            this.Start.Text = "Start Game";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // ServerSideMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(319, 153);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.IPoints);
            this.Controls.Add(this.IHealth);
            this.Controls.Add(this.LPointsToEnd);
            this.Controls.Add(this.IPlayerNumbers);
            this.Controls.Add(this.Lmaxhealth);
            this.Controls.Add(this.LPlayerNumber);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "ServerSideMenu";
            this.Text = "ServerSide";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LPlayerNumber;
        private System.Windows.Forms.Label Lmaxhealth;
        private System.Windows.Forms.ComboBox IPlayerNumbers;
        private System.Windows.Forms.Label LPointsToEnd;
        private System.Windows.Forms.TextBox IHealth;
        private System.Windows.Forms.TextBox IPoints;
        private System.Windows.Forms.Button Start;
    }
}