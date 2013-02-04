using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    class Player:GameObject
    {
        int id;
        string name;
 
        public Player(int id, string name, int spriteId)
        {
            this.id = id;
            this.name = name;
            setSpriteId(spriteId);
        }

        public int getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }   

         public void accelerate()
         {
             //called whenever the user has 'up' key depressed
             double accelerationRate = GameScene.getInstance().getConfig().getAccelerationRate();
             speedX += (int) (Math.Sin(angle) * accelerationRate);
             speedY += (int) (Math.Cos(angle) * accelerationRate);
          }
          public void brake()
          {
               //called whenever the user has 'down' key depressed
              double brakeRate = GameScene.getInstance().getConfig().getBrakeRate();
              speedX = (int)(speedX * brakeRate);
              speedX = (int)(speedX * brakeRate);
          }
          public void rotateLeft()
          {

              angle -= GameScene.getInstance().getConfig().getRotationRate();
          }
          public void rotateRight()
          {
              angle += GameScene.getInstance().getConfig().getRotationRate();
          }
    }
}
