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
        public string name;
        
        List<GameObject> gravityObjects;
        List<GameObject> nonGravityObjects;

        public Map(String mapPath)
        {
            //IMPLEMENT INITIALIZATION LISTS
            this.gravityObjects = new List<GameObject>();
            this.nonGravityObjects = new List<GameObject>();
            //Console.WriteLine("Map Constructor Called");
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

            //Console.WriteLine("Loading fileString:");
            //Console.WriteLine(fileString);
            loadMap(fileString);

        }

        private void loadMap(string fileString)
        {
            int index1 = 0, index2 = 0;
            //Console.WriteLine("Loading Map:");
            if (((index1 = fileString.IndexOf("<Map")) > -1) && ((index2 = fileString.IndexOf(">")) > -1) && fileString.Contains("</Map>"))
            {
               
                string propriety = fileString.Substring(index1, index2 - index1 + 1);
                fileString = fileString.Remove(index1, index2 - index1 + 1);
                fileString = fileString.Remove(fileString.IndexOf("</Map>"));

                loadMapPropriety(propriety);
                Planet.loadPlanets(fileString, this);

            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }
        }

        private void loadMapPropriety(string propriety)
        {
            int index1 = 0, index2 = 0;
            if ((index1 = propriety.IndexOf("name='")) > 0)
            {
                index1 += 6;
                index2 = propriety.IndexOf(' ', index1);
                this.name = propriety.Substring(index1, index2-index1-1);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }
            
            if (((index1 = propriety.IndexOf("size='")) > 0) && propriety.Contains('x'))
            {
                //Console.WriteLine("getting size");
                index1 += 6;
                index2 = propriety.IndexOf('\'', index1);
                string size = propriety.Substring(index1, index2 - index1);
                string width = size.Substring(0, size.IndexOf('x'));
                string height = size.Remove(0, size.IndexOf('x') + 1);
                this.mapHeigth = Convert.ToInt32(height);
                this.mapWidth = Convert.ToInt32(width);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }
        }

        //private void loadMapBody(string body)
        //{
        //    // IMPLEMENTE IF THERE IS GONNA BE MORE THEN ONE PLANNET ON THE MAP.
        //}
    }
}
