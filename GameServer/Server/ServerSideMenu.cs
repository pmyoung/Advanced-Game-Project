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
    /// This is the login form for the server
    /// <Author>Lacey Townes</Author>
    /// </summary>
    public partial class ServerSideMenu : Form
    {
        /// <summary>
        /// The constructor takes in no variables and initializes all the components
        /// <param name="confirm"> ServerSideConfirm. The confirm form</param>
        /// <param name="playerNum">Int. The number of players. If an incorrect value is entered the default is two</param>
        /// <param name="health">Int. The amount of health the players have. If an incorrect value is entered the default is 30</param>
        /// <param name="pointsToWin">Int. The number of points a player needs to win. If an incorrect value is entered the default is 100</param>
        /// </summary>
        public ServerSideConfirm confirm;
        public int playerNum = 2; //default
        public int health = 30; //default
        public int pointsToWin = 100; //default

        public ServerSideMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// IPlayerNumbers_SelectedIndexChanged. This method is called when the user selects an item from
        /// the dropdown list. It sets the playerNum.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        private void IPlayerNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            playerNum = IPlayerNumbers.SelectedIndex + 1;
        }

        /// <summary>
        /// IHealth_TextChanged. This method is called when the user enters a number for health. It
        /// checks to make sure that a number is entered, and throws an exception if an incorrect
        /// format is entered. It sets the health value.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        private void IHealth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                health = Int32.Parse(IHealth.Text);
            }
            catch (FormatException ex)
            {
                this.Text = "Sorry you must enter a int number";
                health = 30;
            }

        }

        /// <summary>
        /// IPoints_TextChanged. This method is called when the user enters a number for the points.
        /// It checks to make sure that the value entered is in the correct format and throws an error
        /// if not. It modifies the pointsToWin.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        private void IPoints_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pointsToWin = Int32.Parse(IPoints.Text);
            }
            catch (FormatException ex)
            {
                this.Text = "Sorry you must enter a int number";
            }
        }
        /// <summary>
        /// Start_Click. This method is called when the user clicks on the "Start" Button. It creates
        /// a new serverConfirm form and closes this form iff the user selected yes from the confirm 
        /// form.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event Click</param>
        private void Start_Click(object sender, EventArgs e)
        {
            confirm = new ServerSideConfirm(this);
            confirm.ShowDialog();
            if (confirm.answer == 1)
                Close();

        }

    }
}
