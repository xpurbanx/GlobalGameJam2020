using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CableEntry : MonoBehaviour
{
    public GameManager GameController;
    public GameObject PlayerController;
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
        if (other.gameObject.CompareTag("CableEnding") && !isGood)
        {
            if (other.gameObject.GetComponentInParent<Cable>().color != neededColor)
            {
                Sparks();
                return;
            }

            // Jeśli odpowiedni kabel trzyma lewa ręka
            if (other.gameObject == PlayerController.GetComponent<HandObject>().GetLeftObject())
            {
                InsertCable(other);
                PlayerController.GetComponent<HandObject>().GetLeftHand().GetComponent<VRTK_InteractGrab>().ForceRelease();
            }

            // Jeśli odpowiedni kabel trzyma prawa ręka
            else if (other.gameObject == PlayerController.GetComponent<HandObject>().GetRightObject())
            {
                InsertCable(other);
                PlayerController.GetComponent<HandObject>().GetRightHand().GetComponent<VRTK_InteractGrab>().ForceRelease();
            }

            // Jeśli nie jest to odpowiedni kabel
            else
            {
                Sparks();
            }
        }
    }

    void InsertCable(Collider other)
    {
        other.transform.position = positionForCable.transform.position;
        other.transform.rotation = positionForCable.transform.rotation;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        isGood = true;
        GetComponentInParent<Doll>().CheckIfFixed();
    }

    void Sparks()
    {
        if (!sparks.isPlaying)
        {
            sparks.gameObject.SetActive(false);
            sparks.gameObject.SetActive(true);
            sparks.Play();
        }
    }
}

