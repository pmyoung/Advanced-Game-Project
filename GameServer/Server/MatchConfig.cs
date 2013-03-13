/// Matchconfig Class
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class stores settings related to the physics and gameplay in a game match.
/// </summary>
/// <remarks> 
/// This class includes the maximum number of players, constants for gravity and bullet speed, reload rate, maximum health, acceleration and rotation rate of ships and other constants
/// that are set when the game initializes.  In later revisions of the game, these constants could be made to change based upon the map file. Presently they are static values.
///</remarks> 



class MatchConfig

{
    //static public List<Player> players = new List<Player>();
    //static public Map map;

    //maximum number of players in the game
    static public int maxPlayers = 2;

    //strength of gravity
    static public float gravityConstant = (float)(-5);

    //base speed of bullets
    static public int bulletSpeed = 7;

    //reload speed for ships (Not currently in use)
    static public int reloadRate = 1;

    //max health for ships (Not currently in use)
    static public int maxHealth = 30;

    //acceleration rate of ships
    static public double accelerationRate = 0.125;

    //rotation rate of ships
    static public int rotationRate = 3;

    //brake rate of ships (Not currently in use)
    static public double brakeRate = 0.125;

    //width of the map
    static public int mapWidth = 800;

    //height of the map
    static public int mapHeight = 600;

    //static public void connectPlayers()
    //{
    //    map.getNonGravityObjects().AddRange(players);
    //}
}
