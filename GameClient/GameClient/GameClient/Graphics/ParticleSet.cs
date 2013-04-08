using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameGraphics
{
    public class ParticleSet
    {
        public static int NUM_PARTICLES = 100;

        private int index;
        private Particle[] particleList;

        public ParticleSet()
        {
            this.SetIndex(0);
            particleList = new Particle[NUM_PARTICLES];
        }

        public void AddParticle(Particle p)
        {
            particleList[index] = p;
            index = (index + 1) % NUM_PARTICLES;
        }

        public Particle GetParticle(int index)
        {
            if (index < NUM_PARTICLES)
            {
                return this.particleList[index];
            }

            return null;
        }

        public void Update(GameTime gametime)
        {
            for (int p = 0; p < NUM_PARTICLES; p++)
            {
                if (this.particleList[p] != null)
                {
                    this.particleList[p].Update(gametime);
                }
            }
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetIndex()
        {
            return this.index;
        }
    }
}
