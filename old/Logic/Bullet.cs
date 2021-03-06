using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains player data
//Modifies player coordinates based on the keys depressed by the player

namespace SpaceShip.Logic
{
    class Bullet : GameObject
    {
        int playerId;

        public Bullet(float bX, float bY, float bSpeedX, float bSpeedY, int playerId, int spriteId)
        {
            this.x = bX;
            this.y = bY;
            this.speedX = bSpeedX;
            this.speedY = bSpeedY;
            this.playerId = playerId;
            this.spriteID = spriteId;
            MatchConfig.map.addNonGravityObject(this);
        }


        private void destroy()
        {
        }

        public void outOfBounds()
        {
            if (x < 0 || x > MatchConfig.map.mapWidth)
            { //change these to global variables for map boundary
                this.destroy();
            }
            if (y < 0 || y > MatchConfig.map.mapHeigth)
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
