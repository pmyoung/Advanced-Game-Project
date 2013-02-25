using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.Logic
{
    class Planet:GameObject
    {      
       protected  bool fixedPosition = true;

        protected Planet(int spriteID = -1, float x = 0, float y = 0, int radius = 10, int mass = 10, bool fixedPos = true)
        {
            this.spriteID = spriteID;
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.radius = 0;
            this.mass = mass;
            this.fixedPosition = fixedPos;
            this.speedX = 0;
            this.speedY = 0;
        }

        static public void loadPlanets(string fileString, Map map)
        {
            //Loading just the first one in the file.

            int index1 = 0, index2 = 0;
            //Console.WriteLine("Loading Map:");
            if (((index1 = fileString.IndexOf("<Planet")) > -1) && ((index2 = fileString.IndexOf(">")) > -1) && fileString.Contains("</Planet>"))
            {

                string propriety = fileString.Substring(index1, index2 - index1 + 1);
                fileString = fileString.Remove(index1, index2 - index1 + 1);
                fileString = fileString.Remove(fileString.IndexOf("</Planet>"));

                Planet p = loadPlanetPropriety(propriety);
                map.getGravityObjects().Add(p);
                p.loadMoons(fileString, map);
            }
        }

        private static Planet loadPlanetPropriety(string propriety)
        {
            int spriteId = -1, mass = -1, radius = -1;
            float x=-1, y=-1;
            bool fixedPlanet=true;

            int index1 = 0, index2 = 0;

            if ((index1 = propriety.IndexOf("spriteId=")) > 0)
            {
                index1 += 9;
                index2 = propriety.IndexOf(' ', index1);
                string id = propriety.Substring(index1, index2 - index1 - 1);
                spriteId = Convert.ToInt32(id);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = propriety.IndexOf("x=")) > 0)
            {
                index1 += 2;
                index2 = propriety.IndexOf(' ', index1);
                string xPos = propriety.Substring(index1, index2 - index1 - 1);
                x = Convert.ToInt32(xPos);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = propriety.IndexOf("y=")) > 0)
            {
                index1 += 2;
                index2 = propriety.IndexOf(' ', index1);
                string yPos = propriety.Substring(index1, index2 - index1 - 1);
                y = Convert.ToInt32(yPos);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = propriety.IndexOf("mass=")) > 0)
            {
                index1 += 5;
                index2 = propriety.IndexOf(' ', index1);
                string m = propriety.Substring(index1, index2 - index1 - 1);
                mass = Convert.ToInt32(m);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = propriety.IndexOf("radius=")) > 0)
            {
                index1 += 7;
                index2 = propriety.IndexOf(' ', index1);
                string r = propriety.Substring(index1, index2 - index1 - 1);
                radius = Convert.ToInt32(r);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = propriety.IndexOf("fixed='")) > 0)
            {
                index1 += 7;
                index2 = propriety.IndexOf('\'', index1);
                string f = propriety.Substring(index1, index2 - index1 - 1);
                fixedPlanet = Convert.ToBoolean(f);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            Planet p = new Planet(spriteId, x, y, radius, mass, fixedPlanet);

            return p;

        }

        private void loadMoons(String fileString, Map map)
        {
            int spriteId = -1, number = -1;
            int radius = -1, distance = 30, mass = 20;

            int index1 = 0, index2 = 0;

            if ((index1 = fileString.IndexOf("spriteId=")) > 0)
            {
                index1 += 9;
                index2 = fileString.IndexOf(' ', index1);
                string id = fileString.Substring(index1, index2 - index1 - 1);
                spriteId = Convert.ToInt32(id);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = fileString.IndexOf("number=")) > 0)
            {
                index1 += 7;
                index2 = fileString.IndexOf(' ', index1);
                string n = fileString.Substring(index1, index2 - index1 - 1);
                number = Convert.ToInt32(n);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = fileString.IndexOf("mass=")) > 0)
            {
                index1 += 5;
                index2 = fileString.IndexOf(' ', index1);
                string m = fileString.Substring(index1, index2 - index1 - 1);
                mass = Convert.ToInt32(m);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = fileString.IndexOf("radius=")) > 0)
            {
                index1 += 7;
                index2 = fileString.IndexOf(' ', index1);
                string r = fileString.Substring(index1, index2 - index1 - 1);
                radius = Convert.ToInt32(r);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }

            if ((index1 = fileString.IndexOf("distance=")) > 0)
            {
                index1 += 9;
                index2 = fileString.IndexOf('\'', index1);
                string d = fileString.Substring(index1, index2 - index1 - 1);
                distance = Convert.ToInt32(d);
            }
            else
            {
                //SENT WRONG Map FILE FormatException EVENT
            }


            setUpMoons(spriteId, number, radius, distance, mass, map);
        }

        private void setUpMoons(int spriteId, int number, int radius, int d, int mass, Map map)
        {
            double  angle, sin, cos;
            int distance = this.radius + radius + d;

            
            double x, y;

            for (int i = 0; i < number; i++)
            {
                angle = 360/number * i;
                sin = Math.Sin(angle);
                cos = Math.Cos(angle);
                x = cos*distance + this.x;
                y = -1 * sin + this.y;

                map.getGravityObjects().Add(new Moon(angle,spriteId, (float)x, (float)y, radius, mass, false));

            }

           //Getting the system acceleration to the moons
            Moon moon = (Moon) map.getGravityObjects().Last();
            moon.calculateGravity();
            double moonSpeed = Math.Sqrt(Math.Pow(moon.speedX,2) + Math.Pow(moon.speedY,2));
            moonSpeed = Math.Sqrt(mass*moonSpeed*distance);
            int sizeList = map.getGravityObjects().Capacity;
            for (int i = sizeList - number; i < sizeList; i++)
            {
                Moon m = (Moon) map.getGravityObjects().ElementAt(i);
                m.speedX = (float) (Math.Cos(m.angle) * moonSpeed);
                m.speedX = (float) (Math.Sin(m.angle) * moonSpeed);                
            }
           
        }

        public override void updatePosition()
        {
            if (!fixedPosition)
            {
                base.updatePosition();
            }
        }
        public override  void calculateGravity()
        {
            if (!fixedPosition)
            {
                base.calculateGravity();
            }
        }

        public bool isFixed()
        {
            return fixedPosition;
        }
    }
}
