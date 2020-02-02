using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{

    public enum SeenState {Cautious, Noticed, Seen };

    public Collider fovCautious;
    public Collider fovNoticed;
    public Collider fovSeen;

    public GameObject head;
    public List<Transform> lookTargets;
    public float lookSpeed;
    public float minDelay;
    public float maxDelay;

    [SerializeField]
    private Transform currentTarget;
    private Quaternion rotation;


    private void Start()
    {
        StartCoroutine("ILookAtNextTarget");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == Player.instance.playerBody)
        {
            
        }
    }
    private void Update()
    {
        //rotation = Quaternion.LookRotation(currentTarget.position - head.transform.position);
        //head.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);

        rotation = Quaternion.LookRotation(currentTarget.position - head.transform.position);
            // Will assume you mean to divide by damping meanings it will take damping seconds to face target assuming it doesn't move
            head.transform.rotation = Quaternion.Slerp(head.transform.rotation, rotation, Time.deltaTime * lookSpeed);

    }

    public void LookAtNextTarget()
    {
        Debug.Log("GETTING TARGET");
        currentTarget = lookTargets[Random.Range(0, lookTargets.Count)];
        //var step = lookSpeed * Time.deltaTime;
        //head.transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTarget.rotation, step);

       // head.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, step);
       // head.transform.LookAt(currentTarget);


    }


    IEnumerator ILookAtNextTarget()
    {
        while (true)
        {
            LookAtNextTarget();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }

    }
}
