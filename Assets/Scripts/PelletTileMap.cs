using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PelletTileMap : MonoBehaviour
{
    //Class variables
    // TODO: Add code for pellet eaters to signal updating this whenever they come or go,
    //if that's something that happens (such as adding or removing PelletEaters (multiplayer?))
    private PelletEater[] pelletEaters;
    private ScoreManager scoreManager;
    private Tilemap myTilemap;

    //'FruitTilemap' and 'PPTilemap' created in Unity. (Identical to 'PelletTilemap' (used to hold
    //reference to this script, and to hold the sprites for fruits and power pellets (PP))).
    
    //What happens when we eat a pellet on this map?
    //For a simple Pac-Man game, this is all we'd really need to know, but could be expanded on and 
    //potentially re-done into a more flexible but more complex design.
    
    //But what we don't have is the ability to get information out of this pellet, 
    //which is where subclassing TileBase in TileData could come in.

    //What we can do (because we currently don't have or need a bunch of custom behaviours) is have these
    //variables shared which we'll use to hold information about things on the map.
    [SerializeField]
    private int pelletPoints = 1;
    [SerializeField]
    private bool requiredForLevelCompletion = false;
    [SerializeField]
    private float powerSeconds = 0;

    //Declaring offsetPos here so we can use it in the Update() method
    Vector2 offsetPos;

    // The actual pellet tilemap has this local code and is checking for pellets.
    void Awake()
    {
        pelletEaters = GameObject.FindObjectsOfType<PelletEater>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        myTilemap = GetComponent<Tilemap>();
        //easy, because the script belongs to the same object as the Tilemap
    }

    // Update is called once per frame
    void Update()
    {
        //Is a pellet eater on a tile with a pellet?
        foreach (PelletEater pe in pelletEaters)
        {
            //Not having to check every pellet, just literally checking in the tile we're about to enter, whether
            //there's a pellet sprite or not.

            if (CheckPellet(pe))
            {
                EatPelletAt(offsetPos);
                scoreManager.AddScore(pelletPoints);
            }
        }
    }

    bool CheckPellet(PelletEater pelletEater)
    {
        //No matter what, at some point something we do will not look right in terms of graphics, whether
        //we use a center point, bottom left corner or whatever. So we need to use maths to determine the offset.
                                    //this lookup should be very lightweight
        offsetPos = (Vector2)pelletEater.transform.position + new Vector2(0.5f, 0.5f);

        //Check what tile pe is in, and if there is a pellet there, eat it, otherwise do nothing.
        TileBase tile = GetTileAt(offsetPos);

        return tile != null; // && tile.name == "Pellet";

        //Debug.Log("Scran!");
        //Tile has a pellet on it, t.f. return true so in Update() we can call EatPelletAt() easily.
    }

    void EatPelletAt(Vector2 pos )
    {
        SetTileAt(pos, null);

        //TODO: increment points based on PelletPoints variable
        


    }

    void SetTileAt(Vector2 pos, TileBase tile)
    {
        //First we need to change the world position to a tile cell index.
        Vector3Int cellPos = myTilemap.WorldToCell(pos);

        //Then, set the tile at that cell.
        myTilemap.SetTile(cellPos, tile);
    }

    //below is ctrl c+v from MazeMover.cs

    TileBase GetTileAt(Vector2 pos)
    {
        //First we need to change the world position to a tile cell index.
        Vector3Int cellPos = myTilemap.WorldToCell(pos);

        //Now return the tile at that cell.
        return myTilemap.GetTile(cellPos);
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
