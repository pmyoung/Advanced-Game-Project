using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.Logic
{
    class Moon : Planet
    {
        public double angle;

        public Moon(double angle, int spriteID = -1, float x = 0, float y = 0, int radius = 10, int mass = 10, bool fixedPos = true)
        {
            this.angle = angle;
            this.spriteID = spriteID;
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.radius = 0;
            this.mass = mass;
            this.fixedPosition = fixedPos;
            this.speedX = 0;
            this.speedY = 0;
        }
    }
}
