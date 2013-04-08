using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameGraphics
{
    /// <file></file>
    /// <author>Patrick Young</author>
    /// <version></version>
    /// <summary>
    /// 
    /// </summary>
    public class GraphicsModel
    {
        // dictionary key is the unique ID of the given GraphicsObject which is the same GetID()
        private Dictionary<int, GraphicsObject> dictionary;

        ///<summary>Default Constructor that initializes the dictionary</summary>
        public GraphicsModel()
        {
            this.dictionary = new Dictionary<int, GraphicsObject>();
        }// GraphicsModel

        ///<summary>Update takes a GraphicsObject as a parameter. If there is an object with the
        /// same ID we will update the existing object with the new info otherwise we add
        /// the object to our dictionary of object</summary>
        /// <param name="obj">The object that needs to be updated</param>
        public void Update(GraphicsObject obj)
        {
            if (obj != null) // we actually have an object
            {
                GraphicsObject value;
                if (this.dictionary.TryGetValue(obj.GetID(), out value))
                {
                    // add particle
                    if (value.GetX() != obj.GetX() || value.GetY() != obj.GetY())
                    {
                        value.GetParticleSet().AddParticle(new Particle(value.GetX(), value.GetY(), 4, value.GetAngle()+180, 101, value.GetColor()));
                    }
                    // we have it so we just need to update it
                    value.SetX(obj.GetX());
                    value.SetY(obj.GetY());
                    value.SetAngle(obj.GetAngle());
                    value.IsUpdated(true);
                }
                else
                {
                    // we dont have it so we need to add it
                    obj.IsUpdated(true);
                    this.dictionary.Add(obj.GetID(), obj);
                }
            }
            
        }// Update

        public void CleanUp()
        {
            List<GraphicsObject> list = this.GetAsList();

            for (int l = 0; l < list.Count; l++)
            {
                if (!list[l].HasUpdated())
                {
                    this.Remove(list[l].GetID());
                }
            }
        }// CleanUp

        ///<summary>When called will return a reference to the dictionary it uses</summary>
        ///<returns>returns the stored dictionary</returns>
        public Dictionary<int, GraphicsObject> GetAsDictionary()
        {
            return this.dictionary;
        }// GetAsDictionary

        ///<summary>When called wil return the dictionary in a List format of its values
        ///to allow iteration.</summary>
        ///<returns>returns the dictionary as a List object</returns>
        public List<GraphicsObject> GetAsList()
        {
            List<GraphicsObject> list = new List<GraphicsObject>(this.dictionary.Values);
            return list;
        }// GetAsList

        ///<summary>Returns the GraphicsObject with the given ID
        ///if no object with this ID exists then it returns null</summary>
        ///<param name="id">the ID of the GraphicsObject</param>
        ///<returns>Returns the GraphicsObject with the given ID
        ///if no object with this ID exists then it returns null</returns>
        public GraphicsObject GetObjectWithID(int id)
        {
            GraphicsObject obj;

            if (this.dictionary.TryGetValue(id, out obj))
            {
                // we have it
                return obj;
            }
            else
            {
                // we dont have it
                return null;
            }
        }// GetObjectWithID

        ///<summary>Attempts to remove a GraphicsObject with the specified ID from the dictionary</summary>
        ///<param name="id">the unique ID of the Graphics object to be removed.</param>
        ///<returns>If the ID exists then it will remove the object and return true. otherwise if
        ///it can not find it it will return false.</returns>
        public Boolean Remove(int id)
        {
            return this.dictionary.Remove(id);
        }// Remove
    }// GraphicsModel Class
}
