using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when the player presses E,
    // and there is a resource that is collectible as defined by PlayerCollectibleRadius,
    // change player state to "collecting"
    // in "collecting" state,
        // perhaps you are "elapsing" time from the resource. send out events.
        // show a signifier of this as well: grab the remaining and elapsed time to mine a resource
}
