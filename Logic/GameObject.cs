using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains information relating to all game objects (Ships, bullets, planets, etc). 
//This class is not used alone; It is exteneded by other game objects.

namespace SpaceShip
{
    class GameObject
    {
        protected int x;
        protected int y;
        protected int angle;
        protected int mass;
        protected int radius;
        protected int spriteID;

        protected int speedX0;
        protected int speedY0;
        protected int speedX;
        protected int speedY;
        protected int deltaSpeedX;
        protected int deltaSpeedY;

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public int getAngle()
        {
            return this.angle;
        }

        public int getMass()
        {
            return this.mass;
        }

        public int getRadius()
        {
            return this.radius;
        }

        public int getSpriteId()
        {
            return this.spriteID;
        }

        public int getSpeedX0()
        {
            return this.speedX0;
        }

        public int getSpeedY0()
        {
            return this.speedY0;
        }

        public void speedSinc()
        {
            speedX0 = speedX;
            speedY0 = speedY;
        }

        public bool checkCollision(GameObject object2)
        {
            //Checks if two objects have collided, returning true if they have or false if they haven't
            //Collision detection is based upon the radius of the objects and the distance between them
            double distance = Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

      public void outOfBounds()
      //If an object moves out of the world boundary, switch it's direction of motion.
      //Bullets should be an exception to this rule (Override function?) 
      //as they should be destroyed when reaching the edge of the world
	{
		if ((x < MatchConfig.windowBoundaryXmin && speedX < 0) || (x > MatchConfig.windowBoundaryXmax && speedX > 0)) 
            	{
			this.speedX=this.speedX*(-1);
		}
        if ((y < MatchConfig.windowBoundaryYmin && speedY < 0) || (y > MatchConfig.windowBoundaryYmax && speedY > 0)) 
            	{
			this.speedY=this.speedY*(-1);
		}
	}

        public void calculateGravityResultant()
        {
            //Modify acceleration rates of game objects based on gravity effect
            double acceleration_X = 0;
            double acceleration_Y = 0;

            List<GameObject> objectsG = GameScene.getInstance().getObjectsG();
            double G = GameScene.getInstance().getConfig().getGravityConstant();

            foreach (GameObject ob in objectsG){
                acceleration_X += (G*ob.mass*this.mass)/Math.Pow(this.x-ob.x,2);
                acceleration_Y += (G*ob.mass*this.mass)/Math.Pow(this.y-ob.y,2);
            }
        }
    }
}








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
    class GameObject
    {
        protected float x;
        protected float y;
        protected float angle;
        protected float radius;
        protected int mass;
        protected int spriteID;

        protected float speedX;
        protected float speedY;

        public float getX()
        {
            return this.x;
        }

        public float getY()
        {
            return this.y;
        }

        public float getAngle()
        {
            return this.angle;
        }

        public float getMass()
        {
            return this.mass;
        }

        public float getRadius()
        {
            return this.radius;
        }

        public int getSpriteId()
        {
            return this.spriteID;
        }

        protected void setSpriteId(int sprite)
        {
            this.spriteID = sprite;
        }

        public void updatePosition()
        {
            x += speedX;
            y += speedY;
        }

        public bool checkCollision(GameObject object2)
        {
            float distance = (float)(Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2)));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

        public void calculateGravity()
        {
            List<GameObject> objectsG =  MatchConfig.map.getGravityObjects();
            float G = MatchConfig.gravityConstant;

            foreach (GameObject ob in objectsG)
            {
                speedX += (float)((G * ob.mass * this.mass) / Math.Pow(this.x - ob.x, 2));
                speedY += (float)((G * ob.mass * this.mass) / Math.Pow(this.y - ob.y, 2));
            }
        }
    }
}
