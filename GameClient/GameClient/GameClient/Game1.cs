///<file>Game1.cs</file>
///<author>Nicholas Harding & Max Frattolin</author>
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using GameGraphics;
using Client;

namespace GameClient
{
    /// <summary>
    /// Commands for interaction between the server and the client 
    /// </summary>
    enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Move,       //Move command
        Update,     //Update command
        Create,     //Create an object (not currently used)
        Message,    //Send a text message to all the chat clients  (not currently used)
        List,       //Get a list of users in the chat room from the server (not currently used)
        Null        //No command
    }

    /// <summary>
    /// This is the main game, sets up many of the variables used throughout the game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Required variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        WorldGraphics world;
        static GraphicsModel model;
        public Socket clientSocket;
        public EndPoint epServer;
        public string strName;
        public int playerID;
        double dwTimePrevUpdate = 0;
        public int playercount;
        public static List<GraphicsObject> shipList = new List<GraphicsObject>();
        public static List<GraphicsObject> bulletList = new List<GraphicsObject>();
        public static List<GraphicsObject> planetList = new List<GraphicsObject>();
        byte[] byteData = new byte[1024];
        SpriteFont font1;
        private static String response = String.Empty;

        /// <summary>
        /// Default constructer
        /// </summary>
        public Game1()
        {
            //Setup all graphics
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add some name detection from forms.
            strName = "nicktest";
            world = new WorldGraphics();
            model = new GraphicsModel();
            world.SetGraphicsModel(model);

            //Setup ships for players 1 & 2 (offscreen to start)
            //GraphicsObject(int id, float x, float y, float radius, float angle, int spriteID, int colorID)
            GraphicsObject ship1 = new GraphicsObject(1, 0, 0, 16, 0, 1, 5);
            model.Update(ship1);
            shipList.Add(ship1);
            GraphicsObject ship2 = new GraphicsObject(2, -16, -16, 16, 0, 1, 7);
            model.Update(ship2);
            shipList.Add(ship2);

            //Setup planet
            GraphicsObject planet = new GraphicsObject(3, 400, 300, 32, 0, 201, 0);
            model.Update(planet);
            planetList.Add(planet);


            //******** Networking Stuff *************
            //Using UDP sockets
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            Console.WriteLine(ipAddress);

            //Server is listening on port 1000
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);
            epServer = (EndPoint)ipEndPoint;

            //Get message ready
            Data msgToSend = new Data();
            msgToSend.cmdCommand = Command.Login;
            msgToSend.strMessage = null;
            msgToSend.strName = strName;
            byte[] byteData = msgToSend.ToByte();

            //Login to the server
            clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            byteData = new byte[1024];
            //Start listening to the data asynchronously
            clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epServer, new AsyncCallback(OnReceive), null);
            //******** End Networking Stuff *************
            base.Initialize();

        }

        /// <summary>
        /// On send function
        /// </summary>
        /// <param name="ar">Status of the asynchronous operation</param>
        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                strName = "nicktest";
            }
            //Exception handling
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Receiving data function, goes through data received and uses the appropriate
        /// command
        /// </summary>
        /// <param name="ar">Status of the asynchronous operation</param>
        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                //Convert the bytes received into an object of type Data
                clientSocket.EndReceive(ar);
                Data msgReceived = new Data(byteData);

                //Accordingly process the message received
                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:
                        playercount = msgReceived.playerCount;
                        break;

                    case Command.Create:

                        playerID = msgReceived.playerID;
                        break;

                    case Command.Logout:
                        break;

                    case Command.Move:
                        break;

                    case Command.Message:
                        break;

                    case Command.List:
                        break;
                }
                byteData = new byte[1024];


                //Start listening to receive more data
                clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epServer, new AsyncCallback(OnReceive), null);

            }
            //Exception Handling
            catch (ObjectDisposedException)
            { }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// LoadContent will be called once per game to load all content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            font1 = Content.Load<SpriteFont>("SpriteFont1");
            SpriteStore sp = new SpriteStore();
            sp.LoadSprites(Content);
            world.SetSpriteStore(sp);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game gather input from the user.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Capture keyboard input

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            //Turn right
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
            {
                Data msgToSend = new Data();
                msgToSend.strName = strName;
                msgToSend.playerID = playerID;
                msgToSend.strMessage = "right";
                msgToSend.cmdCommand = Command.Move;
                byte[] byteData = msgToSend.ToByte();
                //Send it to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            }

            //Turn left
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
            {
                Data msgToSend = new Data();
                msgToSend.strName = strName;
                msgToSend.playerID = playerID;
                msgToSend.strMessage = "left";
                msgToSend.cmdCommand = Command.Move;
                byte[] byteData = msgToSend.ToByte();
                //Send it to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            }

            //Speed up
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                Data msgToSend = new Data();
                msgToSend.strName = strName;
                msgToSend.playerID = playerID;
                msgToSend.strMessage = "up";
                msgToSend.cmdCommand = Command.Move;
                byte[] byteData = msgToSend.ToByte();
                //Send it to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            }

            //Slow down
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                Data msgToSend = new Data();
                msgToSend.strName = strName;
                msgToSend.playerID = playerID;
                msgToSend.strMessage = "down";
                msgToSend.cmdCommand = Command.Move;
                byte[] byteData = msgToSend.ToByte();
                //Send it to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            }

            //For shooting, we want to limit the number of bullets that can be created (fire rate)
            double gameTimeNow = gameTime.TotalGameTime.TotalMilliseconds;
            //Check how long it's been since the last shot.
            if (gameTimeNow - dwTimePrevUpdate > 500)
            {
                //Check the shoot button is pressed
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                {
                    Data msgToSend = new Data();
                    msgToSend.strName = strName;
                    dwTimePrevUpdate = gameTime.TotalGameTime.TotalMilliseconds;
                    msgToSend.playerID = playerID;
                    msgToSend.strMessage = "shoot";
                    msgToSend.cmdCommand = Command.Move;
                    byte[] byteData = msgToSend.ToByte();
                    //Send it to the server
                    clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);

                }
            }

            //Get the list of graphics objects
            List<GraphicsObject> list = model.GetAsList();
            //Call a new update.
            base.Update(gameTime);
        }

        /// <summary>
        /// The data structure by which the server and the client interact with each other
        /// </summary>
        class Data
        {
            //Default constructor
            public Data()
            {
                this.cmdCommand = Command.Null;
                this.strMessage = null;
                this.strName = null;
            }

            //Converts the bytes into an object of type Data
            public Data(byte[] data)
            {
                //Index to keep track of positioning
                int index = 0;

                //The first four bytes are for the Command
                this.cmdCommand = (Command)BitConverter.ToInt32(data, index);
                index = index + 4;
                
                //Number of players
                this.playerCount = BitConverter.ToInt32(data, index);
                index = index + 4;
                
                //player ID
                this.playerID = BitConverter.ToInt32(data, index);
                index = index + 4;

                //For each player, get ship data
                for (int i = 0; i <= playerCount - 1; i++)
                {
                    shipList[i].SetX(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    shipList[i].SetY(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    shipList[i].SetAngle(BitConverter.ToInt32(data, index));
                    index = index + 4;
                }

                //Number of planets
                this.planetCount = BitConverter.ToInt32(data, index);
                index = index + 4;

                //For each planet get data
                for (int m = 0; m < planetCount; m++)
                {
                    planetList[m].SetX(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    planetList[m].SetY(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    model.Update(planetList[m]);
                }

                //The next four store the length of the nickname
                int nameLen = BitConverter.ToInt32(data, index);
                index = index + 4;

                //The next four store the length of the message
                int msgLen = BitConverter.ToInt32(data, index);
                index = index + 4;

                //This gets the name or sets it as null.
                if (nameLen > 0)
                    this.strName = Encoding.UTF8.GetString(data, index, nameLen);
                else
                {
                    this.strName = null;
                    index = index + 4;
                }

                //This gets the message or sets it as null
                if (msgLen > 0)
                    this.strMessage = Encoding.UTF8.GetString(data, index + nameLen, msgLen);
                else
                {
                    this.strMessage = null;
                    index = index + 4;
                }
                //update index
                index = index + nameLen + msgLen;

                //Get the number of bullets
                this.bulletCount = BitConverter.ToInt32(data, index);
                index = index + 4;

                //For each bullet, remove them all (redraw them in their new position.
                //TODO: Review this
                //Note: this may not be the best way of doing things, but is needed so bullets don't mess up currently.
                for (int n = bulletCount; n < bulletList.Count(); n++)
                {
                    model.Remove(20 + n);
                    bulletList.RemoveAt(n);
                }
                //Readd all the bullets to the model
                for (int j = 0; j < bulletCount; j++)
                {
                    int exists = 0;
                    //If the bullets already exist, do nothing
                    foreach (GraphicsObject g in bulletList)
                    {
                        if (g.GetID() == (20 + j))
                        {
                            //do nothing
                            exists = 1;
                            break;
                        }
                    }
                    //If they don't, create a bullet and add it to the list.
                    if (exists == 0)
                    {
                        bulletList.Add(new GraphicsObject(20 + j, 0, 0, 4, 0, 102, 1));
                    }
                    bulletList[j].SetX(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    bulletList[j].SetY(BitConverter.ToInt32(data, index));
                    index = index + 4;
                    model.Update(bulletList[j]);
                }//End for loop
            }//End Data constructor

            //Converts the Data structure into an array of bytes (for sending)
            public byte[] ToByte()
            {
                List<byte> result = new List<byte>();

                //First four are for the Command
                result.AddRange(BitConverter.GetBytes((int)cmdCommand));
                result.AddRange(BitConverter.GetBytes((int)playerID));

                //Add the length of the name
                if (strName != null)
                    result.AddRange(BitConverter.GetBytes(strName.Length));
                else
                    result.AddRange(BitConverter.GetBytes(0));

                //Length of the message
                if (strMessage != null)
                    result.AddRange(BitConverter.GetBytes(strMessage.Length));
                else
                    result.AddRange(BitConverter.GetBytes(0));

                //Add the name
                if (strName != null)
                    result.AddRange(Encoding.UTF8.GetBytes(strName));

                //And, lastly we add the message text to our array of bytes
                if (strMessage != null)
                    result.AddRange(Encoding.UTF8.GetBytes(strMessage));

                return result.ToArray();
            }

            public int playerCount;
            public int bulletCount;
            public int planetCount;
            public int playerID;
            public string strName;      //Name by which the client logs into the room
            public string strMessage;   //Message text
            public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
        }//End of data object

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            world.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
