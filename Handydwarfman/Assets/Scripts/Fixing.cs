using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixing : MonoBehaviour
{
    bool isFixed;

    public ToolType toolNeeded;

    void Fix(GameObject usedTool)
    {
        switch (toolNeeded)
        {
            case ToolType.Rag:
                // sprawdzanie czy kontroler jest w ruchu przez x sekund
                // jeśli tak - pokaż particle, zmniejszaj opacity syfu, jeśli nie - zapauzuj timer
                // naprawione
                break;

            case ToolType.Screwdriver:
                // sprawdzanie rotacji kontrolera i rotowanie śruby (gotowy komponent z VRTK)
                // wraz ze zmianą stopniową rotacji śruba wsuwa się dalej
                // naprawione
                break;

            case ToolType.Hammer:
                if (usedTool.GetComponent<Rigidbody>().velocity.magnitude > 2.5f)
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tool>() != null)
            if (collision.gameObject.GetComponent<Tool>().toolType == toolNeeded && toolNeeded != ToolType.Hammer && !isFixed)
                Fix(collision.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Tool>() != null)
            if (collision.gameObject.GetComponent<Tool>().toolType == toolNeeded && toolNeeded == ToolType.Hammer && !isFixed)
                Fix(collision.gameObject);
    }
}
