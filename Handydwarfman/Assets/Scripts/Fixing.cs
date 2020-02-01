using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixing : MonoBehaviour
{
    public ParticleSystem dirtParticle;
    public ToolType toolNeeded; // Narzędzie potrzebne do naprawienia tej usterki
    public float positionChangeNeeded;  // O ile trzeba przesunąć gwóźdź

    bool isFixed;   // Czy usterka jest naprawiona
    float decreasingDirtOverTime = 0.01f;   // O ile zmniejsza się alpha materiału brudu, każdorazowo gdy prędkość ścierki jest > 0.3f
    float decreasingPosition = 0.03f;   // O ile gwóźdź 

    void Fix(GameObject usedTool)
    {
        switch (toolNeeded)
        {
            case ToolType.Rag:
                if (usedTool.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
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

                break;

            case ToolType.Screwdriver:
                // sprawdzanie rotacji kontrolera i rotowanie śruby (gotowy komponent z VRTK)
                // wraz ze zmianą stopniową rotacji śruba wsuwa się dalej
                // naprawione
                break;

            case ToolType.Hammer:
                if (usedTool.GetComponent<Rigidbody>().velocity.magnitude > 1f)
                {
                    positionChangeNeeded -= decreasingPosition;
                    transform.position = new Vector3(transform.position.x, transform.position.y - decreasingPosition, transform.position.z);
                    if (positionChangeNeeded <= 0) isFixed = true;
                }
                break;

            case ToolType.Pliers:
                // sprawdzenie czy przytrzymano przycisk trigger (na końcówce)
                // sprawdzenie czy końcówka znajduje się w swoim snapping zonie (w kontakcie)
                // kiedy puścimy końcówkę w snapping zonie blokuje się możliwość podniesienia końcówki
                // naprawione
                break;

            case ToolType.Scissors:
                // sprawdzenie czy naciśnięto trigger
                // sprawdzenie czy ucięliśmy kapsułkę pierwszą
                // naprawione
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>() != null)
            if (other.gameObject.GetComponent<Tool>().toolType == toolNeeded && toolNeeded != ToolType.Hammer && !isFixed)
                Fix(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>() != null)
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.GetComponent<Tool>().toolType == toolNeeded && toolNeeded == ToolType.Hammer && !isFixed)
                Fix(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Tool>() != null && dirtParticle != null)
            if (dirtParticle.isPlaying) dirtParticle.Stop();
    }
}
