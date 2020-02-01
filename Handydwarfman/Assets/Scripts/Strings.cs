using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strings : MonoBehaviour
{
    public int partsNumber;
    Rigidbody[] parts;
    public GameObject stringPartPrefab;

    private void Start()
    {
        parts = new Rigidbody [partsNumber];
        for (int i = 0; i < partsNumber; i++)
        {
            //rigidbody = Instantiate(stringPartPrefab).GetComponent<Rigidbody>();
            parts[i] = Instantiate(stringPartPrefab).GetComponent<Rigidbody>();
            if (i > 0)
                parts[i - 1].gameObject.GetComponent<CharacterJoint>().connectedBody = parts[i];
        }
    }
}
