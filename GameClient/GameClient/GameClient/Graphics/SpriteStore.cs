/// <file>SpriteStore.cs</file>
/// <author>Patrick Young</author>
/// <summary>
/// 
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameGraphics
{
    public class SpriteStore
    {
        private static int SPRITE_ERROR = 0;
        private Dictionary<int, Sprite> dictionary;

        ///<summary>This method uses the games content manager to load all the sprites
        /// used in the game. This is to be called in the LoadContent method</summary>
        ///<param name="Content">Content the content manager used by the games LoadContent method</param>
        public void LoadSprites(ContentManager Content)
        {
            dictionary = new Dictionary<int, Sprite>();

            // temporary loading
            dictionary.Add(0, new Sprite(Content.Load<Texture2D>("sprite_error_8x8"), 8, 8, "error"));
            dictionary.Add(1, new Sprite(Content.Load<Texture2D>("ship1_32x32"), 32, 32, "ship"));
            dictionary.Add(101, new Sprite(Content.Load<Texture2D>("bullet1_8x8"), 8, 8, "bullet"));
            dictionary.Add(102, new Sprite(Content.Load<Texture2D>("bullet2_8x8"), 8, 8, "bullet"));
            dictionary.Add(201, new Sprite(Content.Load<Texture2D>("planet1_64x64"), 64, 64, "planet"));
            dictionary.Add(301, new Sprite(Content.Load<Texture2D>("moon1_32x32"), 32, 32, "moon"));
        }// LoadSprites

        ///<summary>Returns a sprite based on the given ID.</summary>
        /// <param name="id">The ID of the sprite requested</param>
        /// <returns>If the ID exists
        /// the sprite that uses the ID is returned. Otherwise we send
        /// the error sprite. However if the error sprite somehow does
        /// not exist for some unknown reason then we send null.</returns>
        public Sprite GetSprite(int id)
        {
            Sprite value;
            if (this.dictionary.TryGetValue(id, out value))
            {
                // we have it
                return value;
            }
            else if (this.dictionary.TryGetValue(SPRITE_ERROR, out value))
            {
                // send the error sprite
                return value;
            }
            else
            {
                // something has gone horribly wrong!
                return null;
            }
        }// GetSprite

    }// SpriteStore Class
}
