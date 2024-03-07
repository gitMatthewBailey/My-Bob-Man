using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MazeMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()               
    {
        //Test
        desiredDirection = new Vector2(1, 0);

        //Set our initial target position to be our starting position so
        //that the Update() will update the target position correctly.
        targetPos = transform.position;

        //This only works so long as there's only ONE TileMap
        wallTileMap = GameObject.FindObjectOfType<Tilemap>();
        //Quite heavy in terms of data so definitely don't want to call
        //on each Update() but its fine to do once in Start() for now.

        // TODO: we broke this, fix it.
    }
    
    float Speed = 3;

    private Vector2 desiredDirection; //current direction we want to move in

    private Vector2 targetPos;

    private Tilemap wallTileMap;

    // Update is called once per frame
    void Update()
    {
        //we have some kind of direction/velocity being applied (a force)
        //so move in that direction...
        //.. IF WE CAN. What if theres a wall in the way? Then we stop.
        
        //First check we can legally move in the direction we want!
        UpdateTargetPosition();

        //Do move.
        MoveToTargetPosition();
        
    }

    //INFO
    //Our objects are not physics-enabled rigidbodies, so
    //the physics system isn't  moving us, nor are we doing 
    //'real' collisions, so Update() is more appropriate in this case.

    //INFO
    //Time.deltaTime is the time its been since the last call to what
    //we're currently (if we're in Update() its the time since this was
    //last called, if in FixedUpdate() its the time since that.

    void UpdateTargetPosition( bool force = false )
    {
        if (force == false)
        {
            //Have we reached our target?
            float distanceToTarget = Vector3.Distance(transform.position, targetPos);
            //Only checking for 0 here since we can check for this in MoveToTargetPosition()
            //and if we're a little bit out we can just set ourselves correctly there.
            if(distanceToTarget > 0)
            {
                //Not there yet, no need to update.
                return;
            }
        }

        //We have reached our target, we need a new target position.
        targetPos += desiredDirection;

        //Normalise the target position to a tile's position
        targetPos = FloorPosition(targetPos);

        if (isTileEmpty(targetPos))
        {
            return;
        }

        //if we get here it means our target position is occupied, so don't allow.
        targetPos = transform.position;
    }

    Vector2 FloorPosition(Vector2 pos)
    {
        //Normalising to a tile's position.
        //This might not line up right if we have a weirdly offset tilemap
        //A 'more robust' way to do this might be 
        //to use the Tilemap's CellToWorld(), where you'd lookup the tile at the new
        //target position, reading back that Tile's world position.
        return new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    bool isTileEmpty(Vector2 pos)
    {
        return GetTileAt(pos) == null;
    }

    TileBase GetTileAt(Vector2 pos)
    {
        //First we need to change the world position to a tile cell index.
        Vector3Int cellPos = wallTileMap.WorldToCell(pos);

        //Now return the tile at that cell.
        return wallTileMap.GetTile(cellPos);
    }

    void MoveToTargetPosition()
    {
        //How far can we move this frame?
        float distanceThisUpdate = Speed * Time.deltaTime;

        //And in what direction is this movement?
        //Towards our target position!
        //Also, we're giving the vector a length of 1 with 'normalized' *spit* silly americans *in overtly british voice*
        //to make it easier to work with when manipulating with arithmetic.
        Vector2 distToTarget = (targetPos - (Vector2)transform.position);
        //And how far are we moving in this update? 
        Vector2 movementThisUpdate = distToTarget.normalized * distanceThisUpdate;

        //What if we're moving PAST the target?
        //We COULD change movementThisUpdate to have the same magnitude as distance to target
        //We also don't care about the actual length of the vectors as we're comparing two, so
        //we can save some time in terms of maths here by comparing the squares of the magnitudes.
        if(distToTarget.SqrMagnitude() < movementThisUpdate.SqrMagnitude())
        {
            //We're past our target, so just move to it.
            transform.position = targetPos;
            return;
        }

        //Do Move!
        transform.Translate(movementThisUpdate);
    }

    public void SetDesiredDirection(Vector2 newDir)
    {
        //Just set our desired direction.
        //Make sure not diagonal? In THEORY, our PlayerMover/EnemyMover script already does this.
        
        // But we shouldn't accept a direction that would slam us into a wall.
        
        Vector2 testPos = FloorPosition(targetPos + newDir);
        if(isTileEmpty(testPos) == false)
        {
            //Trying to slam into wall, ignore.
            return;
        }

        Vector2 oldDir = desiredDirection;
        desiredDirection = newDir;
        //If the input is to reverse our direction, do it instantly?
        //if ((oldDir.x * newDir.x) < 0 || (oldDir.y * newDir.y) < 0)
        if (Vector2.Dot(oldDir, newDir) < 0) // above and this are mathematically identical hence going with slicker ver.
        {
            UpdateTargetPosition(true); //this is all we want to do, but
            //only when we're trying to reverse our direction.
        }
    }

    public Vector2 GetDesiredDirection()
    {
        //Get input!
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //desiredDirection = new Vector2(horizontal, vertical);
        return desiredDirection;
    }
    
}
