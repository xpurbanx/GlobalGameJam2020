using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DestroySnappedObject : MonoBehaviour
{
    public VRTK_SnapDropZone dropZone;

    // Start is called before the first frame update
    void Start()
    {
        dropZone = GetComponent<VRTK_SnapDropZone>();
    }

    private void OnEnable()
    {
        dropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(DestroyNail);
    }

    private void OnDisable()
    {
        dropZone.ObjectSnappedToDropZone -= new SnapDropZoneEventHandler(DestroyNail);
    }

    // Update is called once per frame
    private void DestroyNail(object sender, SnapDropZoneEventArgs e)
    {
        Destroy(dropZone.GetCurrentSnappedObject());
    }
}
