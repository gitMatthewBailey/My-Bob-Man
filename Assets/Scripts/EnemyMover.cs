using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeMover))]
public class EnemyMover : MonoBehaviour
{
    //Class variables to declare so we can use them in the methods with less of a performance hit.
    MazeMover mazeMover;
    Vector2 newDir;
    Vector2 oldDir;


    //Might want to weight the chance of changing direction, this way if we have more enemies in the future
    //we can change this weight as, in the real Pac-Man the ghosts have different behaviours to each other.
    [SerializeField]
    float forwardWeight = 0.5f; //Chance of continuing forward instead of turning at intersection.

    // Start changed to Awake             
    void Awake() 
    {
        mazeMover = GetComponent<MazeMover>();
        mazeMover.OnEnterNewTile += OnEnterNewTile;
    }

    // Update is called once per frame
    void Update()
    {
        //Essentially all that our 'AI', so to speak, is going to do is change the direction
        //of our ghosts.
        //The EnemyMover gets the tools to do this from the MazeMover. 
        //We just need to tell it how and when and which direction to move in.
        // For this, we'll use an event-driven approach.
        //We want the oppurtunity to tell it to change direction specifically when we enter
        //a new tile - maybe when we hit a wall or something.


        // mazeMover.SetDesiredDirection(newDir.normalized);
        
    }

    void DoTurn()
    {
        //Do a turn to the left or right
        //We can use this to change our direction if we hit a wall.
        //Initialise our new direction, and old direction.
        newDir = Vector2.zero;

        oldDir = mazeMover.GetDesiredDirection();

        if (Mathf.Abs(oldDir.x) > 0)
        {
            //Moving left-right currently
            //So we want to turn to go up-down
            //A smarter enemy might ask: is the player above or below us? and
            //weight the "Y" direction accordingly
            newDir.y = Random.Range(0, 2) == 0 ? -1 : 1;
        }
        else
        {
            //Moving up-down currently
            //So we want to turn to go left-right
            newDir.x = Random.Range(0, 2) == 0 ? -1 : 1;
        }

        /* Don't need to do this anymore //Check about reversing direction
        if (Vector2.Dot(newDir, mazeMover.GetDesiredDirection()) < 0)
        {
            //Trying to reverse our direction.
            newDir *= -1;
        }
        Debug.Log("Turning " + newDir.) */

        mazeMover.SetDesiredDirection(newDir);
    }

    void OnEnterNewTile()
    {
        //Debug.Log(gameObject.name + " OnEnterNewTile");
        //We definitely want something smarter to determine what to do if we're heading into a wall.
        //If we're face planting into a wall, first invert outr direction
        //THEN we decide if we want to continue going "Straight" (backwards)
        //or attempt a turn
        if (mazeMover.WouldHitWall())
        {
            newDir = mazeMover.GetDesiredDirection();
            newDir.x *= -1f;
            newDir.y *= -1f;
            mazeMover.SetDesiredDirection(newDir);
        }
        //50-50 chance of continuing forward.
        if (Random.Range(0f, 1f) < forwardWeight)
        {
            //Keep moving forward
            return;
        }
        
        DoTurn();
    }
}
