using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    class GameScene
    {
        static GameScene scene;

        MatchConfig config;

        List<GameObject> objectsG;
        List<GameObject> objectsNG;

        GameScene(MatchConfig config)
        {
            //IMPLEMENT INITIALIZATION LISTS
            this.objectsG = new List<GameObject>();
            this.objectsNG = new List<GameObject>();
            GameScene.scene = this;
            this.config = config;
        }

        static public GameScene getInstance()
        {
            return GameScene.scene;
        }

        public MatchConfig getConfig()
        {
            return this.config;
        }

        public List<GameObject> getObjectsG()
        {
            return objectsG;
        }

        public List<GameObject> getObjectsNG()
        {
            return objectsNG;
        }
    }
}
