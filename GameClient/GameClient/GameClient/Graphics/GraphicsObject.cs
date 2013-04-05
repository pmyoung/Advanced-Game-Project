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
    public class GraphicsObject : AbstractGraphicEntity
    {

        private int id;
        private int score;
        private int health;
        private ParticleSet particles;

        ///<summary>This constructor creates a GraphicsObject with all its parameters defined with values</summary>
        ///<param name="id">the ID of the object this should be unique</param>
        ///<param name="x">the x location of the object</param>
        ///<param name="y">the y location of the object</param>
        ///<param name="radius">the radius of the object</param>
        ///<param name="angle">the angle of the object</param>
        ///<param name="spriteID">the ID of the sprite the object uses</param>
        ///<param name="colorID">the ID of the color it wil have (used for the player sprites)</param>
        public GraphicsObject(int id, float x, float y, float radius, float angle, int spriteID, int colorID)
            : base(x, y, radius, angle, spriteID, colorID)
        {
            this.SetID(id);
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
            : base(x, y, radius, angle, spriteID)
        {
            this.SetID(id);
        }// GraphicsObject

        /// <summary>Constructor that creates an object with all parameters defined.
        /// This version allows the color to be defined through a color object rather than using
        /// a default list of colors based on ID.</summary>
        ///<param name="id">the ID of the object this should be unique</param>
        ///<param name="x">the x location of the object</param>
        ///<param name="y">the y location of the object</param>
        ///<param name="radius">the radius of the object</param>
        ///<param name="angle">the angle of the object</param>
        ///<param name="spriteID">the ID of the sprite the object uses</param>
        /// <param name="color">The color object that will be used to change the sprites color</param>
        public GraphicsObject(int id, float x, float y, float radius, float angle, int spriteID, Color color)
            : base(x, y, radius, angle, spriteID, color)
        {
            this.SetID(id);
        }// GraphicsObject

        ///<summary>Sets the ID of the GraphicsObject</summary>
        ///<param name="id">the id to be given to the object</param>
        public void SetID(int id)
        {
            this.id = id;
        }// SetID

        ///<summary>Returns the objects unique ID.</summary>
        ///<returns>Returns the objects unique ID</returns>
        public int GetID()
        {
            return this.id;
        }// GetID

        public void SetScore(int score)
        {
            this.score = score;
        }

        public int GetScore()
        {
            return this.score;
        }

        public void SetHealth(int health){
            this.health = health;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SetParticleSet(ParticleSet particles)
        {
            this.particles = particles;
        }

        public ParticleSet GetParticleSet()
        {
            return this.particles;
        }

    }// GrahicsObject
}
