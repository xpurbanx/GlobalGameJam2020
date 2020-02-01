using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandDisappear : MonoBehaviour
{
    public GameObject left, right;

    private void Start()
    {
        left.SetActive(false);
        left.SetActive(true);
        right.SetActive(false);
        right.SetActive(true);
    }

    public void DisappearOnGrab(GameObject hand)
    {
        hand.SetActive(false);
    }

    public void AppearOnUngrab(GameObject hand)
    {
        hand.SetActive(true);
    }
}
