using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StringParent : MonoBehaviour
{
    public GameObject Left, Right;
    public Scissors scissors;
    public bool triggerPressed;
    public bool isFixed;

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

    public void Fix(int index)
    {
        if (index > 0)
            parts[index].GetComponent<CharacterJoint>().connectedBody = null;
        else
        {
            isFixed = true;
            Destroy(parts[index].gameObject);
        }

    }

    private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = true;
    }

    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = false;
    }
}
