using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    public GameManager GameController;
    public List<CableEntry> cableEntries;
    public ParticleSystem hearts;

    private bool isFixed;

    public void CheckIfFixed()
    {
        bool isFixed = true;

        foreach (CableEntry cableEntry in cableEntries)
        {
            if (!cableEntry.isGood) isFixed = false;
        }

        if (isFixed) Fixed();
    }

    void Fixed()
    {
        if (!hearts.isPlaying)
        {
            hearts.Play();
            GameController.AddPoints(1);
        }

    }
}
