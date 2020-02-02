using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableEnding : MonoBehaviour
{
    public GameObject holdingHand;

    public void OnGrab(GameObject hand)
    {
        holdingHand = hand;
    }

    public void OnUngrab(GameObject hand)
    {
        if (holdingHand == hand)
        {
            holdingHand = null;
        }
    }
}
