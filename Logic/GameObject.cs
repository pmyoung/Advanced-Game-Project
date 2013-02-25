using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains information relating to all game objects (Ships, bullets, planets, etc). 
//This class is not used alone; It is exteneded by other game objects.

namespace SpaceShip.Logic
{
    abstract class  GameObject
    {
        public float x;
        public float y;
        public float angle;
        public int radius;
        public int mass;
        public int spriteID;

        public float speedX;
        public float speedY;

        public void updatePosition()
        {
            x += speedX;
            y += speedY;
        }

        //Checks if two objects have collided, returning true if they have or false if they haven't
        //Collision detection is based upon the radius of the objects and the distance between them
        public bool checkCollision(GameObject object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2)));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

        public void outOfBounds()
        //If an object moves out of the world boundary, switch it's direction of motion.
        //Bullets should be an exception to this rule (Override function?) 
        //as they should be destroyed when reaching the edge of the world
        {
            //if ((x < MatchConfig.windowBoundaryXmin && speedX < 0) || (x > MatchConfig.windowBoundaryXmax && speedX > 0))
            if ((x < 0 && speedX < 0) || (x > MatchConfig.map.mapWidth && speedX > 0))
            {
                this.speedX = this.speedX * (-1);
            }
            //if ((y < MatchConfig.windowBoundaryYmin && speedY < 0) || (y > MatchConfig.windowBoundaryYmax && speedY > 0))
            if ((y < 0 && speedY < 0) || (y > MatchConfig.map.mapHeigth && speedY > 0))
            {
                this.speedY = this.speedY * (-1);
            }
        }

        //Modify acceleration rates of game objects based on gravity effect
        public void calculateGravity()
        {
            List<GameObject> objectsG =  MatchConfig.map.getGravityObjects();
            float G = MatchConfig.gravityConstant;

            foreach (GameObject ob in objectsG)
            {
                if(!ob.Equals(this))
                {
                    speedX += (float)((G * ob.mass) / Math.Pow(this.x - ob.x, 2));
                    speedY += (float)((G * ob.mass) / Math.Pow(this.y - ob.y, 2));
                }

                
            }
        }
    }
}
