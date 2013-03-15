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
    
    //Bullet class (To be moved out and abstracted later)
    /// <summary>
    /// This class represents a bullet fired by a ship .
    /// </summary>
    class Bullet : GameObject
    {
        float x;
        float y;
        int id;
        int ownedby;
        int radius;
        float angle;
        float speedy;
        float speedx;

        //Get and sets for all variables
        public int bulletid
        {
            get { return id; }
            set { id = value; }
        }
        public float bulletx
        {
            get { return x; }
            set { x = value; }
        }
        public float bullety
        {
            get { return y; }
            set { y = value; }
        }
        public int whoshot
        {
            get { return ownedby; }
            set { ownedby = value; }
        }
        public int bulletradius
        {
            get { return radius; }
            set { radius = value; }
        }
        public float bulletangle
        {
            get { return angle; }
            set { angle = value; }
        }
        public float bulletspeedy
        {
            get { return speedy; }
            set { speedy = value; }
        }
        public float bulletspeedx
        {
            get { return speedx; }
            set { speedx = value; }
        }

        //Collision detection
        /// <summary>
        /// Check the collision between the bullet and a GameObject instance
        /// </summary>
        /// <param name="object2">An instance to a GameObject</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(GameObject object2)
        {
            if (this == object2) return false;
            float distance = (float)(Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2)));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

        /// <summary>
        /// Check the collision between the bullet and a player instance
        /// </summary>
        /// <param name="object2">An instance to a player</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(player object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.bulletx - object2.playerx), 2) + Math.Pow(this.bullety - object2.playery, 2)));
            if (distance < this.bulletradius + object2.playerradius) return true;
            return false;
        }

        /// <summary>
        /// Check the collision between the bullet and a Planets instance
        /// </summary>
        /// <param name="object2">An instance to a Planets</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(Planets object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.bulletx - object2.planetx), 2) + Math.Pow(this.bullety - object2.planety, 2)));
            if (distance < this.bulletradius + object2.planetradius) return true;
            return false;
        }

        /// <summary>
        /// Check the collision between the bullet and a Bullet instance
        /// </summary>
        /// <param name="object2">An instance to a Bullet</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(Bullet object2)
        {
            if (this == object2) return false;
            float distance = (float)(Math.Sqrt(Math.Pow((this.bulletx - object2.bulletx), 2) + Math.Pow(this.bullety - object2.bullety, 2)));
            if (distance < this.bulletradius + object2.bulletradius) return true;
            return false;
        }

        public override void treatCollision(GameObject object2)
        { }
        public override void treatCollision(Bullet object2)
        { }
        public override void treatCollision(Planets object2)
        { }
        public override void treatCollision(player object2)
        { }
    }

    /// <summary>
    /// This class represents a player's ship including the ships variables.
    /// </summary>
    class player : GameObject
    {
        float x;
        float y;
        int id;
        float angle;
        int radius;
        float speedy;
        float speedx;
        string name;
        int bulletcount;
        //Get and sets for all variables
        public float playerx
        {
            get { return x; }
            set { x = value; }
        }
        public int bcount
        {
            get { return bulletcount; }
            set { bulletcount = value; }
        }

        public float playerangle
        {
            get { return angle; }
            set { angle = value; }
        }

        public int playerradius
        {
            get { return radius; }
            set { radius = value; }
        }

        public float playery
        {
            get { return y; }
            set { y = value; }
        }

        public float playerspeedy
        {
            get { return speedy; }
            set { speedy = value; }
        }

        public float playerspeedx
        {
            get { return speedx; }
            set { speedx = value; }
        }

        public string playername
        {
            get { return name; }
            set { name = value; }
        }
        public int playerid
        {
            get { return id; }
            set { id = value; }
        }

        //Collision detection (to be modified for explosions later)
        /// <summary>
        /// Check the collision between the ship and a GameObject instance
        /// </summary>
        /// <param name="object2">An instance to a GameObject</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(GameObject object2)
        {
            if (this == object2) return false;
            float distance = (float)(Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2)));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }
        
        /// <summary>
        /// Check the collision between the ship and a player instance
        /// </summary>
        /// <param name="object2">An instance to a player</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(player object2)
        {
            if (this == object2) return false;
            float distance = (float)(Math.Sqrt(Math.Pow((this.playerx - object2.playerx), 2) + Math.Pow(this.playery - object2.playery, 2)));
            if (distance < this.playerradius + object2.playerradius) return true;
            return false;
        }
        
        /// <summary>
        /// Check the collision between the ship and a Planets instance
        /// </summary>
        /// <param name="object2">An instance to a Planets</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(Planets object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.playerx - object2.planetx), 2) + Math.Pow(this.playery - object2.planety, 2)));
            if (distance < this.playerradius + object2.planetradius) return true;
            return false;
        }
        
        /// <summary>
        /// Check the collision between the ship and a Bullet instance
        /// </summary>
        /// <param name="object2">An instance to a Bullet</param>
        /// <returns>Bool, true if them colide, false otherwise</returns>
        public override bool checkCollision(Bullet object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.playerx - object2.bulletx), 2) + Math.Pow(this.playery - object2.bullety, 2)));
            if (distance < this.playerradius + object2.bulletradius) return true;
            return false;
        }
        public override void treatCollision(GameObject object2)
        {}
        public override void treatCollision(Bullet object2)
        {}
        
        /// <summary>
        /// In case a collison is detected, this function should be called to treat how a player instance will respond in collison with another object
        /// </summary>
        /// <param name="object2">An instance to a Planets</param>
        public override void treatCollision(Planets object2)
        {
            if (object2 is Planets)
            {
                this.speedx = this.speedx * (float)(-0.25);
                this.speedy = this.speedy * (float)(-0.25);
            }
        }
        
        /// <summary>
        /// In case a collison is detected, this function should be called to treat how a player instance will respond in collison with another object
        /// </summary>
        /// <param name="object2">An instance to a player</param>
        public override void treatCollision(player object2)
        {
            if (object2 is player)
            {
                //Give object that is being collided with the same speed but add the colliders speed to it to simulate a bounce.
                object2.playerspeedx = object2.playerspeedx + (this.playerspeedx * (float)(0.8));
                object2.playerspeedy = object2.playerspeedy + (this.playerspeedy * (float)(0.8));
                //make the colliders' speed the reverse because of collision.
                this.playerspeedx = this.playerspeedx * (float)(-0.25);
                this.playerspeedy = this.playerspeedy * (float)(-0.25);
                
            }

        }
    }

    //Planets (to be moved out later)
    /// <summary>
    /// This class represents a Planets that it would be not affected by gravity and collisions with other objects.
    /// </summary>
    class Planets : GameObject
    {
        float x;
        float y;
        int id;
        int radius;
        int mass;
        //Get and sets for all variables
        public int planetid
        {
            get { return id; }
            set { id = value; }
        }
        public float planetx
        {
            get { return x; }
            set { x = value; }
        }
        public float planety
        {
            get { return y; }
            set { y = value; }
        }
        public int planetradius
        {
            get { return radius; }
            set { radius = value; }
        }
        public int planetmass
        {
            get { return mass; }
            set { mass = value; }
        }
        //Collision detection
        public override bool checkCollision(GameObject object2)
        { return false; }
        public override bool checkCollision(player object2)
        { return false; }
        public override bool checkCollision(Planets object2)
        { return false; }
        public override bool checkCollision(Bullet object2)
        { return false; }
        public override void treatCollision(GameObject object2)
        { }
        public override void treatCollision(player object2)
        { }
        public override void treatCollision(Bullet object2)
        { }
        public override void treatCollision(Planets object2)
        { }

        /// <summary>
        /// This function changes the speed of the object based on its proximity to its Planet instance
        /// </summary>
        /// <param name="p">An instance to a player</param>
        public void applyGravity(player p)
        {
            //Gravity Calculations between players and the planet(object producing the gravity).
            float G = MatchConfig.gravityConstant;

            double distanceG = Math.Sqrt((Math.Pow(p.playerx - this.planetx, 2) + Math.Pow(p.playery - this.planety, 2)));
            double speedVector = (float)((G * this.planetmass) / Math.Pow(distanceG, 2));
            double deltaY = (p.playery - this.planety);
            double deltaX = (p.playerx - this.planetx);
            double angleDegree = Math.Atan(deltaY / deltaX);// * 180 / Math.PI;
            //float rad = (float)(Math.PI / 180) * o.angle;
            if (p.playerx != this.planetx)
            {
                //speedX += (float)(Math.Cos(angleDegree) * speedVector);
                if (p.playerx > this.planetx)
                    p.playerspeedx -= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                else
                    p.playerspeedx += (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
            }
            if (p.playery != this.planety)
            {
                //speedY += (float)(Math.Sin(angleDegree) * speedVector);
                if (p.playery < this.planety)
                    p.playerspeedy += (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                else
                    p.playerspeedy -= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
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

            double distanceG = Math.Sqrt((Math.Pow(b.bulletx - this.planetx, 2) + Math.Pow(b.bullety - this.planety, 2)));
            double speedVector = (float)((G * this.planetmass) / Math.Pow(distanceG, 2));
            double deltaY = (b.bullety - this.planety);
            double deltaX = (b.bulletx - this.planetx);
            double angleDegree = Math.Atan(deltaY / deltaX);// * 180 / Math.PI;
            //float rad = (float)(Math.PI / 180) * o.angle;
            if (b.bulletx != this.planetx)
            {
                //speedX += (float)(Math.Cos(angleDegree) * speedVector);
                if (b.bulletx > this.planetx)
                    b.bulletspeedx -= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                else
                    b.bulletspeedx += (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
            }
            if (b.bullety != this.planety)
            {
                //speedY += (float)(Math.Sin(angleDegree) * speedVector);
                if (b.bullety < this.planety)
                    b.bulletspeedy += (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                else
                    b.bulletspeedy -= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
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
        List<player> playerList = new List<player>();
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
            player playerbullet = null;
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
                    planet.planetx = 400;
                    planet.planety = 300;
                }
                //Update players
                foreach (player p in playerList)
                {
                    //Adjust placement by speed
                    p.playerx += p.playerspeedx;
                    p.playery += p.playerspeedy;

                    //Collision Detection between Player and Border. For now, Players will bounce off the border in opposite direction.
                    if (p.playerx < 0)
                    {
                        p.playerspeedx = p.playerspeedx * (float)(-0.1);
                        p.playerx = 0;
                    }
                    else if (p.playerx > MatchConfig.mapWidth)
                    {
                        p.playerspeedx = p.playerspeedx * (float)(-0.1);
                        p.playerx = MatchConfig.mapWidth;
                    }
                    if (p.playery < 0)
                    {
                        p.playerspeedy = p.playerspeedy * (float)(-0.1);
                        p.playery = 0;
                    }
                    else if (p.playery > MatchConfig.mapHeight)
                    {
                        p.playerspeedy = p.playerspeedy * (float)(-0.1);
                        p.playery = MatchConfig.mapHeight;
                    }
                    
                    //Collision Detection between Players. For now, Players will bounce off of each other. No damage to players.
                    foreach (player p2 in playerList)
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
                    foreach (player playerp in playerList)
                    {
                        if (playerp.playerid == b.whoshot)
                        {
                            playerbullet = playerp;
                        }
                    }

                    //Bullet Movement
                    b.bulletx += b.bulletspeedx;
                    b.bullety += b.bulletspeedy;
                    
                    //Collision Detection between bullets and border. At this time, bullets will disappear after they reach the border.
                    if (b.bulletx < 0)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    else if (b.bulletx > MatchConfig.mapWidth)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    if (b.bullety < 0)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }
                    else if (b.bullety > MatchConfig.mapHeight)
                    {
                        playerbullet.bcount--;
                        toRemove.Add(b);
                    }

                    //Collision Detection between players and bullets. For now, the bullets will disappear when they collide with players. Players will remain.
                    foreach (player p in playerList)
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
                            player player1 = new player();
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.endpoint = epSender;
                            clientInfo.strName = msgReceived.strName;
                            msgToSend.cmdCommand = Command.Login;
                            msgToSend.playerID = playercount;
                            clientList.Add(clientInfo);
                            player1.playerx = 100;
                            player1.playery = 100;
                            player1.playerradius = 16;
                            player1.playername = msgReceived.strName;
                            player1.playerid = playercount;
                            playerList.Add(player1);
                            playercount = playercount + 1;

                            //Setup planets (1 for now)
                            Planets planet = new Planets();
                            planet.planetx = 400;
                            planet.planety = 300;
                            planet.planetradius = 32;
                            planet.planetmass = 150;
                            planetList.Add(planet);
                            planetcount = 1;

                            //Set the text of the message that we will broadcast to all users (doesn't currently do much)
                            msgToSend.strMessage = "<<<" + msgReceived.strName + " has joined the room>>>";
                            msgToSend.cmdCommand = Command.Login;

                        }
                        else
                        {
                            //Setup player 2
                            player player2 = new player();
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.endpoint = epSender;
                            clientInfo.strName = msgReceived.strName;
                            msgToSend.playerID = playercount;
                            player2.playerid = playercount;
                            clientList.Add(clientInfo);
                            player2.playerx = 700;
                            player2.playery = 500;
                            player2.playerradius = 16;
                            player2.playername = msgReceived.strName;
                            player2.playerid = playercount;
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
            player playermoved = null;
            double accelerate = 0;
            int rot = 0;

            //Figure out who is making the action.
            foreach (player playerp in playerList)
            {
                if (playerp.playerid == msgReceived.playerID)
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
                playermoved.playerangle += rot;

            }
            //Rotate left
            if (msgReceived.strMessage == "left")
            {
                rot++;
                rot = rot * MatchConfig.rotationRate;
                playermoved.playerangle -= rot;

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
                float rad = (float)(Math.PI / 180) * playermoved.playerangle;
                playermoved.playerspeedx += (float)((float)Math.Sin(rad) * accelerate);
                playermoved.playerspeedy += -1 * (float)((float)Math.Cos(rad) * accelerate);
                //Note we don't actually increase the x or y here, just the speed.
            }
            //Decrease speed
            if (msgReceived.strMessage == "down")
            {
                accelerate++;
                accelerate = accelerate * MatchConfig.brakeRate;
                float rad = (float)(Math.PI / 180) * playermoved.playerangle;
                playermoved.playerspeedx -= (float)((float)Math.Sin(rad) * accelerate);
                playermoved.playerspeedy -= -1 * (float)((float)Math.Cos(rad) * accelerate);
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
                    bullet.bulletspeedx = playermoved.playerspeedx;
                    //Setup bullet's speeds
                    if (playermoved.playerspeedx >= 0)
                    {
                        bullet.bulletspeedx = (float)(playermoved.playerspeedx + Math.Sin((Math.PI / 180) * playermoved.playerangle) * MatchConfig.bulletSpeed);
                    }
                    else
                    {
                        bullet.bulletspeedx = (float)(playermoved.playerspeedx + Math.Sin((Math.PI / 180) * playermoved.playerangle) * MatchConfig.bulletSpeed);
                    }
                    if (playermoved.playerspeedy >= 0)
                    {
                        bullet.bulletspeedy = ((float)(playermoved.playerspeedy + (-1 * (Math.Cos((Math.PI / 180) * playermoved.playerangle))) * MatchConfig.bulletSpeed));
                    }
                    else
                    {
                        bullet.bulletspeedy = ((float)(playermoved.playerspeedy + (-1 * (Math.Cos((Math.PI / 180) * playermoved.playerangle))) * MatchConfig.bulletSpeed));
                    }
                    //Setup bullet location
                    bullet.bulletx = (float)(playermoved.playerx + Math.Sin((Math.PI / 180) * playermoved.playerangle) * (playermoved.playerradius + 10));
                    bullet.bullety = (float)(playermoved.playery + (-1 * (Math.Cos((Math.PI / 180) * playermoved.playerangle))) * (playermoved.playerradius + 10));
                    //Add who shot the bullet
                    bullet.whoshot = playermoved.playerid;
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
            this.playerID = BitConverter.ToInt32(data, 4);

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
        public byte[] ToByte(List<player> playerList, List<Bullet> bulletList, List<int> removeids, int playercount, List<Planets> planetList)
        {
            List<byte> result = new List<byte>();

            //First four are for the Command
            result.AddRange(BitConverter.GetBytes((int)cmdCommand));

            //Add how many players there are (for client)
            result.AddRange(BitConverter.GetBytes(playercount));


            result.AddRange(BitConverter.GetBytes((int)playerID));

            //Next add locations of every player
            foreach (player p in playerList)
            {   //May need names/angles later.
                //Add the length of the name
                result.AddRange(BitConverter.GetBytes((int)p.playerx));
                result.AddRange(BitConverter.GetBytes((int)p.playery));
                result.AddRange(BitConverter.GetBytes((int)p.playerangle));

            }

            result.AddRange(BitConverter.GetBytes((int)planetList.Count));
            foreach (Planets planet in planetList)
            {
                result.AddRange(BitConverter.GetBytes((int)planet.planetx));
                result.AddRange(BitConverter.GetBytes((int)planet.planety));
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
                result.AddRange(BitConverter.GetBytes((int)bulletList[j].bulletx));
                result.AddRange(BitConverter.GetBytes((int)bulletList[j].bullety));

            }


            return result.ToArray();
        }//End ToByte

        public string strName;      //Name by which the client logs into the room
        public string strMessage;   //Message text
        public int playerID;
        public Command cmdCommand;  //Command type (login, logout, send message, etcetera)
    }//End class data
}//End Server Namespace