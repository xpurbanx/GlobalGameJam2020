using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StringPieces : Fixing
{
    private Scissors scissors;
    Rigidbody[] parts;
    bool triggerPressed;

    public GameObject Left, Right;

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

    void Fix()
    {
        Debug.Log("CIĘCIE");

    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("okooook");
        if (scissors != null && !isFixed)
        {
            if (triggerPressed)
                Fix();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (scissors != null)
        {
            scissors = null;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("ok");
        scissors = other.gameObject.GetComponent<Scissors>();
    }

    public void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = true;
    }

    public void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = false;
    }
}
