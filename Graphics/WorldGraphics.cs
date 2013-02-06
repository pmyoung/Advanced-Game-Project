/*
 * @author Patrick Young
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Comp4432_AGP
{
    class WorldGraphics
    {
        private SpriteStore store;

        /*
         * Renders the game world this method is called during the games
         * Draw method.
         * 
         * @param
         * spriteBatch: Contains graphics contexts for rendering on the game screen
         */
        public void Render(SpriteBatch spriteBatch)
        {
            // temporary drawing
            this.getSpriteStore().GetSprite(0).Render(spriteBatch, 100, 100, 16, 0.0f, 0.0f, 0, 0);
            this.store.GetSprite(0).Render(spriteBatch, 200, 100, 16, 1.0f, 0.0f, 0, 0);
            this.getSpriteStore().GetSprite(0).Render(spriteBatch, 200, 200, 16, 2.0f, 0.0f, 0, 0);
            this.getSpriteStore().GetSprite(0).Render(spriteBatch, 100, 200, 16, 3.0f, 0.0f, 0, 0);
        }// Render

        /*
         * Sets the SpriteStore object that will hold all the sprites
         * WorldGraphics will reference.
         * 
         * @param
         * store: The SpriteStore object to be used
         */
        public void setSpriteStore(SpriteStore store)
        {
            this.store = store;
        }// setSpriteStore

        /*
         * Gets the SpriteStore object that this class uses when rendering.
         */
        public SpriteStore getSpriteStore()
        {
            return this.store;
        }// getSpriteStore
    }// WorldGraphics Class
}
