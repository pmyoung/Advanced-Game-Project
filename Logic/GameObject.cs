using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            double distance = Math.Sqrt(Math.Pow((this.x - object2.x), 2) + Math.Pow(this.y - object2.y, 2));
            if (distance < this.radius + object2.radius) return true;
            return false;
        }

        public void calculateGravityResultant()
        {
            double acelleration_X = 0;
            double acelleration_Y = 0;

            List<GameObject> objectsG = GameScene.getInstance().getObjectsG();
            double G = GameScene.getInstance().getConfig().getGravityConstant();

            foreach (GameObject ob in objectsG){
                acelleration_X += (G*ob.mass*this.mass)/Math.Pow(this.x-ob.x,2);
                acelleration_Y += (G*ob.mass*this.mass)/Math.Pow(this.y-ob.y,2);
            }
        }
    }
}
