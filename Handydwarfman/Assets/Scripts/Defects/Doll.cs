using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    public GameManager GameController;
    public List<CableEntry> cableEntries;
    public ParticleSystem hearts;

    private bool isFixed;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

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
