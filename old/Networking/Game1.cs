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


namespace GameClient
{
    enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Move,
        Create,
        Message,    //Send a text message to all the chat clients
        List,       //Get a list of users in the chat room from the server
        Null        //No command
    }

    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {


        public class ship
        {
            Texture2D obj;

            int x;
            int y;
            int id;
            string name;
            public int shipx
            {
                get { return x; }
                set { x = value; }
            }
            public int shipy
            {
                get { return y; }
                set { y = value; }
            }
            public string playername
            {
                get { return name; }
                set { name = value; }
            }
            public int shipid
            {
                get { return id; }
                set { id = value; }
            }

            public Texture2D shipObj
            {
                get { return obj; }
                set { obj = value; }
            }


        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Socket clientSocket;
        public EndPoint epServer;
        public string strName;
        public int playerID;
        public int playercount;
        public static List<ship> shipList = new List<ship>();
        byte []byteData = new byte[1024];

        SpriteFont font1;


        // The response from the remote device.
        private static String response = String.Empty;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
             strName = "nicktest";
             ship ship1 = new ship();
             ship1.shipx = 0;
             ship1.shipy = 0;
             ship1.shipObj = Content.Load<Texture2D>("test");
             shipList.Add(ship1);
             ship ship2 = new ship();
             ship2.shipx = 300;
             ship2.shipy = 300;
             ship2.shipObj = Content.Load<Texture2D>("test");
             shipList.Add(ship2);


                //Using UDP sockets
                clientSocket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);

                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                Console.WriteLine(ipAddress);

                //Server is listening on port 1000
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);

                epServer = (EndPoint)ipEndPoint;
                
                Data msgToSend = new Data();
                msgToSend.cmdCommand = Command.Login;
                msgToSend.strMessage = null;
                msgToSend.strName = strName;

                byte[] byteData = msgToSend.ToByte();
                
                //Login to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, 
                    SocketFlags.None, epServer, new AsyncCallback(OnSend), null);

                byteData = new byte[1024];
                //Start listening to the data asynchronously
                clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epServer,
                                           new AsyncCallback(OnReceive),null);
           
            base.Initialize();
            
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                strName = "nicktest";
                //DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                //Convert the bytes received into an object of type Data
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


                //Start listening to receive more data from the user
                clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epServer,
                                           new AsyncCallback(OnReceive), null);
                
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font1 = Content.Load<SpriteFont>("SpriteFont1");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if(Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right)){
                Data msgToSend = new Data();

                msgToSend.strName = strName;
                msgToSend.playerID = playerID;
                msgToSend.strMessage = "right";
                msgToSend.cmdCommand = Command.Move;

                byte[] byteData = msgToSend.ToByte();
                //Send it to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(OnSend), null);
            }

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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here


            base.Update(gameTime);
        }
        //The data structure by which the server and the client interact with 
        //each other
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

                for (int i = 0; i <= playerCount-1; i++)
                {
                    shipList[i].shipx = BitConverter.ToInt32(data, index);
                    index = index + 4;
                    shipList[i].shipy = BitConverter.ToInt32(data, index);
                    index = index + 4;
                }

                //The next four store the length of the name
                int nameLen = BitConverter.ToInt32(data, index);
                index = index + 4;

                //The next four store the length of the message
                int msgLen = BitConverter.ToInt32(data, index);
                index = index + 4;

                //This check makes sure that strName has been passed in the array of bytes
                if (nameLen > 0)
                    this.strName = Encoding.UTF8.GetString(data, index, nameLen);
                else
                    this.strName = null;

                //This checks for a null message field
                if (msgLen > 0)
                    this.strMessage = Encoding.UTF8.GetString(data, index + nameLen, msgLen);
                else
                    this.strMessage = null;
            }

            //Converts the Data structure into an array of bytes
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

            public int player1x;
            public int player1y;
            public int player2x;
            public int player2y;
            public int playerCount;
            public int playerID;
            public string strName;      //Name by which the client logs into the room
            public string strMessage;   //Message text
            public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            String output = "Hello World";
            Vector2 fontOrigin = font1.MeasureString(output) / 2;
            for (int j = 0; j <= playercount-1; j++)
            {
                spriteBatch.Draw(shipList[j].shipObj, new Vector2(shipList[j].shipx, shipList[j].shipy), new Color(255, 255, 255));
            }
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
