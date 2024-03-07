using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletTileMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        pelletEaters = GameObject.FindObjectsOfType<PelletEater>();

        myTileMap = GetComponent<Tilemap>();
        //easy, because the script belongs to the same object as the Tilemap
    }

    // TODO: Add code for pellet eaters to signal updating this whenever they come or go.,
    //if that's something that happens (such as adding or removing PelletEaters (multiplayer?))
    PelletEater[] pelletEaters;

    Tilemap myTileMap;

    // Update is called once per frame
    void Update()
    {
        //Is a pellet eater on a tile with a pellet?
        foreach (PelletEater pe in pelletEaters)
        {
            CheckPellet(pe);
        }
    }

    void CheckPellet(PelletEater pelletEater)
    {
        //TODO: Add code to check what tile pe is in, and if there is a pellet there.
        TileBase tile = GetTileAt((Vector2)pelletEater.transform.position);

        if (tile == null)
        {
            //Empty tile with no pellets.
            return;
        }

        Debug.Log("Scran!");
    }

    //below is ctrl c+v from MazeMover.cs

    TileBase GetTileAt(Vector2 pos)
    {
        //First we need to change the world position to a tile cell index.
        Vector3Int cellPos = myTileMap.WorldToCell(pos);

        //Now return the tile at that cell.
        return myTileMap.GetTile(cellPos);
    }

}

//Before PelletTileMap.cs existed there was Pellet.cs, and the consideration was to just 
//code the behaviour of when a Player touches a Pellet there. But, this would be too
//challenging to handle accurately ourselves, so then the consideration was to use Unity's
//2D collision/trigger component within the Pellet -> Graphics so that complex partitioning
//isn't an issue I have to deal with directly. But, from my understanding, this would still
//require a good bit of computing, so the consideration now is to have all the logic within 
//TileMap itself, instead.

//From old Pellet.cs

    //Could use the collision system Unity gives us to do this
        /* void OnTriggerEnter2D(Collider2D other)
        {
            //and then something like:
            if (other.gameObject.tag == "Pellet")
            {
                Destroy(other.gameObject);
            }
        } */
        //But using this system might be overkill for this scope, so we'll just do a 
        //more code-centric way than above.

        //But if we want the game to have all the pellets and all the pelletEaters always 
        //checking for one another's distance to each other, we'd need some pretty complex 
        //partitioning code to be written, which is definitely outside the scope of this project.
        //pelletEaters = GameObject.FindObjectsOfType<PelletEater>(); 
        //and then having a for loop then goes through a global array of all pelletEaters for example,
        //would be pretty slow presumably.

        //So, we'll go back to Unity's collision system.

        //At the point of this comment, we want the pellet to be part of the tilemap system, 
        //so its easier to paint out for 1, and also can't have more than 1 pellet per tile.
