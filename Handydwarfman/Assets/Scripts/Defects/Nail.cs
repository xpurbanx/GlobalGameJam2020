using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : Fixing
{
    public GameManager GameController;
    public float positionChangeNeeded;  // O ile trzeba przesunąć gwóźdź
    public float decreasingPosition = 0.03f;   // O ile gwóźdź 

    public Collider topCollider;
    private Hammer hammer;

    private Vector3 translateVector;

    public void Fix()
    {
        if (hammer.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
        {
            translateVector = new Vector3(0, decreasingPosition, 0);
            positionChangeNeeded -= decreasingPosition;
            transform.localPosition -= translateVector;
            if (positionChangeNeeded <= 0) isFixed = true;
            GameController.AddPoints(1);
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
