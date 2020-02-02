using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CableEntry : MonoBehaviour
{
    public GameObject positionForCable;
    public GameObject left, right;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CableEnding")
        {
            other.transform.position = positionForCable.transform.position;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.GetComponent<CableEnding>().holdingHand.GetComponent<VRTK_InteractGrab>().ForceRelease();
        }
    }
}
