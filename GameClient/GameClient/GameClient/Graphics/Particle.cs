using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameGraphics
{
    class Particle : AbstractGraphicEntity
    {
        private float velocityX;
        private float velocityY;
        private float ttl;

        public Particle(float ttl, float x, float y, float radius, float angle, int spriteID)
            : base(x, y, radius, angle, spriteID)
        {
            this.SetTTL(ttl);
        }

        public Particle(float ttl, float x, float y, float radius, float angle, int spriteID, Color color)
            : base(x, y, radius, angle, spriteID, color)
        {
            this.SetTTL(ttl);
        }

        public void Update(GameTime gametime)
        {
            float delta = (float)(gametime.ElapsedGameTime.TotalMilliseconds);
            this.SetTTL(this.GetTTL() - delta);
            this.SetX(this.GetX() + this.GetVelocityX() * delta);
            this.SetY(this.GetY() + this.GetVelocityY() * delta);
        }

        public Boolean isAlive()
        {
            return (this.ttl > 0);
        }

        public void SetVelocityX(float velocityX)
        {
            this.velocityX = velocityX;
        }

        public float GetVelocityX()
        {
            return this.velocityX;
        }

        public void SetVelocityY(float velocityY)
        {
            this.velocityY = velocityY;
        }

        public float GetVelocityY()
        {
            return this.velocityY;
        }

        public void SetTTL(float ttl)
        {
            this.ttl = ttl;
        }

        public float GetTTL()
        {
            return this.ttl;
        }
    }
}
