using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceFX : MonoBehaviour
{
    //Class variables
    //bounces a second
    float timeScale = 20f;
    //how tall it appears to bounce
    float distScale = 0.05f;
    float t = 0;

    //Will move the entire tilemap up and down, but is okay due to the nature of the game.

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * timeScale;
        
        //"X" doesn't change hence the 0, "Y" changes which is where the sin maths comes from,
        //"Z" doesn't change hence the 0.
        this.transform.position = new Vector3(0, Mathf.Sin(t) * distScale, 0);
    }
}
