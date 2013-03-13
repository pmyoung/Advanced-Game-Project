using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Client
{
    /// <summary>
    ///  The ClientSideMenu. This takes information from the user.
    ///  <author>Lacey Townes </author>
    /// </summary>

    public partial class ClientSideMenu : Form
    {
        /// <summary>
        /// The ClientSideMenu constructor, this initilizes the components and sets the testIp addresses
        /// </summary>

        /// <param name = "userName"> String. The user's name </param>
        /// <param name = "shiptype"> Int. Designates which ship type the user will use </param>
        /// <param name = "testIP"> Object. The IP address used for testing purposes </param>
        /// <param name = "info"> ClientConfirm. A client confirm menu</param>
       public String userName = "Default Name";
       public int shiptype = 0; 
       public object testIP = "0"; 
       public ClientConfirm info; 


        public ClientSideMenu()
        {
            InitializeComponent();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                testIP = IIPAddresses.Items.Add(ipHostInfo.AddressList[i]);
            }
        }

      /// <summary>
      /// IName_TextChanged. Method that is called when "IName" text has changed. It sets userName
      /// to whatever is entered.
      /// </summary>
      /// <param name="sender">Object</param>
      /// <param name="e">Event</param>
      /// 
        private void IName_TextChanged(object sender, EventArgs e)
        {
            userName = IName.Text;
        }

        /// <summary>
        /// IShiptype_SlectedIndexChanged. Method that is called when the user selects an item from
        /// the drop down list of ship types. It sets shipType
        /// </summary>
        /// <param name="sender">Object </param>
        /// <param name="e"> Event</param>
        private void IShiptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            shiptype = IShiptype.SelectedIndex;
        }

        /// <summary>
        /// IIPAddresses_SelectedIndexChanged method. This method is called when the user selects
        /// an IP address from the dropdown list
        /// </summary>
        /// <param name="sender"> Object </param>
        /// <param name="e"> Event </param>
        private void IIPAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            testIP = IIPAddresses.SelectedItem;  
        }

        /// <summary>
        /// BStart_Click. This method is called when the "Start" button is clicked. It creates a new
        /// ClientConfirm form, passes itself. It only closes the ClientSideMenu if the user selects
        /// yes from the confirm method.
        /// </summary>
        /// <param name="sender"> Object</param>
        /// <param name="e"> Event Click</param>
        private void BStart_Click(object sender, EventArgs e)
        {
            info = new ClientConfirm(this);
            info.ShowDialog();
            if (info.answer == 1)
                Close(); 

        }

    }
}
