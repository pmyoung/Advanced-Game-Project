using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameGraphics
{
    public class Particle : AbstractGraphicEntity
    {
        private static float LIFE_TIME = 500;

        private float velocity = 0.075f;
        private float ttl;

        public Particle(float x, float y, float radius, float angle, int spriteID)
            : base(x, y, radius, angle, spriteID)
        {
            this.SetTTL(LIFE_TIME);
        }

        public Particle(float x, float y, float radius, float angle, int spriteID, Color color)
            : base(x, y, radius, angle, spriteID, color)
        {
            this.SetTTL(LIFE_TIME);
        }

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
            float rotation = (float)((Math.PI / 180.0) * this.GetAngle());
            float dirX = (float)Math.Sin(rotation);
            float dirY = (float)Math.Cos(rotation);

            this.SetX(this.GetX() + (this.GetVelocity() * dirX * delta));
            this.SetY(this.GetY() - (this.GetVelocity() * dirY * delta));
        }

        public Boolean isAlive()
        {
            return (this.ttl > 0);
        }

        public void SetVelocity(float velocity)
        {
            this.velocity = velocity;
        }

        public float GetVelocity()
        {
            return this.velocity;
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
