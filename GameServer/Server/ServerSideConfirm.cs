using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// ServerSideConfirm. This form is created as a pop-up menu to ensure that the information that 
    /// the user entered for the server is correct.
    /// <Author>Lacey Townes</Author>
    /// </summary>
    public partial class ServerSideConfirm : Form
    {
        /// <summary>
        /// The constructor for ServerSideConfirm. It takes ServerSideMenu as a parameter and initializes
        /// the components.
        /// </summary>
        /// <param name = "SMenu">ServerSideMenu. The passed form that contains the user's information </para>
        /// <param name = "answer">Int. Stores the user's response </param>
        private ServerSideMenu SMenu;
        public int answer=0;

        public ServerSideConfirm(ServerSideMenu _sMenu)
        {
            SMenu = _sMenu;
            InitializeComponent();
        }

        /// <summary>
        /// ServerSideConfirm_Load. This initialzes the values of all the labels when the form is 
        /// loaded. (ie the number of players, the max health, the max points).
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        /// 
        private void ServerSideConfirm_Load(object sender, EventArgs e)
        {
            DPlayerNum.Text = SMenu.playerNum.ToString();
            DMaxHealth.Text = SMenu.health.ToString();
            DMaxPoints.Text = SMenu.pointsToWin.ToString();
        }
        /// <summary>
        /// True_Click. This method is called when the user clicks the "Yes" button. It sets the
        /// answer to 1 and closes the form
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event Click</param>
        private void True_Click(object sender, EventArgs e)
        {
            answer = 1;
            Close();
        }
        /// <summary>
        ///  False_Click. This method is called when the user clicks the "No" button. It sets the 
        ///  answer to zero and closes the form.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event Click</param>
        private void False_Click(object sender, EventArgs e)
        {
            answer = 0;
            Close();
        }

      
    }
}
