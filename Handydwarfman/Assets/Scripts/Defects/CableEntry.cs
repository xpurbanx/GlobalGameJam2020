using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CableEntry : MonoBehaviour
{
    public ParticleSystem sparks;
    public GameObject positionForCable;
    public int neededColor;
    public bool isGood;

    private void Start()
    {
        GetComponentInParent<Doll>().cableEntries.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CableEnding")
        {
            if (other.gameObject.GetComponentInParent<Cable>().color == neededColor && other.GetComponentInParent<Cable>().holdingHand != null)
            {
                other.transform.position = positionForCable.transform.position;
                other.transform.rotation = positionForCable.transform.rotation;
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.GetComponentInParent<Cable>().holdingHand.GetComponent<VRTK_InteractGrab>().ForceRelease();
                isGood = true;
                GetComponentInParent<Doll>().CheckIfFixed();
            }

            else
            {
                if (!sparks.isPlaying)
                {
                    sparks.gameObject.SetActive(false);
                    sparks.gameObject.SetActive(true);
                    sparks.Play();
                }
            }
        }
    }
}
