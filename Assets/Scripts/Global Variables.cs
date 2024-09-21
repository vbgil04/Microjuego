using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{ 
    public static int cantAsteroides = 0;
    public static int maxAsteroides = 12;
    public static float vcelocidadGrande = 5f;
    public static float vcelocidadChiq = 7.5f;
    public static float borderX = 22f;
    public static float bordery = 11f;
    public static int score = 0;

    private static readonly object lockObject = new object();
    private static readonly object lockObject2 = new object();

    public static void IncrementAsteroides()
    {
        lock (lockObject)
        {
            cantAsteroides++;
        }
    }

    public static void DecrementAsteroides()
    {
        lock (lockObject)
        {
            cantAsteroides--;
        }
    }
    public static void IncrementarPuntos(){
        lock (lockObject2)
        {
            score++;
        }
    }
    public static void DecrementarPuntos(){
        lock (lockObject2)
        {
            score--;
        }
    }
}
