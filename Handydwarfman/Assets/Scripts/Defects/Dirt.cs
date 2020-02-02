using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Fixing
{
    public GameManager GameController;
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
                GameController.AddPoints(1);
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                dirtParticle.Stop();
                rag = null;
            }
        }

        else
        {
            dirtParticle.Stop();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (rag != null && !isFixed)
        {
            Fix();
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        rag = other.gameObject.GetComponent<Rag>();
    }

    private void OnCollisionExit(Collision other)
    {
        if (rag != null && dirtParticle != null)
        {
            if (dirtParticle.isPlaying) dirtParticle.Stop();
            rag = null;
        }

    }

}
