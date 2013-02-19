/*
 * @author Patrick Young
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameGraphics
{
    class Sprite
    {
        private Texture2D sprite;
        private string spriteType;
        private int frameWidth;
        private int frameHeight;

        /*
         * The Sprite Constructor that requires a Texture2D object (image file), and
         * the type of object (like ship, planet).
         * 
         * @param
         * sprite: The image that the sprite will use
         * type: The type of sprite (like Ship, Planet)
         */
        public Sprite(Texture2D sprite, int frameWidth, int frameHeight, string type)
        {
            this.SetSprite(sprite);
            this.SetSpriteType(type);
            this.setFrameRectangle(frameWidth, frameHeight);
        }// Sprite

        /*
         * Render method that all overloads call.
         * 
         * @param
         * spriteBatch: Contains graphics contexts for rendering on the game screen
         * bounds: The region of the screen the sprite will be drawn at
         * color: Changes the color of the sprite
         * rotation: The amount to rotate the sprite in radians
         * origin: The point on the sprite that is used as the center of rotation
         * effect: Chosen sprite mirroring option (like None, FlipHorizontally)
         * layer: Represents the depth 0 front of layer 1 back of layer
         * action: Integer representing what "action" of the sprite to display
         * frame: Integer representing what "frame" of the action animation to display
         */
        public void Render(SpriteBatch spriteBatch, Rectangle bounds, Color color, float rotation, Vector2 origin, SpriteEffects effect, float layer, int action, int frame)
        {
            // the rectangle for where on the sprite sheet will be displayed
            Rectangle source = new Rectangle(frame*this.GetFrameWidth(), action*this.GetFrameHeight(), this.GetFrameWidth(), this.GetFrameHeight());

            spriteBatch.Draw(this.GetSprite(), bounds, source, color, rotation, origin, effect, layer);
        }// Render

        /*
         * Render this overload allows basic rendering of a sprite that has circular properties.
         * 
         * @param
         * spriteBatch: Contains graphics contexts for rendering on the game screen
         * x: The left-right/west-east location of the sprite
         * y: The up-down/north-south location of the sprite
         * radius: The radius of the "circle"
         * angle: The angle of the sprite in degrees
         * layer: Represents the depth 0 front of layer 1 back of layer
         * action: Integer representing what "action" of the sprite to display
         * frame: Integer representing what "frame" of the action animation to display
         */
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

        /*
         * Renders the given GraphicsObject
         * 
         * @param
         * spriteBatch: Contains graphics contexts for rendering on the game screen
         * obj: The GraphicsObject that is to be rendered
         */
        public void Render(SpriteBatch spriteBatch, GraphicsObject obj)
        {
            this.Render(spriteBatch, (int)obj.GetX(), (int)obj.GetY(), (int)obj.GetRadius(), obj.GetColor(), (int)obj.GetAngle(), 0.0f, 0, 0);
        }// Render

        /*
         * Sets the image that this sprite will use
         * 
         * @param
         * sprite: The image that will be used for the sprite
         */
        public void SetSprite(Texture2D sprite)
        {
            this.sprite = sprite;
        }// SetSprite

        /*
         * Sets the type of sprite that this image will be used with
         * 
         * @param
         * type: The type of object this sprite is used for
         */
        public void SetSpriteType(string type)
        {
            this.spriteType = type;
        }// SetSpriteType

        /*
         * Sets how wide each frame of animation is
         * 
         * @param
         * width: The width of the animation frame
         */
        public void SetFrameWidth(int width)
        {
            this.frameWidth = width;
        }// SetFrameWidth

        /*
         * Sets the height of each frame of animation
         * 
         * @param
         * height: The height of the animation frame
         */
        public void SetFrameHeight(int height)
        {
            this.frameHeight = height;
        }// SetFrameHeight

        /*
         * Sets the width and height of each frame of animation
         * 
         * @param
         * width: The width of the animation frame
         * height: The height of the animation frame
         */
        public void setFrameRectangle(int width, int height)
        {
            this.SetFrameWidth(width);
            this.SetFrameHeight(height);
        }// SetFrameRectangle

        /*
         * Gets the image the sprite uses
         */
        public Texture2D GetSprite()
        {
            return this.sprite;
        }// GetSprite

        /*
         * Gets the type of object this sprite should be used for
         */
        public string GetSpriteType()
        {
            return this.spriteType;
        }// GetSprtieType

        /*
         * Gets the width of the animation frame
         */
        public int GetFrameWidth()
        {
            return this.frameWidth;
        }// GetFrameWidth

        /*
         * Gets the height of the animation frame
         */
        public int GetFrameHeight()
        {
            return this.frameHeight;
        }// GetFrameHeight
    }// Sprite class
}
