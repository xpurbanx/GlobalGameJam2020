using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : Fixing
{
    public float rotationChangeNeeded = 0;
    private float allRotation = 0;
    public Collider topCollider;
    private float lastScrewdriverRotation = 0;

    private Screwdriver screwdriver;


    public void Fix()
    {
        lastScrewdriverRotation = screwdriver.GetComponent<Rigidbody>().rotation.y - lastScrewdriverRotation;
        allRotation += lastScrewdriverRotation;
        if (allRotation >= rotationChangeNeeded)
        {            
            isFixed = true;
        }
    }

    private void OnCollisionStay (Collision collision)
    {
        Debug.Log("There is " + collision.collider.gameObject.name + " in the screw");
        screwdriver = collision.collider.GetComponentInParent<Screwdriver>();
        if (screwdriver != null)
        {
            Debug.Log("Screw is being repaired by " + collision.collider.gameObject.name);
            if (!isFixed)
            {
                Debug.Log(allRotation);
                if (lastScrewdriverRotation < screwdriver.GetComponent<Rigidbody>().rotation.y)
                    Fix();
            }
                
        }
    }


}
