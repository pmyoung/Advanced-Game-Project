using System;
using System.Collections.Generic;
using System.Text;

//SERVERSIDE
//Contains information relating to all game objects (Ships, bullets, planets, etc). 
//This class is not used alone; It is exteneded by other game objects.

namespace Server
{
    /// <summary>
    /// Class GameObject is the master class for all game objects in the Game. 
    /// </summary>
    /// <remarks>All game objects in the game will extend this class and inherit certain properties such as x, y, angle, radius, etc. When we merged with networking this inheritance was lost, team Logic will work to get it back.</remarks>
    abstract class GameObject
    {
        public float x;
        public float y;
        public float angle;
        public int radius;
        public int mass;
        public int spriteID;
        public int id;
        static int nextId = 2;
        public float speedX;
        public float speedY;

        /// <summary>
        /// GameObject() initializes a new ID for every GameObject object which is one more than the previous ID.
        /// </summary>
        /// <remarks>
        /// This sets the IDs so that they will always be unique.
        /// </remarks>
        public GameObject()
        {
            id = nextId;
            nextId++;
        }

        /// <summary>
        /// updatePosition() updates the GameObjects position based off of its current speed.
        /// </summary>
        /// <remarks>
        /// At this time, function is not called. In the future, this method might handle all movement for the GameObjects
        /// </remarks>
        public virtual void updatePosition()
        {
            x += speedX;
            y += speedY;
        }

        //Checks if two objects have collided, returning true if they have or false if they haven't
        //Collision detection is based upon the radius of the objects and the distance between them
        /// <summary>
        /// checkCollision() checks the collision between two GameObjects.
        /// </summary>
        /// <param name="object2">An instance to a GameObject</param>
        /// <returns>True, if first GameObject collided with second GameObject or false, otherwise.</returns>
        /// <remarks>Collision detection is based upon the radius of the objects and the distance between them.</remarks>
        public virtual bool checkCollision(GameObject object2)
        {
            //if (this == object2) return false;
            float distance = (float)(Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2)));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

        public virtual void outOfBounds()
        {
            if (x < 0)
            {
                this.speedX = this.speedX * (float)(-0.1);
                this.x = 0;
            }
            else if (x > MatchConfig.mapWidth)
            {
                this.speedX = this.speedX * (float)(-0.1);
                this.x = MatchConfig.mapWidth;
            }
            if (y < 0)
            {
                this.speedY = this.speedY * (float)(-0.1);
                this.y = 0;
            }
            else if (y > MatchConfig.mapHeight)
            {
                this.speedY = this.speedY * (float)(-0.1);
                this.y = MatchConfig.mapHeight;
            }
        }

        /// <summary>
        /// calculateGravity() modifies acceleration rates of game objects based on gravity effect
        /// </summary>
        public virtual void calculateGravity()
        {
            List<GameObject> objectsG = new List<GameObject>(); //MatchConfig.getGravityObjects();
            float G = MatchConfig.gravityConstant;

            foreach (GameObject ob in objectsG)
            {
                if(!ob.Equals(this))
                {
                    double distance = Math.Sqrt((Math.Pow(this.x - ob.x, 2) + Math.Pow(this.y - ob.y, 2)));
                    double speedVector = (float)((G * ob.mass) / Math.Pow(distance, 2));
                    double deltaY = (this.y - ob.y);
                    double deltaX = (this.x - ob.x);
                    double angleDegree = Math.Atan(deltaY / deltaX);// * 180 / Math.PI;
                    //float rad = (float)(Math.PI / 180) * o.angle;
                    if (this.x != ob.x)
                    {
                        //speedX += (float)(Math.Cos(angleDegree) * speedVector);
                        if (this.x > ob.x)
                            speedX -= (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                        else
                            speedX += (float)Math.Abs((Math.Cos(angleDegree) * speedVector));
                    }
                    if (this.y != ob.y)
                    {
                        //speedY += (float)(Math.Sin(angleDegree) * speedVector);
                        if (this.y < ob.y)
                            speedY += (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                        else
                            speedY -= (float)Math.Abs((Math.Sin(angleDegree) * speedVector));
                    }
                }                
            }
        }
    }
}
