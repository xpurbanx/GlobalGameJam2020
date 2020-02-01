using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : Fixing
{
    public float positionChangeNeeded;  // O ile trzeba przesunąć gwóźdź
    public float decreasingPosition = 0.03f;   // O ile gwóźdź 

    public Collider topCollider;
    private Hammer hammer;

    public void Fix()
    {
        if (hammer.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
        {
            positionChangeNeeded -= decreasingPosition;
            transform.position = new Vector3(transform.position.x, transform.position.y - decreasingPosition, transform.position.z);
            if (positionChangeNeeded <= 0) isFixed = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Nail is hit by " + collision.collider.gameObject.name);
        hammer = collision.collider.GetComponentInParent<Hammer>();
        if (hammer != null)
        {
            Debug.Log("Nail is being repaired by  " + collision.collider.gameObject.name);
            if (!isFixed)
                Fix();
        }
    }
}
