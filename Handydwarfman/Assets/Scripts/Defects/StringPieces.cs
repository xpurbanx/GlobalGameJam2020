using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StringPieces : MonoBehaviour
{
    StringParent stringParent;

    private void Start()
    {
        stringParent = GetComponentInParent<StringParent>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (stringParent.scissors != null && !stringParent.isFixed && other.gameObject.GetComponent<Scissors>() != null)
        {
            if (stringParent.triggerPressed)
                stringParent.Fix(transform.GetSiblingIndex());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (stringParent.scissors != null)
        {
            stringParent.scissors = null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Scissors>() != null || other.gameObject.GetComponentInParent<Scissors>() != null)
            stringParent.scissors = other.gameObject.GetComponent<Scissors>()
                                    ?? other.gameObject.GetComponentInParent<Scissors>();
    }
}
