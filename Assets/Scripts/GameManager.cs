using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Might do a lot of things like keep track of our score for example, but for now as of creation
    // gives the other objects the ability to ask the game manager for a quick and easy lookup of what they
    // want (at this point, it is the wall tilemap)
    void Start()
    {
        //FOR NOW -- this will change to support level loading.
        WallTilemap = GameObject.FindObjectOfType<WallTileMap>().GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Might want to make this protected with a public setter and getter instead so that the other objects
    //can't change the link to the tilemap.
    //Now, this consideration of using the GameManager might actually be better, because regardless of the
    //implementation here (whether we use getters and setters and all that), in the rest of the code (i.e. in
    //MazeMover) the reference to the tilemap is always the same (it will always look like GameManager.WallTileMap).
    static public Tilemap WallTilemap;
}
