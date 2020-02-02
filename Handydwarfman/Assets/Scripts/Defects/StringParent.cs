﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StringParent : MonoBehaviour
{
    public GameObject Left, Right;
    public Scissors scissors;
    public bool triggerPressed;
    public bool isFixed;
    private VRTK_InteractGrab grab;

    Rigidbody[] parts;


    void Start()
    {
        Left.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerClicked);
        Right.GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerClicked);
        Left.GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
        Right.GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);

        parts = new Rigidbody[10];

        for (int i = 0; i < 10; i++)
        {
            parts[i] = gameObject.transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>();
        }
    }

    public void SetScissors()
    {
        if (grab != null)
        {
            if (grab.GetGrabbedObject().GetComponent<Scissors>() != null)
                scissors = grab.GetGrabbedObject().GetComponent<Scissors>();
        }

    }

    public void Fix(int index)
    {
        if (index > 0)
            Destroy(parts[index].GetComponent<CharacterJoint>());
        else
        {
            isFixed = true;
            Destroy(parts[index].gameObject);
        }

    }

    private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        if (scissors != null)
        {
            triggerPressed = true;
            scissors.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            scissors.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log(scissors.gameObject.transform.GetChild(0).gameObject.name);
        }

    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (scissors != null)
        {
            triggerPressed = false;
            scissors.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            scissors.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}
