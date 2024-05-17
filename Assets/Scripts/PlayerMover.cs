using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization; 


//PlayerMover is a component that allows us to move the player.
//Not a Runtime 'thing', more so we can spot errors in the editor as they happen easier, 
//also to ensure the GameObject we want to have this component DEFINITELY DOES have it.
[RequireComponent(typeof(MazeMover))]
public class PlayerMover : MonoBehaviour
{
//Could subclass and inherit Update from MazeMover but having PlayerMover as just another component
//makes just as much sense and might actually be better.
//PlayerMover really just wants to change desiredDirection so that's the purpose of having
//Setters and Getters in the MazeMover.

    //Class variables to declare so we can use them in the methods with less of a performance hit.
    private MazeMover mazeMover;
    private Vector2 newDir;

    // Start changed to Awake
    void Awake()
    {
        mazeMover = GetComponent<MazeMover>();
    }

    // Update is called once per frame
    void Update()
    {
        //Even if we're performing our movement in th FixedUpdate we still need to
        //capture the current position here to update the target position correctly.
        //Usually we want to do this as its a good force of habit to prevent falling 
        //into mistakes using GetKeyUp or GetKeyDown in FixedUpdate, is my understanding of it.
        /* if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Returns true EVERY FRAME while the key is held down
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Returns true on the first VISUAL frame when the key is first pressed down
        }
        if (Input.GetButton('Left'))
        {
            //could define this 'Left' in the Input Manager in Unity
        }*/
        //In this situation, to follow along with tutorial we'll use GetAxisRaw since we want 
        //it to properly tell us the exact value of the button press (straight to 1 instead of 
        //simulating a joystick's axis movement with GetAxis)

        //Initialise our new direction
        newDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (newDir.SqrMagnitude() < 0.05f)
        {
            //This is effectively 0, so just return and do nothing since no input is happening
            //If you have any thoughts on a more robust solution such as using Mathf.Abs, please let me know.
            return;
        }
        
        // newDir could be REALLY wonky at this point. Could be diagonal, could have
        //a fractional number like (0.67, -0.24) just for an example.
        //So, we want to sanitise this value to drop 1 of the axes if there is 2 of them:
        
        //In case we have both an X and Y.
        if (Mathf.Abs(newDir.x) > Mathf.Abs(newDir.y))
        {
            newDir.y = 0;
        }
        else
        {
            newDir.x = 0;
        }

        //Set our new direction
        //true is there because there was a bug with the ghosts where they move diagonally
        //when they reverse their direction and try to turn direction in the same frame. So, 
        //as a quick fix we make a boolean to allow the player to instant update while stopping
        //the ghost from doing the same. (This also then means that the player is capable of doing
        //the same, but as it isn't automated, and our scope is so small, it isn't a massive problem.)
        mazeMover.SetDesiredDirection(newDir.normalized, true);

        //Or:
        
        /* if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //Returns a value from -1 to 1 based on left/right input
            mazeMover.SetDesiredDirection(newDir.left);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            mazeMover.SetDesiredDirection(Vector2.right);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            mazeMover.SetDesiredDirection(Vector2.down);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            mazeMover.SetDesiredDirection(Vector2.up);
        } */
    }

    
}
