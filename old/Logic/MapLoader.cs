using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip.Logic
{
    class MapLoader
    {

        static MapLoader loader;
        string initialPath;
        string mapDirectory;
        string[] mapFiles;
        public Map[] maps;

        private MapLoader()
        {
            initialPath = System.IO.Directory.GetCurrentDirectory();
            mapDirectory = initialPath + "\\Maps";
            mapFiles = Directory.GetFiles(@mapDirectory);
            maps = new Map[mapFiles.Length];
            int i = 0;
            foreach (string m in mapFiles)
            {
                maps[i] = new Map(m);
                i++;
            }
        }

        static public MapLoader getInstance()
        {
            if (loader == null) 
                loader = new MapLoader();
            return loader;
        }
    }
}
