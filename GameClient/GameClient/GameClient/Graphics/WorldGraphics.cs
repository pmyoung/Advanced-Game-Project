using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameGraphics
{
    /// <file></file>
    /// <author>Patrick Young</author>
    /// <version></version>
    /// <summary>
    /// 
    /// </summary>
    public class WorldGraphics
    {
        private GraphicsModel model;
        private SpriteStore store;

        public void Update(GameTime gametime)
        {
            List<GraphicsObject> list = model.GetAsList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].GetParticleSet().Update(gametime);
            }
        }

        ///<summary>Renders the game world this method is called during the games
        /// Draw method.</summary>
        /// <param name="spriteBatch">Contains graphics contexts for rendering on the game screen</param>
        public void Render(SpriteBatch spriteBatch)
        {
            // currently ignores if the object can actually be seen or not
            List<GraphicsObject> list = model.GetAsList();
            for (int i = 0; i < list.Count; i++)
            {
                ParticleSet ps = list[i].GetParticleSet();
                for (int p = 0; p < ParticleSet.NUM_PARTICLES; p++)
                {
                    if (ps.GetParticle(p) != null)
                    {
                        if (ps.GetParticle(p).isAlive())
                        {
                            this.GetSpriteStore().GetSprite(ps.GetParticle(p).GetSpriteID()).Render(spriteBatch, ps.GetParticle(p));
                        }
                    }
                }
                this.GetSpriteStore().GetSprite(list[i].GetSpriteID()).Render(spriteBatch, list[i]);
            }
        }// Render

        ///<summary>Sets the GraphicsModel that this class will reference and use
        /// to render the game world.</summary>
        /// <param name="model">The GraphicsModel object that this class will use</param>
        public void SetGraphicsModel(GraphicsModel model)
        {
            this.model = model;
        }// SetGraphicsModel

        ///<summary>Sets the SpriteStore object that will hold all the sprites
        /// WorldGraphics will reference.</summary>
        /// <param name="store">The SpriteStore object to be used</param>
        public void SetSpriteStore(SpriteStore store)
        {
            this.store = store;
        }// SetSpriteStore

        ///<summary>Returns the GraphicsModel object that this class is using</summary>
        ///<returns>Returns the GraphicsModel object that is being referenced</returns>
        public GraphicsModel GetGraphicsModel()
        {
            return this.model;
        }// GetGraphicsModel

        ///<summary>Gets the SpriteStore object that this class uses when rendering.</summary>
        ///<returns>Returns the SpriteStore object that is being referenced</returns>
        public SpriteStore GetSpriteStore()
        {
            return this.store;
        }// GetSpriteStore
    }// WorldGraphics Class
}
