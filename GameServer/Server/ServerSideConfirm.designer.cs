namespace Server
{
    partial class ServerSideConfirm
    {
        /// <summary>
        /// Required designer variable. This generates the labels/buttons/form (etc) and displays them
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
            this.LQuestion = new System.Windows.Forms.Label();
            this.LPlayerNum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Lpoints = new System.Windows.Forms.Label();
            this.DPlayerNum = new System.Windows.Forms.Label();
            this.DMaxHealth = new System.Windows.Forms.Label();
            this.DMaxPoints = new System.Windows.Forms.Label();
            this.True = new System.Windows.Forms.Button();
            this.False = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LQuestion
            // 
            this.LQuestion.AutoSize = true;
            this.LQuestion.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LQuestion.Location = new System.Drawing.Point(13, 13);
            this.LQuestion.Name = "LQuestion";
            this.LQuestion.Size = new System.Drawing.Size(177, 13);
            this.LQuestion.TabIndex = 0;
            this.LQuestion.Text = "Start the game with this information?";
            // 
            // LPlayerNum
            // 
            this.LPlayerNum.AutoSize = true;
            this.LPlayerNum.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LPlayerNum.Location = new System.Drawing.Point(16, 39);
            this.LPlayerNum.Name = "LPlayerNum";
            this.LPlayerNum.Size = new System.Drawing.Size(95, 13);
            this.LPlayerNum.TabIndex = 1;
            this.LPlayerNum.Text = "Number of players:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(16, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max health:";
            // 
            // Lpoints
            // 
            this.Lpoints.AutoSize = true;
            this.Lpoints.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Lpoints.Location = new System.Drawing.Point(16, 96);
            this.Lpoints.Name = "Lpoints";
            this.Lpoints.Size = new System.Drawing.Size(61, 13);
            this.Lpoints.TabIndex = 3;
            this.Lpoints.Text = "Max points:";
            // 
            // DPlayerNum
            // 
            this.DPlayerNum.AutoSize = true;
            this.DPlayerNum.Location = new System.Drawing.Point(139, 39);
            this.DPlayerNum.Name = "DPlayerNum";
            this.DPlayerNum.Size = new System.Drawing.Size(35, 13);
            this.DPlayerNum.TabIndex = 4;
            this.DPlayerNum.Text = "label2";
            // 
            // DMaxHealth
            // 
            this.DMaxHealth.AutoSize = true;
            this.DMaxHealth.Location = new System.Drawing.Point(139, 67);
            this.DMaxHealth.Name = "DMaxHealth";
            this.DMaxHealth.Size = new System.Drawing.Size(35, 13);
            this.DMaxHealth.TabIndex = 5;
            this.DMaxHealth.Text = "label3";
            // 
            // DMaxPoints
            // 
            this.DMaxPoints.AutoSize = true;
            this.DMaxPoints.Location = new System.Drawing.Point(139, 96);
            this.DMaxPoints.Name = "DMaxPoints";
            this.DMaxPoints.Size = new System.Drawing.Size(35, 13);
            this.DMaxPoints.TabIndex = 6;
            this.DMaxPoints.Text = "label4";
            // 
            // True
            // 
            this.True.ForeColor = System.Drawing.SystemColors.ControlText;
            this.True.Location = new System.Drawing.Point(233, 8);
            this.True.Name = "True";
            this.True.Size = new System.Drawing.Size(34, 23);
            this.True.TabIndex = 7;
            this.True.Text = "Yes";
            this.True.UseVisualStyleBackColor = true;
            this.True.Click += new System.EventHandler(this.True_Click);
            // 
            // False
            // 
            this.False.ForeColor = System.Drawing.SystemColors.ControlText;
            this.False.Location = new System.Drawing.Point(273, 8);
            this.False.Name = "False";
            this.False.Size = new System.Drawing.Size(32, 23);
            this.False.TabIndex = 8;
            this.False.Text = "No";
            this.False.UseVisualStyleBackColor = true;
            this.False.Click += new System.EventHandler(this.False_Click);
            // 
            // ServerSideConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(313, 126);
            this.Controls.Add(this.False);
            this.Controls.Add(this.True);
            this.Controls.Add(this.DMaxPoints);
            this.Controls.Add(this.DMaxHealth);
            this.Controls.Add(this.DPlayerNum);
            this.Controls.Add(this.Lpoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LPlayerNum);
            this.Controls.Add(this.LQuestion);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "ServerSideConfirm";
            this.Text = "ServerSideConfirm";
            this.Load += new System.EventHandler(this.ServerSideConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LQuestion;
        private System.Windows.Forms.Label LPlayerNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lpoints;
        private System.Windows.Forms.Label DPlayerNum;
        private System.Windows.Forms.Label DMaxHealth;
        private System.Windows.Forms.Label DMaxPoints;
        private System.Windows.Forms.Button True;
        private System.Windows.Forms.Button False;
    }
}