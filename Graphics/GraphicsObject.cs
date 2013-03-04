using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class GraphicsObject
    {
        // this color will be used by default if the color is set to it or an invalid ID is given
        private static int DEFAULT_COLOR = 0;

        /*
         * Player 1 = light coral "red"
         * Player 2 = light blue "blue"
         * Player 3 = lime "green"
         * Player 4 = yellow "yellow"
         * Player 5 = cyan "cyan"
         * Player 6 = orange "orange"
         * Player 7 = medium purple "purple"
         * Player 8 = pink "pink"
         * Everything else will use white (a non altered sprite color)
         */
        private static Color[] COLOR = { Color.White, Color.LightCoral, Color.LightBlue, Color.Lime, Color.Yellow, Color.Cyan, Color.Orange, Color.MediumPurple, Color.Pink };

        private int id;
        private int spriteID;
        private int colorID;
        
        private float x;
        private float y;
        private float radius;
        private float angle;

        /**
         * This constructor creates a GraphicsObject with all its parameters defined with values
         * 
         * @param id the ID of the object this should be unique
         * @param x the x location of the object
         * @param y the y location of the object
         * @param radius the radius of the object
         * @param angle the angle of the object
         * @param spriteID the ID of the sprite the object uses
         * @param colorID the ID of the color it wil have (used for the player sprites)
         */
        public GraphicsObject(int id, float x, float y, float radius, float angle, int spriteID, int colorID)
        {
            this.SetID(id);
            this.SetX(x);
            this.SetY(y);
            this.SetRadius(radius);
            this.SetAngle(angle);
            this.SetSpriteID(spriteID);
            this.SetColorID(colorID);
        }// GraphicsObject

        /**
         * Constructor that accepts all but the colorID as parameters. This is usefull
         * for object that do not need their color pallet changed (all objects other
         * than the players)
         * 
         * @param id the ID of the object this should be unique
         * @param x the x location of the object
         * @param y the y location of the object
         * @param radius the radius of the object
         * @param angle the angle of the object
         * @param spriteID the ID of the sprite the object uses
         */
        public GraphicsObject(int id, float x, float y, float radius, float angle, int spriteID)
        {
            this.SetID(id);
            this.SetX(x);
            this.SetY(y);
            this.SetRadius(radius);
            this.SetAngle(angle);
            this.SetSpriteID(spriteID);
            this.SetColorID(DEFAULT_COLOR);
        }// GraphicsObject

        /**
         * Sets the ID of the GraphicsObject
         * 
         * @param id the id to be given to the object
         */
        public void SetID(int id)
        {
            this.id = id;
        }// SetID

        /**
         * Sets the X location of the GraphicsObject
         * 
         * @param x the X location to be given to the object
         */
        public void SetX(float x){
            this.x = x;
        }// SetX

        /**
         * Sets the Y location of the GraphicsObject
         * 
         * @param y the Y location to be given to the object
         */
        public void SetY(float y)
        {
            this.y = y;
        }// SetY

        /**
         * Sets the radius of the GraphicsObject
         * 
         * @param r the radius to be given to the object
         */
        public void SetRadius(float r)
        {
            this.radius = r;
        }// SetRadius

        /**
         * Sets the angle of the GraphicsObject
         * 
         * @param a the angle to be given to the object
         */
        public void SetAngle(float a)
        {
            this.angle = a%360;
        }// SetAngle

        /**
         * Sets the spriteID which will determine what sprite will be used
         * for this object
         * 
         * @param spriteID the ID of the sprite to be used
         */
        public void SetSpriteID(int spriteID)
        {
            this.spriteID = spriteID;
        }// SetSpriteID

        /**
         * Sets the colorID of the GraphicsObject which determines the color.
         * If the ID is not in the range of the colorID's then it will use the
         * default (White)
         * 
         * @param colorID the ID value of the color to be used
         */
        public void SetColorID(int colorID)
        {
            if (colorID > 0 && colorID < COLOR.Length)
            {
                this.colorID = colorID;
            }
            else // if we get a value that is not in our range make it white
            {
                this.colorID = DEFAULT_COLOR;
            }
        }// SetColorID

        /**
         * Returns the objects unique ID.
         */
        public int GetID()
        {
            return this.id;
        }// GetID

        /**
         * Returns the given X location
         */
        public float GetX()
        {
            return this.x;
        }// GetX

        /**
         * Returns the given Y location
         */
        public float GetY()
        {
            return this.y;
        }// GetY

        /**
         * Returns the given radius of the object
         */
        public float GetRadius()
        {
            return this.radius;
        }// GetRadius

        /**
         * Returns the given angle of the object
         */
        public float GetAngle()
        {
            return this.angle;
        }// GetAngle

        /**
         * Returns the given sprite ID of the object
         */
        public int GetSpriteID()
        {
            return this.spriteID;
        }// GetSpriteID

        /**
         * Returns the given color ID of the object
         */
        public int GetColorID()
        {
            return this.colorID;
        }// GetColorID

        /**
         * Returns the actual color that this sprite will be using
         */
        public Color GetColor()
        {
            return COLOR[this.GetColorID()];
        }// GetColor

    }// GrahicsObject
}
