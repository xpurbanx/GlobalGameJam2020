using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Fixing
{
    public ParticleSystem dirtParticle;
    public float decreasingDirtOverTime = 0.01f;   // O ile zmniejsza się alpha materiału brudu, każdorazowo gdy prędkość ścierki jest > 0.3f

    private Rag rag;

    private void Fix()
    {
        if (rag.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
        {
            if (!dirtParticle.isPlaying) dirtParticle.Play();
            Color color = GetComponent<MeshRenderer>().material.color;
            GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, color.a - decreasingDirtOverTime);
            if (color.a - decreasingDirtOverTime <= 0)
            {
                isFixed = true;
            }
        }

        else
        {
            dirtParticle.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (rag != null && !isFixed)
        {
            Fix();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        rag = other.gameObject.GetComponent<Rag>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (rag != null && dirtParticle != null)
        {
            if (dirtParticle.isPlaying) dirtParticle.Stop();
            rag = null;
        }

    }

}
