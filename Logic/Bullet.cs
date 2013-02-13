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
        
        public Bullet(float bX,float bY,float bSpeedX,float bSpeedY, int playerId, int spriteId)
        {
            this.x = bX;
            this.y = bY;
            this.speedX = bSpeedX;
            this.speedY = bSpeedY;
            this.playerId = playerId;
            setSpriteId(spriteId);
            GameScene.getInstance().addNonGravityObject(this);
        }
         
        private void destroy()
        {
        	
        }
          
  	public void outOfBounds()
	{
		//if the bullet moves outside of the world boundary, destroy it.  
		//This needs to override the outOfBounds function for regular gameobjects
		if ( x < MatchConfig.windowBoundaryXmin || x > MatchConfig.windowBoundaryXmax ) 
            	{
			this.destroy();
		}
            	if ( y < MatchConfig.windowBoundaryYmin || y > MatchConfig.windowBoundaryYmax ) 
            	{
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
