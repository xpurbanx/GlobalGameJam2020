using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{

    public enum SeenState {Cautious, Noticed, Seen };

    public Collider fovCautious;
    public Collider fovNoticed;
    public Collider fovSeen;


    private void OnTriggerStay(Collider other)
    {
        if(other == Player.instance.playerBody)
        {

        }
    }

}
