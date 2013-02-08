/*
 * @author Patrick Young
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameGraphics
{
    class SpriteStore
    {
        private static int SPRITE_ERROR = 0;
        private Dictionary<int, Sprite> spriteDictionary;

        /*
         * This method uses the games content manager to load all the sprites
         * used in the game. This is to be called in the LoadContent method
         * 
         * @param
         * Content: the content manager used by the games LoadContent method
         */
        public void LoadSprites(ContentManager Content)
        {
            spriteDictionary = new Dictionary<int, Sprite>();

            // temporary loading
            spriteDictionary.Add(0, new Sprite(Content.Load<Texture2D>("test"), "Test"));
            spriteDictionary.Add(1, new Sprite(Content.Load<Texture2D>("ship_test_32x32"), "Ship"));
        }// LoadSprites

        /*
         * Returns a sprite based on the given ID
         * 
         * @param
         * id: The ID of the sprite requested
         */
        public Sprite GetSprite(int id)
        {
            Sprite sprite;

            spriteDictionary.TryGetValue(id, out sprite);

            return sprite;
        }// GetSprite

    }// SpriteStore Class
}
