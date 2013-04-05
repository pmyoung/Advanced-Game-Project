using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameGraphics
{
    public class ParticleSet
    {
        private int index = 0;
        private int numParticles = 100;
        private Particle[] particleList;

        public ParticleSet()
        {
            particleList = new Particle[numParticles];
        }

        public void Update(GameTime gametime)
        {
            for (int p = 0; p < numParticles; p++)
            {
                
            }
        }

        public void SetNumParticles(int numParticles)
        {
            this.numParticles = numParticles;
        }

        public int GetNumParticles()
        {
            return this.numParticles;
        }
    }
}
