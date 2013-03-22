///<file>GameServerForm.cs</file>
///<author>Nicholas Harding & Max Frattolin</author>
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    /// <summary>
    /// Commands for interaction between the server and the client 
    /// </summary>
    enum Command
    {
        Login,      //Log into the server
        Logout,     //Logout of the server
        Move,       //Move a player (also used for shoot)
        Update,     //Update the locations (used by update loop)
        Create,     //Create an object (not currently in use)
        Message,    //Send a text message to all clients (not currently in use)
        List,       //Get a list of users in the chat room from the server (not currently in use)
        Null        //No command
    }
    
    //Powerup class (To be moved out and abstracted later)
    /// <summary>
    /// This class represents a powerup.
    /// </summary>
    class PowerUp : GameObject
    {
        string powerUpType;
        public void randomizePosition()
        {
            //Randomly place the powerup somewhere within the map boundaries
            Random random = new Random();
            this.x = random.Next(this.radius, MatchConfig.mapWidth+1-this.radius);
            this.y = random.Next(this.radius, MatchConfig.mapHeight+1-this.radius);
        }
        public void randomizeType()
        {
            //Randomize the type of the object
            Random random = new Random();
            //Choose a random spriteID and set the powerUpType based on the spriteID
            this.spriteID = random.Next(300, 310);
            switch (this.spriteID)
            {
                case 300:
                    //Lower reload rate compared to regular bullets
                    this.powerUpType = "Rapid Fire";
                    break;
                case 301:
                    //The player's gravity is reversed, preventing them from crashing into planets
                    this.powerUpType = "Anti-Gravity";
                    break;
                case 302:
                    //Fires 3 bullets at a time, at -7, 0 and 7 degrees respectively
                    this.powerUpType = "Spread Shot - 3";
                    break;
                case 303:
                    //Fires 5 bullets at a time, at -10, -5, 0, 5 and 10 degrees respectively
                    this.powerUpType = "Spread Shot - 5";
                    break;
                case 304:
                    //Provides upgraded movement speed and turning rate
                    this.powerUpType = "Engine Upgrade";
                    break;
                case 305:
                    //Homing bullets chase nearby players
                    this.powerUpType = "Homing Bullet";
                    break;
                case 306:
                    //Big bullets are extra large (Bigger radius) and deal extra damage
                    this.powerUpType = "Big Bullet";
                    break;
                case 307:
                    //Gravity bullets are regular bullets, only they have a strong gravitational pull (Like moons)
                    this.powerUpType = "Gravity Bullet";
                    break;
                case 308:
                    //Mines can be implemented as non-moving bullets that spawn behind the player's ship.  Make them affected by gravity
                    //so that they are eventually destroyed.
                    this.powerUpType = "Mine";
                    break;
                case 309:
                    //Lazers are very fast-moving bullets that are not affected by gravity
                    //The sprite for lazers will need to be rendered at a specific angle
                    this.powerUpType = "Lazer";
                    break;
                case 310:
                    //Ramming plate substantially increases the weight of the player's ship, causing it knock away enemy ships with
                    //a huge amount of force when it collides with them
                    this.powerUpType = "Ramming Plate";
                    break;
            }
        }

        public void TreatCollision(Planets object2) {
        //The powerup spawned on a planet - move it to a new location
            this.randomizePosition();
        }

        public void TreatCollision(Player object2) {
	    //A player collected the powerUp
            object2.powerUpDuration = 1000;
            object2.powerUpType = this.powerUpType;
        }
    }

    //Bullet class (To be moved out and abstracted later)
    /// <summary>
    /// This class represents a bullet fired by a ship .
    /// </summary>
    class Bullet : GameObject
    {
        int ownedby;

        //Get and sets for all variables
        public int whoshot
        {
            get { return ownedby; }
            set { ownedby = value; }
        }

    }

    /// <summary>
    /// This class represents a player's ship including the ships variables.
    /// </summary>
    class Player : GameObject
    {
        string name;
        int bulletcount;
        public int powerUpDuration = 0;
        public int reloadTime = 0;
        public string powerUpType = "";
        //Get and sets for all variables

        public int bcount
        {
            get { return bulletcount; }
            set { bulletcount = value; }
        }

        public string playername
        {
            get { return name; }
            set { name = value; }
        }

        public void weaponTimer()
        {
            powerUpDuration--;
            reloadTime--;
        }
        
        /// <summary>
        /// In case a collison is detected, this function should be called to treat how a player instance will respond in collison with another object
        /// </summary>
        /// <param name="object2">An instance to a player</param>
        public void treatCollision(Player object2)
        {
                //Elastic head-on collision, both objects will reflect based on their respective masses
                //http://hyperphysics.phy-astr.gsu.edu/%E2%80%8Chbase/colsta.html#c5
                this.speedX = ((this.mass - object2.mass) / (object2.mass + this.mass)) * object2.speedX;
                this.speedY = ((this.mass - object2.mass) / (object2.mass + this.mass)) * object2.speedY;
                object2.speedX = ((2 * object2.mass) / (object2.mass + this.mass)) * this.speedX;
                object2.speedY = ((2 * object2.mass) / (object2.mass + this.mass)) * this.speedY;
                //Separate the two objects so the collision isn't detected multiple times
                object2.x = object2.x + object2.speedX;
                object2.y = object2.y + object2.speedY;
                this.x = this.x + this.speedX;
                this.y = this.y + this.speedY;
        }
        
        public void treatCollision(Planets object2)
        {
            //"Massive target" collision, the player will reflect only, other object will not move
            //http://hyperphysics.phy-astr.gsu.edu/%E2%80%8Chbase/colsta.html#c5
            this.speedX = -this.speedX*MatchConfig.collisionEnergy;
            this.speedY = -this.speedY*MatchConfig.collisionEnergy;
            //Separate the two objects so the collision isn't detected multiple times
            this.x = this.x + this.speedX;
            this.y = this.y + this.speedY;
        }
    }

    //Planets (to be moved out later)
    /// <summary>
    /// This class represents a Planets that it would be not affected by gravity and collisions with other objects.
    /// </summary>
    class Planets : GameObject
    {
        /// <summary>
        /// This function changes the speed of the object based on its proximity to its Planet instance
        /// </summary>
        /// <param name="p">An instance to a player</param>
        public void applyGravity(Player p)
        {
            //Gravity Calculations between players and the planet(object producing the gravity).
            float G = MatchConfig.gravityConstant;

            double distanceG = Math.Sqrt((Math.Pow(p.x - this.x, 2) + Math.Pow(p.y - this.y, 2)));
            double speedVector = (float)((G * this.mass) / Math.Pow(distanceG, 2));
            double deltaY = (p.y - this.y);
            double deltaX = (p.x - this.x);
            double angleDegree = Math.Atan(deltaY / deltaX);// * 180 / Math.PI;
            //float rad = (float)(Math.PI / 180) * o.angle;
            if (p.x != this.x)
            {
                //speedX += (float)(Math.Cos(angleDegree) * speedVector);
                if (p.x > this.x)
                    p.speedX -= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                else
                    p.speedX += (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
            }
            if (p.y != this.y)
            {
                //speedY += (float)(Math.Sin(angleDegree) * speedVector);
                if (p.y < this.y)
                    p.speedY += (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                else
                    p.speedY -= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
            }
        }

        /// <summary>
        /// This function changes the speed of the object based on its proximity to its Planet instance
        /// </summary>
        /// <param name="b">An instance to a Bullet</param>
        public void applyGravity(Bullet b)
        {
            //Gravity Calculations between bullets and the planet(object that is producing gravity)
            float G = MatchConfig.gravityConstant;

            double distanceG = Math.Sqrt((Math.Pow(b.x - this.x, 2) + Math.Pow(b.y - this.y, 2)));
            double speedVector = (float)((G * this.mass) / Math.Pow(distanceG, 2));
            double deltaY = (b.y - this.y);
            double deltaX = (b.x - this.x);
            double angleDegree = Math.Atan(deltaY / deltaX);// * 180 / Math.PI;
            //float rad = (float)(Math.PI / 180) * o.angle;
            if (b.x != this.x)
            {
                //speedX += (float)(Math.Cos(angleDegree) * speedVector);
                if (b.x > this.x)
                    b.speedX -= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                else
                    b.speedX+= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
            }
            if (b.y != this.y)
            {
                //speedY += (float)(Math.Sin(angleDegree) * speedVector);
                if (b.y < this.y)
                    b.speedY+= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                else
                    b.speedY-= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
            }
        }
    }

    /// <summary>
    /// GameServerForm is where all the Server information is set up
    /// </summary>
    public partial class GameServerForm : Form
    {
        //The ClientInfo structure holds the required information about every
        //client connected to the server
        struct ClientInfo
        {
            public EndPoint endpoint;   //Socket of the client
            public string strName;      //Name by which the user logged into the chat room
        }

        //The collection of all clients logged into the room (an array of type ClientInfo)
        ArrayList clientList;
        //Lists of players, planets, bullets and objects to remove (for sending to client)
        List<Player> playerList = new List<Player>();
        List<Planets> planetList = new List<Planets>();
        List<Bullet> bulletList = new List<Bullet>();
        List<int> removeids = new List<int>();
        //The main socket on which the server listens to the clients
        Socket serverSocket;
        //Counts used for various things
        int playercount = 0;
        int planetcount = 1;
        int bulletcount = 19;
        //Byte data 
        byte[] byteData = new byte[1024];
        public GameServerForm()
        {
            clientList = new ArrayList();
            InitializeComponent();
        }

        /// <summary>
        /// Form1_Load is called when server is started by the user, sets up the UDP Socket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contains event data(not used)</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;

                //We are using UDP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                Console.WriteLine(ipAddress);
                //Assign the any IP of the machine and listen on port number 1000
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 11000);

                //Bind this address to the server
                serverSocket.Bind(ipEndPoint);
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);

                //The epSender identifies the incoming clients
                EndPoint epSender = (EndPoint)ipeSender;

                //Start receiving data
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender, new AsyncCallback(OnReceive), epSender);

                //Thread to start updating
                Thread Logic = new Thread(new ThreadStart(Update));
                Logic.Start();
            }
            //Exception Handling
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GameUDPServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This is the update loop used to constantly update the game objects
        /// checking for collisions, and removing objects
        /// </summary>
        public void Update()
        {
            //Required variables
            byte[] message;
            Player playerbullet = null;
            List<Bullet> toRemove = new List<Bullet>();
            Data msgToSend = new Data();
            //Loop forever
            while (true)
            {
                //Wait 40 ms
                Thread.Sleep(40);

                //Update planets
                foreach (Planets planet in planetList)
                {
                    planet.x = 400;
                    planet.y = 300;
                }
                //Update players
                foreach (Player p in playerList)
                {
                    //Adjust placement by speed
                    p.x += p.speedX;
                    p.y += p.speedY;

                    //Collision Detection between Player and Border. For now, Players will bounce off the border in opposite direction.
                    p.outOfBounds();
                    
                    //Collision Detection between Players. For now, Players will bounce off of each other. No damage to players.
                    foreach (Player p2 in playerList)
                    {
                        if (p.checkCollision(p2))
                        {
                            p.treatCollision(p2);
                        }
                    }

                    //Gravity and Collision Detection between Planet and players
                    foreach (Planets planet in planetList)
                    {
                        planet.applyGravity(p);

                        //Collision Detection between player and planets. For now, Players will bounce off of the planet. No damage to each other.
                        if (p.checkCollision(planet))
                        {
                            p.treatCollision(planet);                         
                        }                        
                    }
                }//end of player loop

                //Update bullets
                foreach (Bullet b in bulletList)
                {
                    //Check who shot each bullet, (required for the max bullet count per player)
                    foreach (Player playerp in playerList)
                    {
                        if (playerp.id == b.whoshot)
                        {
                            playerbullet = playerp;
                        }
                    }

                    //Bullet Movement
                    b.x += b.speedX;
                    b.y += b.speedY;
                    
                    //Collision Detection between bullets and border. At this time, bullets will disappear after they reach the border.
                    if (b.x < 0)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    else if (b.x > MatchConfig.mapWidth)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    if (b.y < 0)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    else if (b.y > MatchConfig.mapHeight)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }

                    //Collision Detection between players and bullets. For now, the bullets will disappear when they collide with players. Players will remain.
                    foreach (Player p in playerList)
                    {
                        if (b.checkCollision(p))
                        {
                            //remove bullet
                            playerbullet.bcount--;
                            toRemove.Add(b);
                            //remove player
                            //TODO: Add code/lists to delete players when collide with bullets
                        }
                    }

                    //Collision Detection and Gravity between planet and bullets.
                    foreach (Planets planet in planetList)
                    {
                        //Collision Detection between planet and bullets. For now, the bullets will disappear when they collide with the planet.
                        if (b.checkCollision(planet))
                        {
                            playerbullet.bcount--;
                            toRemove.Add(b);
                        }

                        planet.applyGravity(b);
                    }
                }//End bullet loop

                //Remove bullets
                foreach (Bullet r in toRemove)
                {
                    //Add to the list of bullet ids to remove
                    removeids.Add(r.id);
                    bulletList.Remove(r);
                }

                //Create the message to send to the clients
                message = msgToSend.ToByte(playerList, bulletList, removeids, playercount, planetList);

                //Loop through all clients
                foreach (ClientInfo clientInfo in clientList)
                {
                    //Send the message to all users
                    serverSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, clientInfo.endpoint, new AsyncCallback(OnSend), clientInfo.endpoint);
                }
            }//End while loop
        }

        /// <summary>
        /// This function is to unpack all the data received from a client.
        /// </summary>
        /// <param name="ar">Status of the asynchronous operation</param>
        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                //Required variables
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint epSender = (EndPoint)ipeSender;
                serverSocket.EndReceiveFrom(ar, ref epSender);
                byte[] message;
                //Transform the array of bytes received from the user into an
                //intelligent form of object Data
                Data msgReceived = new Data(byteData);

                //We will send this object in response the users request
                Data msgToSend = new Data();


                //If the message is to login, logout, or simple text message
                //then when send to others the type of the message remains the same
                msgToSend.cmdCommand = msgReceived.cmdCommand;
                msgToSend.strName = msgReceived.strName;

                //Check which kind of message we received
                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:
                        //When a user logs in to the server then we add her to our
                        //list of clients
                        if (playercount == 0)
                        {
                            //Setup player 1
                            Player player1 = new Player();
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.endpoint = epSender;
                            clientInfo.strName = msgReceived.strName;
                            msgToSend.cmdCommand = Command.Login;
                            msgToSend.id = playercount;
                            clientList.Add(clientInfo);
                            player1.x = 100;
                            player1.y = 100;
                            player1.mass = 1;
                            player1.radius = 16;
                            player1.playername = msgReceived.strName;
                            player1.id = playercount;
                            playerList.Add(player1);
                            playercount = playercount + 1;

                            //Setup planets (1 for now)
                            Planets planet = new Planets();
                            planet.x = 400;
                            planet.y = 300;
                            planet.radius = 32;
                            planet.mass = 150;
                            planetList.Add(planet);
                            planetcount = 1;

                            //Set the text of the message that we will broadcast to all users (doesn't currently do much)
                            msgToSend.strMessage = "<<<" + msgReceived.strName + " has joined the room>>>";
                            msgToSend.cmdCommand = Command.Login;

                        }
                        else
                        {
                            //Setup player 2
                            Player player2 = new Player();
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.endpoint = epSender;
                            clientInfo.strName = msgReceived.strName;
                            msgToSend.id = playercount;
                            player2.id = playercount;
                            clientList.Add(clientInfo);
                            player2.x = 700;
                            player2.y = 500;
                            player2.radius = 16;
                            player2.playername = msgReceived.strName;
                            player2.id = playercount;
                            playerList.Add(player2);
                            playercount = playercount + 1;

                            //Set the text of the message that we will broadcast to all users
                            msgToSend.strMessage = "<<<" + msgReceived.strName + " has joined the room>>>";
                            msgToSend.cmdCommand = Command.Login;
                        }
                        break; //End case login

                    
                    case Command.Create:
                        //Create objects (not currently in use)
                        break; //End case create

                    case Command.Logout:
                        //When a user wants to log out of the server then we search for her 
                        //in the list of clients and close the corresponding connection
                        //TODO: Fix logout errors.
                        int nIndex = 0;
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.endpoint == epSender)
                            {
                                clientList.RemoveAt(nIndex);
                                break;
                            }
                            ++nIndex;
                        }
                        //Set the text of the message that we will broadcast to all users
                        msgToSend.strMessage = "<<<" + msgReceived.strName + " has left the room>>>";
                        break; //End case logout

                    case Command.Message:
                        //Set the text of the message that we will broadcast to all users
                        msgToSend.strMessage = msgReceived.strName + ": " + msgReceived.strMessage;
                        break;//End case message

                    case Command.Move:
                        //Setup the move command
                        msgToSend = PlayerAction(msgReceived);
                        msgToSend.cmdCommand = msgReceived.cmdCommand;
                        break;//End case move

                    case Command.List:
                        //Send the names of all users in the chat room to the new user
                        msgToSend.cmdCommand = Command.List;
                        msgToSend.strName = null;
                        msgToSend.strMessage = null;

                        //Collect the names of the user in the chat room
                        foreach (ClientInfo client in clientList)
                        {
                            //To keep things simple we use asterisk as the marker to separate the user names
                            msgToSend.strMessage += client.strName + "*";
                        }
                        //Setup our message to send
                        message = msgToSend.ToByte(playerList, bulletList, removeids, playercount, planetList);

                        //Send the name of the users in the chat room
                        serverSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, epSender, new AsyncCallback(OnSend), epSender);
                        break;
                }//End command list

                //TODO: Add check to see if it is a login, if it is send the players ID to the client
                //When the client receives the login response it will set the users ID to the correct value
                if (msgToSend.cmdCommand == Command.Login)
                {
                    //Send the login message
                    message = msgToSend.ToByte(playerList, bulletList, removeids, playercount, planetList);
                    serverSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, epSender, new AsyncCallback(OnSend), epSender);
                    msgToSend.cmdCommand = Command.Create;
                    message = msgToSend.ToByte(playerList, bulletList, removeids, playercount, planetList);
                    serverSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, epSender, new AsyncCallback(OnSend), epSender);
                    msgToSend.cmdCommand = Command.Login;
                }


                if (msgToSend.cmdCommand != Command.List)
                {
                    //Setup the list message
                    message = msgToSend.ToByte(playerList, bulletList, removeids, playercount, planetList);

                    foreach (ClientInfo clientInfo in clientList)
                    {
                        //Send the message to all users
                        serverSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, clientInfo.endpoint,
                            new AsyncCallback(OnSend), clientInfo.endpoint);
                    }
                }


                //If the user is logging out then we need not listen from her
                //Start listening to the message send by the user
                serverSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None, ref epSender, new AsyncCallback(OnReceive), epSender);

            }
            //Exception handling
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GameUDPServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actually send out our messages
        /// </summary>
        /// <param name="ar">Status of the asynchronous operation</param>
        public void OnSend(IAsyncResult ar)
        {
            try
            {
                serverSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GameUDPServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This method is currently not in use, will be implemented with chat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
            txtLog.Refresh();
        }

        /// <summary>
        /// Setup a player action, by going through the received data and figuring out which action
        /// a player is performing (move, shoot, etc.)
        /// </summary>
        /// <param name="msgReceived">Received data</param>
        /// <returns>Data object of player action</returns>
        private Data PlayerAction(Data msgReceived)
        {
            //Required variables
            Player playermoved = null;
            double accelerate = 0;
            int rot = 0;

            //Figure out who is making the action.
            foreach (Player playerp in playerList)
            {
                if (playerp.id == msgReceived.id)
                {
                    playermoved = playerp;
                }

            }
            //Setup our new message to send.
            Data msgToSend = new Data();
            //Rotate right
            if (msgReceived.strMessage == "right")
            {
                rot++;
                rot = rot * MatchConfig.rotationRate;
                playermoved.angle += rot;

            }
            //Rotate left
            if (msgReceived.strMessage == "left")
            {
                rot++;
                rot = rot * MatchConfig.rotationRate;
                playermoved.angle -= rot;

            }
            //Increase speed
            if (msgReceived.strMessage == "up")
            {

                // this is to highlight that the Math.Sin and Math.Cos use the radian
                // for its "angle" It also demenstrates that we need to store our
                // locations using floats as it will allow for nice turning rather
                // than fixed 8-directional (N, NE, E, SE, S, SW, W, NW) movement
                accelerate++;
                accelerate = accelerate * MatchConfig.accelerationRate;
                float rad = (float)(Math.PI / 180) * playermoved.angle;
                playermoved.speedX += (float)((float)Math.Sin(rad) * accelerate);
                playermoved.speedY += -1 * (float)((float)Math.Cos(rad) * accelerate);
                //Note we don't actually increase the x or y here, just the speed.
            }
            //Decrease speed
            if (msgReceived.strMessage == "down")
            {
                accelerate++;
                accelerate = accelerate * MatchConfig.brakeRate;
                float rad = (float)(Math.PI / 180) * playermoved.angle;
                playermoved.speedX -= (float)((float)Math.Sin(rad) * accelerate);
                playermoved.speedY -= -1 * (float)((float)Math.Cos(rad) * accelerate);
                //Note we don't actually decrease the x or y here, just the speed.

            }
            //Shoot command
            if (msgReceived.strMessage == "shoot")
            {
                //Check that the player has less than 10 bullets onscreen.
                if (playermoved.bcount < 10)
                {
                    playermoved.bcount++;
                    Bullet bullet = new Bullet();
                    bullet.speedX= playermoved.speedX;
                    //Setup bullet's speeds
                    if (playermoved.speedX >= 0)
                    {
                        bullet.speedX= (float)(playermoved.speedX + Math.Sin((Math.PI / 180) * playermoved.angle) * MatchConfig.bulletSpeed);
                    }
                    else
                    {
                        bullet.speedX= (float)(playermoved.speedX + Math.Sin((Math.PI / 180) * playermoved.angle) * MatchConfig.bulletSpeed);
                    }
                    if (playermoved.speedY >= 0)
                    {
                        bullet.speedY= ((float)(playermoved.speedY + (-1 * (Math.Cos((Math.PI / 180) * playermoved.angle))) * MatchConfig.bulletSpeed));
                    }
                    else
                    {
                        bullet.speedY= ((float)(playermoved.speedY + (-1 * (Math.Cos((Math.PI / 180) * playermoved.angle))) * MatchConfig.bulletSpeed));
                    }
                    //Setup bullet location
                    bullet.x = (float)(playermoved.x + Math.Sin((Math.PI / 180) * playermoved.angle) * (playermoved.radius + 10));
                    bullet.y = (float)(playermoved.y + (-1 * (Math.Cos((Math.PI / 180) * playermoved.angle))) * (playermoved.radius + 10));
                    //Add who shot the bullet
                    bullet.whoshot = playermoved.id;
                    //Increase the overall bullet count (used for ids)
                    bulletcount++;
                    bullet.id = bulletcount;
                    //Add the bullet to the list
                    bulletList.Add(bullet);
                }

            }//End shoot command
            //Return the player action
            return msgToSend;
        }//End player action
    }//End GameServerForm : Form

    /// <summary>
    ///  //The data structure by which the server and the client interact with each other
    /// </summary>
    class Data
    {
        //Default constructor
        public Data()
        {
            this.cmdCommand = Command.Null;
            this.strMessage = null;
            this.strName = null;
        }//End default constructor

        //Converts the bytes into an object of type Data
        /// <summary>
        /// Converts the bytes into an object of type Data
        /// </summary>
        /// <param name="data">Received data</param>
        public Data(byte[] data)
        {
            //The first four bytes are for the Command
            this.cmdCommand = (Command)BitConverter.ToInt32(data, 0);

            //The next four store the length of the name
            this.id = BitConverter.ToInt32(data, 4);

            int nameLen = BitConverter.ToInt32(data, 8);
            //The next four store the length of the message
            int msgLen = BitConverter.ToInt32(data, 12);

            //This check makes sure that strName has been passed in the array of bytes
            if (nameLen > 0)
                this.strName = Encoding.UTF8.GetString(data, 16, nameLen);
            else
                this.strName = null;

            //This checks for a null message field
            if (msgLen > 0)
                this.strMessage = Encoding.UTF8.GetString(data, 16 + nameLen, msgLen);
            else
                this.strMessage = null;
        }//End data constructor

        /// <summary>
        /// Converts the Data structure into an array of bytes
        /// </summary>
        /// <param name="playerList">A list that contains the players instance</param>
        /// <param name="bulletList">A list that contains the Bullets instance</param>
        /// <param name="removeids">A list that contains the id of the object removed</param>
        /// <param name="playercount">The number of player currently in the game</param>
        /// <param name="planetList">A list that contains the Planets instance</param>
        ///  
        public byte[] ToByte(List<Player> playerList, List<Bullet> bulletList, List<int> removeids, int playercount, List<Planets> planetList)
        {
            List<byte> result = new List<byte>();

            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)cmdCommand));

            //Add how many players there are (for client)
            result.AddRange(BitConverter.GetBytes(playercount));


            result.AddRange(BitConverter.GetBytes((int)id));

            //Next add locations of every player
            foreach (Player p in playerList)
            {   //May need names/angles later.
                //Add the length of the name
                result.AddRange(BitConverter.GetBytes((int)p.x));
                result.AddRange(BitConverter.GetBytes((int)p.y));
                result.AddRange(BitConverter.GetBytes((int)p.angle));

            }

            result.AddRange(BitConverter.GetBytes((int)planetList.Count));
            foreach (Planets planet in planetList)
            {
                result.AddRange(BitConverter.GetBytes((int)planet.x));
                result.AddRange(BitConverter.GetBytes((int)planet.y));
            }

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
            else
                result.AddRange(BitConverter.GetBytes(0));

            //And, lastly we add the message text to our array of bytes
            if (strMessage != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));
            else
                result.AddRange(BitConverter.GetBytes(0));
            
            result.AddRange(BitConverter.GetBytes((int)bulletList.Count));
            for (int j = 0; j < (int)bulletList.Count; j++)//Bullet b in bulletList
            {
                //TODO:angle of bullet if image is pointed
                result.AddRange(BitConverter.GetBytes((int)bulletList[j].x));
                result.AddRange(BitConverter.GetBytes((int)bulletList[j].y));

            }


            return result.ToArray();
        }//End ToByte

        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public int id;
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }//End class data
}//End Server Namespace