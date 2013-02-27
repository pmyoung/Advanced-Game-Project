using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameGraphics
{
    /**
     * @file
     * @author Patrick Young
     * @version
     * 
     * @section DESCRIPTION
     */
    public class WorldGraphics
    {
        private GraphicsModel model;
        private SpriteStore store;

        /**
         * Renders the game world this method is called during the games
         * Draw method.
         * 
         * @param spriteBatch Contains graphics contexts for rendering on the game screen
         */
        public void Render(SpriteBatch spriteBatch)
        {
            // currently ignores if the object can actually be seen or not
            List<GraphicsObject> list = model.GetAsList();
            for (int i = 0; i < list.Count; i++)
            {
                this.GetSpriteStore().GetSprite(list[i].GetSpriteID()).Render(spriteBatch, list[i]);
            }
        }// Render

        /**
         * Sets the GraphicsModel that this class will reference and use
         * to render the game world.
         * 
         * @param model The GraphicsModel object that this class will use 
         */
        public void SetGraphicsModel(GraphicsModel model)
        {
            this.model = model;
        }// SetGraphicsModel

        /**
         * Sets the SpriteStore object that will hold all the sprites
         * WorldGraphics will reference.
         * 
         * @param store The SpriteStore object to be used
         */
        public void SetSpriteStore(SpriteStore store)
        {
            this.store = store;
        }// SetSpriteStore

        /**
         * Returns the GraphicsModel object that this class is using
         */
        public GraphicsModel GetGraphicsModel()
        {
            return this.model;
        }// GetGraphicsModel

        /**
         * Gets the SpriteStore object that this class uses when rendering.
         */
        public SpriteStore GetSpriteStore()
        {
            return this.store;
        }// GetSpriteStore
    }// WorldGraphics Class
}
