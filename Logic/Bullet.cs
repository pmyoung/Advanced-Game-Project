using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains player data
//Modifies player coordinates based on the keys depressed by the player

namespace SpaceShip
{
    class Bullet:GameObject
    {
        int playerId;
        
        public Bullet(float bX,float bY,float bSpeedX,float bSpeedY)
        {
            this.x = bX;
            this.y = bY;
            this.speedX = bSpeedX;
            this.speedY = bSpeedY;

            GameScene.getInstance().addNonGravityObject(this);
        }
         
        public Bullet(int playerId, int spriteId)
        {
            this.playerId = playerId;
            setSpriteId(spriteId);
        }

        public void travel()
        {
             //will run whenever the game is running and a bullet exist
             double bulletSpeed = GameScene.getInstance().getConfig().getBulletSpeed();
             x = x + (int) (Math.Sin(angle) * bulletSpeed);
             y = y + (int) (Math.Cos(angle) * bulletSpeed);
        }
  	public void outOfBounds()
	{
		if ( x < 0 || x > 800 ) 
            	{ //change these to global variables for map boundary
			this.destroy();
		}
            	if (y < 0 || y > 600) 
            	{ //change these to global variables for map boundary
			this.destroy();
		}
	}
	public void hit()
	{
		//check collision with players
        	//add score to player whom shot bullet
        	this.destroy();
	}
    }
}
