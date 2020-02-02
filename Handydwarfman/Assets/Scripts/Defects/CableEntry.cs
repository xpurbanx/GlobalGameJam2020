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
            other.transform.rotation = positionForCable.transform.rotation;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.GetComponentInParent<Cable>().holdingHand.GetComponent<VRTK_InteractGrab>().ForceRelease();
        }
    }
}
