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
            int index1=0, index2=0;
            
            if (((index1 = fileString.IndexOf("<Map")) > 0) && ((index2 = fileString.IndexOf(">")) > -1))
            {
               string propriety = fileString.Remove(index1, index2 - index1 + 1);
                loadMap(propriety);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }
            //size of ">" = 1
            index1=index2+1;
            //Changing here
            if((index2 = fileString.IndexOf("<\Map>") < 0){

            }
        }
    }
}
