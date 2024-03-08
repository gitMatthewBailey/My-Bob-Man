using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{
    //The consideration here is that we can create instances of this scriptable object that 
    //only held the graphics information (the sprite) which a manager class could use to check
    //whether a player has 1. collided with a wall or 2. collided with a pellet. 
    //In the future this could be useful to trigger some kind of 'event' when a player collides
    //with a pellet specifically (not a wall since the only action taken is preventing the player
    //from entering that tile). This could then be expanded on to implement extra mechanics
    //for the player interacting with 'things' that give the player extra points or extra lives for
    //example.
    //I believe that would be the 'observer pattern'. 
    
    //I'm not sure if this is the best way to do it, but the way I'm going to go for now, with the
    //PelletTileMap and TilePellet is similar to this, without the ScriptableObjects. In fact, the 
    //pellets aren't even game objects, they're just sprites on a tilemap. So, the Tilemaps will
    //be the 'observer' and the sprites attached to them will be the things that are being observed. 

    //I think maybe subclassing from TileData would be the way to go if I wanted to add my own mechanics in the 
    //future (like powerups that arent in Pac-Man for example).


    //I want to use this class only to hold the information of which sprite is displayed
    //within it.
    public Sprite sprite;
}
