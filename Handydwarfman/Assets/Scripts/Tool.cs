using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    /// TUTAJ OKREŚLAMY JAKIE NARZĘDZIE JEST POTRZEBNE DO USUNIĘCIA USTERKI ///
    Rag,            // Szmata
    Screwdriver,    // Śrubokręt
    Hammer,         // Młotek
    Pliers,         // Kombinerki
    Scissors,       // Nożyczki
}

public class Tool : MonoBehaviour
{
    public ToolType toolType;
}
