using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    /// <summary> 
    /// The "Client Confirm Menu". A pop-up that displays the inputted information
    /// and confirms that the information you have entered is the information you want to play
    /// with
    /// <author> "Lacey Townes" </author>
    /// </summary>
    /// 

    public partial class ClientConfirm : Form
    {
        /// <summary>
        /// The ClientConfirm constuctor requires a ClientSideMenu to be passed. 
        /// </summary>
        /// 
        /// <param name="Cmenu"> The ClientSideMenu that called confirm</param>
        /// <param name ="answer"> An int that stores the user's responses </param>
        /// 
        private ClientSideMenu Cmenu;
        public int answer = 0;

        public ClientConfirm(ClientSideMenu _Cmenu)
        {
            Cmenu=_Cmenu;
            InitializeComponent();
        }

        /// <summary>
        /// ClientConfirm_Load updates the form with the values it received from the ClientConfirm Menu.
        /// It sets the labels to show the received information.
        /// </summary>
        /// <param name="sender"></param> object
        /// <param name="e"></param> Event

        private void ClientConfirm_Load(object sender, EventArgs e)
        {
            
            Dname.Text = Cmenu.userName;
            DIP.Text = Cmenu.testIP.ToString();
            if (Cmenu.shiptype == 0)
                DShiptype.Text = "Alpha";
            else
                DShiptype.Text = "Beta";
        }

        /// <summary>
        /// InfoTrue_Click The method that is called when the "Yes" button is clicked. It sets the
        /// answer to 1 and closes the form.
        /// </summary>
        /// <param name="sender"> object </param> 
        /// <param name="e"> Event Clicked</param> 

        private void InfoTrue_Click(object sender, EventArgs e)
        {
            answer = 1; 
            Close(); 
        }

        /// <summary>
        /// InfoFalse_Click. The method that is called when the "No" button is clicked. It sets thr
        /// answer to 0 and closes the form.
        /// </summary>
        /// <param name="sender"> object </param>
        /// <param name="e">Event Clicked</param>

        private void InfoFalse_Click(object sender, EventArgs e)
        {
            answer = 0; 
            Close();
        }

    }
}
