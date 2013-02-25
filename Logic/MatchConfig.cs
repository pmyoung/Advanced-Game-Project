using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.Logic
{
    class MatchConfig
    {
        static public List<Player> players = new List<Player>();      
        static public Map map;
        static public int maxPlayers = 2;
        static public float gravityConstant = 5;

        static public int bulletSpeed = 5;
        static public int maxHealth = 30;
        static public int accelerationRate = 2;
        static public int rotationRate = 2;
        static public double brakeRate = 0.9;
        static public int mass = 10;
    }
}
