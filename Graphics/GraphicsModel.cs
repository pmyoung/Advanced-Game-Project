using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameGraphics
{
    /**
     * @file
     * @author Patrick Young
     * @version
     * 
     * @section DESCRIPTION
     */
    public class GraphicsModel
    {
        // dictionary key is the unique ID of the given GraphicsObject which is the same GetID()
        private Dictionary<int, GraphicsObject> dictionary;

        /**
         * Default Constructor that initializes the dictionary
         */
        public GraphicsModel()
        {
            this.dictionary = new Dictionary<int, GraphicsObject>();
        }// GraphicsModel

        /**
         * Update takes a GraphicsObject as a parameter. If there is an object with the
         * same ID we will update the existing object with the new info otherwise we add
         * the object to our dictionary of object
         * 
         * @param obj The object that needs to be updated
         */
        public void Update(GraphicsObject obj)
        {
            GraphicsObject value;
            if (this.dictionary.TryGetValue(obj.GetID(), out value))
            {
                // we have it so we just need to update it
                value.SetX(obj.GetX());
                value.SetY(obj.GetY());
                value.SetAngle(obj.GetAngle());
            }
            else
            {
                // we dont have it so we need to add it
                this.dictionary.Add(obj.GetID(), obj);
            }
        }// Update


        /**
         * When called will return a reference to the dictionary it uses
         */
        public Dictionary<int, GraphicsObject> GetAsDictionary()
        {
            return this.dictionary;
        }// GetAsDictionary

        /**
         * When called wil return the dictionary in a List format of its values
         * to allow iteration.
         */
        public List<GraphicsObject> GetAsList()
        {
            List<GraphicsObject> list = new List<GraphicsObject>(this.dictionary.Values);
            return list;
        }// GetAsList

        /**
         * Returns the GraphicsObject with the given ID
         * if no object with this ID exists then it returns null
         * 
         * @param id the ID of the GraphicsObject
         */
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
    }// GraphicsModel Class
}
