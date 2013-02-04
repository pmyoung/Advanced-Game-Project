using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    class MatchConfig
    {
        List<Player> players = new List<Player>();
        int mapId;
        int maxPlayers;
        double gravityConstant=5;

        int maxHealth = 30;
        int accelerationRate = 2;
        int rotationRate = 2;
        double brakeRate = 0.9;
        int mass = 10;        

        public MatchConfig(int maxPlayers)
        {
            this.maxPlayers = maxPlayers;
        }

        public MatchConfig(int maxPlayers, int maxHealth, int mapId)
        {
            this.maxPlayers = maxPlayers;
            this.maxHealth = maxHealth;
            this.mapId = mapId;
        }

        public MatchConfig(int maxPlayers, int maxHealth, int mapId, double gravityC)
        {
            this.maxPlayers = maxPlayers;
            this.maxHealth = maxHealth;
            this.mapId = mapId;
            this.gravityConstant = gravityC;
        }

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public void setMaxHealth(int HP)
        {
            this.maxHealth = HP;
        }

        public void addPlayer(Player p)
        {
            this.players.Add(p);
        }

        public void setGravityConstant(double G)
        {
            this.gravityConstant = G;
        }

        public int getMapId()
        {
            return this.mapId;
        }

        public int getHP()
        {
            return this.maxHealth;
        }

        public List<Player> getPlayers()
        {
            return this.players;
        }

        public double getGravityConstant()
        {
            return this.gravityConstant;
        }

        public void setAccelerationRate(int AccelerationRate)
        {
            this.accelerationRate = AccelerationRate;
        }

        public void setRotationRate(int RotationRate)
        {
            this.rotationRate = RotationRate;
        }

        public void setBrakeRate(int BrakeRate)
        {
            this.brakeRate = BrakeRate;
        }

        public void setSpaceShipMass(int Mass)
        {
            this.mass = Mass;
        }

        public int getAccelerationRate()
        {
            return this.accelerationRate;
        }

        public int getRotationRate()
        {
            return this.rotationRate;
        }

        public double getBrakeRate()
        {
            return this.brakeRate;
        }
      
        public int getSpaceShipMass()
        {
            return this.mass;
        }

    }
}
