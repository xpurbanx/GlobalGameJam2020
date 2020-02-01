using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Player : MonoBehaviour
{
    // singleton player instance
    public static Player instance;

    VRTK_TransformFollow follow;

    public Collider playerBody;

    private GameObject activeCameraRig;

   

    private void Start()
    {
        follow = GetComponent<VRTK_TransformFollow>();
        follow.gameObjectToFollow = activeCameraRig;
        //follow.gameObjectToFollow = VRTK_DeviceFinder.PlayAreaTransform().gameObject;
        follow.Follow();
    }

    void Awake()
    {
        VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
    }
    protected virtual void OnEnable()
    {
        activeCameraRig = GameObject.FindGameObjectWithTag("CameraRig");
    }
    protected virtual void OnDestroy()
    {
        VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Cautious" )
        {
            Debug.Log("Be cautious!");
        }

        if (other.gameObject.name == "Noticed")
        {
            Debug.Log("You are being noticed!");
        }

        if (other.gameObject.name == "Seen")
        {
            Debug.Log("RUNNNN!!!");
        }
    }

    private void BeingCautious()
    {

    }
    private void BeingNoticed()
    {

    }
    private void BeingSeen()
    {

    }
}

