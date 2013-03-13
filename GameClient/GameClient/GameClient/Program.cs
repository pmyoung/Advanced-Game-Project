///<file>Program.cs</file>
///<author>Nicholas Harding & Max Frattolin</author>
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Client;

namespace GameClient
{
//May need this for xbox controller later
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Startup client login form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ClientSideMenu loginForm = new ClientSideMenu();
            Application.Run(loginForm);
            
            if (loginForm.info.answer == 1)
            {
            //Startup game
                using (Game1 game = new Game1())
                {
                    game.Run();
                }
            }
        }//End Main
    }//End class program
#endif
}//End Namespace GameClient

