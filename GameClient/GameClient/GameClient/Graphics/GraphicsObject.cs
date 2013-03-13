using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameGraphics
{
    /// <file></file>
    /// <author>Patrick Young</author>
    /// <version></version>
    /// <summary>
    /// 
    /// </summary>
    public class GraphicsObject
    {
        // this color will be used by default if the color is set to it or an invalid ID is given
        private static int DEFAULT_COLOR = 0;

        ///Player 1 = light coral "red"
        ///Player 2 = light blue "blue"
        ///Player 3 = lime "green"
        ///Player 4 = yellow "yellow"
        ///Player 5 = cyan "cyan"
        ///Player 6 = orange "orange"
        ///Player 7 = medium purple "purple"
        ///Player 8 = pink "pink"
        ///Everything else will use white (a non altered sprite color)
        private static Color[] COLOR = { Color.White, Color.LightCoral, Color.LightBlue, Color.Lime, Color.Yellow, Color.Cyan, Color.Orange, Color.MediumPurple, Color.Pink };

        private int id;
        private int spriteID;
        private int colorID;
        
        private float x;
        private float y;
        private float radius;
        private float angle;

        ///<summary>This constructor creates a GraphicsObject with all its parameters defined with values</summary>
        ///<param name="id">the ID of the object this should be unique</param>
        ///<param name="x">the x location of the object</param>
        ///<param name="y">the y location of the object</param>
        ///<param name="radius">the radius of the object</param>
        ///<param name="angle">the angle of the object</param>
        ///<param name="spriteID">the ID of the sprite the object uses</param>
        ///<param name="colorID">the ID of the color it wil have (used for the player sprites)</param>
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

        ///<summary>Constructor that accepts all but the colorID as parameters. This is usefull
        ///for object that do not need their color pallet changed (all objects other
        ///than the players)</summary>
        ///<param name="id">the ID of the object this should be unique</param>
        ///<param name="x">the x location of the object</param>
        ///<param name="y">the y location of the object</param>
        ///<param name="radius">the radius of the object</param>
        ///<param name="angle">the angle of the object</param>
        ///<param name="spriteID">the ID of the sprite the object uses</param>
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

        ///<summary>Sets the ID of the GraphicsObject</summary>
        ///<param name="id">the id to be given to the object</param>
        public void SetID(int id)
        {
            this.id = id;
        }// SetID

        ///<summary>Sets the X location of the GraphicsObject</summary>
        ///<param name="x">the X location to be given to the object</param>
        public void SetX(float x){
            this.x = x;
        }// SetX

        ///<summary>Sets the Y location of the GraphicsObject</summary>
        ///<param name="y">the Y location to be given to the object</param>
        public void SetY(float y)
        {
            this.y = y;
        }// SetY

        ///<summary>Sets the radius of the GraphicsObject</summary>
        ///<param name="r">the radius to be given to the object</param>
        public void SetRadius(float r)
        {
            this.radius = r;
        }// SetRadius

        ///<summary>Sets the angle of the GraphicsObject</summary>
        ///<param name="a">the angle to be given to the object</param>
        public void SetAngle(float a)
        {
            this.angle = a%360;
        }// SetAngle

        ///<summary>Sets the spriteID which will determine what sprite will be used
        ///for this object</summary>
        ///<param name="spriteID">the ID of the sprite to be used</param>
        public void SetSpriteID(int spriteID)
        {
            this.spriteID = spriteID;
        }// SetSpriteID

        ///<summary>Sets the colorID of the GraphicsObject which determines the color.
        ///If the ID is not in the range of the colorID's then it will use the
        ///default (White)</summary>
        ///<param name="colorID">the ID value of the color to be used</param>
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

        ///<summary>Returns the objects unique ID.</summary>
        ///<returns>Returns the objects unique ID</returns>
        public int GetID()
        {
            return this.id;
        }// GetID

        ///<summary>Returns the given X location</summary>
        ///<returns>Returns the given X location</returns>
        public float GetX()
        {
            return this.x;
        }// GetX

        ///<summary>Returns the given Y location</summary>
        ///<returns>Returns the given Y location</returns>
        public float GetY()
        {
            return this.y;
        }// GetY

        ///<summary>Returns the given radius of the object</summary>
        ///<returns>Returns the given radius of the object</returns>
        public float GetRadius()
        {
            return this.radius;
        }// GetRadius

        ///<summary>Returns the given angle of the object</summary>
        ///<returns>Returns the given angle of the object</returns>
        public float GetAngle()
        {
            return this.angle;
        }// GetAngle

        ///<summary>Returns the given sprite ID of the object</summary>
        ///<returns>Returns the given sprite ID of the object</returns>
        public int GetSpriteID()
        {
            return this.spriteID;
        }// GetSpriteID

        ///<summary>Returns the given color ID of the object</summary>
        ///<returns>Returns the given color ID of the object</returns>
        public int GetColorID()
        {
            return this.colorID;
        }// GetColorID

        ///<summary>Returns the actual color that this sprite will be using</summary>
        ///<returns>Returns the colorID as a Color object</returns>
        public Color GetColor()
        {
            return COLOR[this.GetColorID()];
        }// GetColor

    }// GrahicsObject
}
