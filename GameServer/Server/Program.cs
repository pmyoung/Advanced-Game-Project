///<file>Program.cs</file>
///<author>Nicholas Harding & Max Frattolin</author>
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Server;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ServerSideMenu serverForm = new ServerSideMenu();

            //Run the server startup form
            Application.Run(serverForm);

            if (serverForm.confirm.answer == 1)
            {
                //Start the server
                Application.Run(new GameServerForm());
            }

        }
    }
}