using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{ 
    public static int cantAsteroides = 0; // cantidad de asteroides en pantalla
    public static int maxAsteroides = 12; // cantidad máxima de asteroides en pantalla
    public static float vcelocidadGrande = 5f; // velocidad de los asteroides grandes
    public static float vcelocidadChiq = 7.5f; // velocidad de los asteroides chiquitos
    public static float borderX = 22f; // bordes de la pantalla
    public static float bordery = 11f;
    public static int score = 0; //puntaje del jugador

    private static readonly object lockObject = new object();
    private static readonly object lockObject2 = new object(); // lock objects para evitar problemas de concurrencia

    // tengo que hacer estos métodos para incrementar y decrementar la cantidad de asteroides y el puntaje
    // en realidad no es necesario hacerlo, pero lo hice para que sea más ordenado y poque en un momento 
    // llegué a pensar que tenía problemas de concurrencia, y ya que estaba lo dejé 
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
