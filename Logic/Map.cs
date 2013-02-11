using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpaceShip.Logic
{
    class Map
    {
        public int mapWidth;
        public int mapHeigth;
        
        List<GameObject> gravityObjects;
        List<GameObject> nonGravityObjects;

        public Map(String mapPath)
        {
            //IMPLEMENT INITIALIZATION LISTS
            this.gravityObjects = new List<GameObject>();
            this.nonGravityObjects = new List<GameObject>();
            load(mapPath);
        }

        public List<GameObject> getGravityObjects()
        {
            return gravityObjects;
        }

        public List<GameObject> getNonGravityObjects()
        {
            return nonGravityObjects;
        }

        public void addNonGravityObject(GameObject o)
        {
            this.nonGravityObjects.Add(o);
        }

        private void load(String mapPath)
        {
            string line;
            string fileString = "";

            // Read the file line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(mapPath);
            line = file.ReadLine();
            while((line = file.ReadLine()) != null) fileString += line;
            file.Close();

            int index1, index2;
            while((index1 = fileString.IndexOf("<!--"))>0)
            {
                index2 = fileString.IndexOf("-->");
                if(index2<0) fileString = fileString.Remove(index1);
                else fileString = fileString.Remove(index1,index2-index1+3);
            }

            loadMap(fileString);
        }

      private void loadMap(string fileString)
        {
            int index1 = 0, index2 = 0;
            //Console.WriteLine("Loading Map:");
            if (((index1 = fileString.IndexOf("<Map")) > -1) && ((index2 = fileString.IndexOf(">")) > -1) && fileString.Contains("</Map"))
            {
               
                string propriety = fileString.Substring(index1, index2 - index1 + 1);
                fileString = fileString.Remove(index1, index2 - index1 + 1);
                fileString = fileString.Remove(fileString.IndexOf("</Map>"));

                loadMapPropriety(propriety);
                loadMapBody(fileString);

            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }
        }

        private void loadMapPropriety(string propriety)
        {
            //int index1 = 0, index2 = 0;
            //if ((index1 = propriety.IndexOf("name=")) > 0)
            //{
            //    index1 += 5 + 1;
            //    propriety = propriety.Substring(index1);
            //    Console.WriteLine("P: " + propriety);
            //    index2 = propriety.IndexOf(" ");
               


            //    //string name = propriety.Substring(index1
            //}
            //else
            //{
            //    //SENT WRONG Map FILE FormatException EVENT
            //}
        }

        private void loadMapBody(string body)
        {

        }
    }
}
