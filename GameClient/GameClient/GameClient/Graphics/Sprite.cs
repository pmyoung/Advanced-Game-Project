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
    public class Sprite
    {
        private Texture2D sprite;
        private string spriteType;
        private int frameWidth;
        private int frameHeight;

        /// <summary> The Sprite Constructor that requires a Texture2D object (image file), and
        /// the type of object (like ship, planet).</summary>
        /// <param name="sprite">sprite The image that the sprite will use</param>
        /// <param name="frameWidth">The width of each frame of animation on the sprite</param>
        /// <param name="frameHeight">The height of each frame of animation on the sprite</param>
        /// <param name="type">type The type of sprite (like Ship, Planet)</param>
        public Sprite(Texture2D sprite, int frameWidth, int frameHeight, string type)
        {
            this.SetSprite(sprite);
            this.SetSpriteType(type);
            this.setFrameRectangle(frameWidth, frameHeight);
        }// Sprite

        /// <summary>Render method that all overloads call.</summary>
        /// <param name="spriteBatch">Contains graphics contexts for rendering on the game screen</param>
        /// <param name="bounds">The region of the screen the sprite will be drawn at</param>
        /// <param name="color">Changes the color of the sprite</param>
        /// <param name="rotation">The amount to rotate the sprite in radians</param>
        /// <param name="origin">The point on the sprite that is used as the center of rotation</param>
        /// <param name="effect">Chosen sprite mirroring option (like None, FlipHorizontally)</param>
        /// <param name="layer">Represents the depth 0 front of layer 1 back of layer</param>
        /// <param name="action">Integer representing what "action" of the sprite to display</param>
        /// <param name="frame">Integer representing what "frame" of the action animation to display</param>
        public void Render(SpriteBatch spriteBatch, Rectangle bounds, Color color, float rotation, Vector2 origin, SpriteEffects effect, float layer, int action, int frame)
        {
            // the rectangle for where on the sprite sheet will be displayed
            Rectangle source = new Rectangle(frame*this.GetFrameWidth(), action*this.GetFrameHeight(), this.GetFrameWidth(), this.GetFrameHeight());

            spriteBatch.Draw(this.GetSprite(), bounds, source, color, rotation, origin, effect, layer);
        }// Render

        /// <summary>Render this overload allows basic rendering of a sprite that has circular properties.</summary>
        /// <param name="spriteBatch">Contains graphics contexts for rendering on the game screen</param>
        /// <param name="x">The left-right/west-east location of the sprite</param>
        /// <param name="y">The up-down/north-south location of the sprite</param>
        /// <param name="radius">The radius of the "circle"</param>
        /// <param name="color">Changes the color of the sprite</param>
        /// <param name="angle">The angle of the sprite in degrees</param>
        /// <param name="layer">Represents the depth 0 front of layer 1 back of layer</param>
        /// <param name="action">Integer representing what "action" of the sprite to display</param>
        /// <param name="frame">Integer representing what "frame" of the action animation to display</param>
        public void Render(SpriteBatch spriteBatch, int x, int y, int radius, Color color, int angle, float layer, int action, int frame)
        {
            Rectangle bounds = new Rectangle();
            bounds.X = x;
            bounds.Y = y;
            bounds.Width = radius*2;
            bounds.Height = radius*2;

            Vector2 origin = new Vector2(this.GetFrameWidth()/2, this.GetFrameHeight()/2);

            float rotation = (float) (Math.PI * angle / 180.0);

            this.Render(spriteBatch, bounds, color, rotation, origin, SpriteEffects.None, layer, action, frame);
        }// Render

        ///<summary>Renders the given GraphicsObject</summary>
        ///<param name="spriteBatch">Contains graphics contexts for rendering on the game screen</param>
        ///<param name="obj">The AbstractGraphicEntity that is to be rendered</param>
        public void Render(SpriteBatch spriteBatch, AbstractGraphicEntity obj)
        {
            this.Render(spriteBatch, (int)obj.GetX(), (int)obj.GetY(), (int)obj.GetRadius(), obj.GetColor(), (int)obj.GetAngle(), 0.0f, 0, 0);
        }// Render

        ///<summary>Sets the image that this sprite will use</summary>
        ///<param name="sprite">The image that will be used for the sprite</param>
        public void SetSprite(Texture2D sprite)
        {
            this.sprite = sprite;
        }// SetSprite

        ///<summary>Sets the type of sprite that this image will be used with</summary>
        ///<param name="type">The type of object this sprite is used for</param>
        public void SetSpriteType(string type)
        {
            this.spriteType = type;
        }// SetSpriteType

        ///<summary>Sets how wide each frame of animation is</summary>
        ///<param name="width">The width of the animation frame</param>
        public void SetFrameWidth(int width)
        {
            this.frameWidth = width;
        }// SetFrameWidth

        ///<summary>Sets the height of each frame of animation</summary>
        ///<param name="height">The height of the animation frame</param>
        public void SetFrameHeight(int height)
        {
            this.frameHeight = height;
        }// SetFrameHeight

        ///<summary>Sets the width and height of each frame of animation</summary>
        ///<param name="width">The width of the animation frame</param>
        ///<param name="height">The height of the animation frame</param>
        public void setFrameRectangle(int width, int height)
        {
            this.SetFrameWidth(width);
            this.SetFrameHeight(height);
        }// SetFrameRectangle

        ///<summary>Gets the image the sprite uses</summary>
        ///<returns>Returns the image</returns>
        public Texture2D GetSprite()
        {
            return this.sprite;
        }// GetSprite

        ///<summary>Gets the type of object this sprite should be used for</summary>
        ///<returns>Returns the type that the sprite was given</returns>
        public string GetSpriteType()
        {
            return this.spriteType;
        }// GetSprtieType

        ///<summary>Gets the width of the animation frame</summary>
        ///<returns>Returns the width of a signle frame of animation</returns>
        public int GetFrameWidth()
        {
            return this.frameWidth;
        }// GetFrameWidth

        ///<summary>Gets the height of the animation frame</summary>
        ///<returns>Returns the height of a single frame of animation</returns>
        public int GetFrameHeight()
        {
            return this.frameHeight;
        }// GetFrameHeight
    }// Sprite class
}
