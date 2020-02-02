using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandObject : MonoBehaviour
{
    public GameObject left, right;

    public GameObject GetLeftObject()
    {
        return left.GetComponent<VRTK_InteractGrab>().GetGrabbedObject();
    }

    public GameObject GetRightObject()
    {
        return right.GetComponent<VRTK_InteractGrab>().GetGrabbedObject();
    }

    public GameObject GetLeftHand()
    {
        return left;
    }

    public GameObject GetRightHand()
    {
        return right;
    }
}
