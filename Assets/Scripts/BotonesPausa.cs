using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPausa : MonoBehaviour
{
    public GameObject panelPausa; // panel de pausa
    public void Despasuar(){ //este método te devuelve a donde estabas antes de pausar
        Time.timeScale = 1f; // reanudo el tiempo
        panelPausa.SetActive(false); // desactivo el panel de pausa
        Pausa.pausado = false; // cambio la variable de pausa a falso
    }
    public void Salir(){
        Application.Quit(); // cierro la aplicación
    }
    public void Reiniciar(){ // este método reinicia la escena
        Time.timeScale = 1f; // reanudo el tiempo
        panelPausa.SetActive(false); // desactivo el panel de pausa
        Pausa.pausado = false; // cambio la variable de pausa a falso
        GlobalVariables.score=0; // reinicio los puntos 
        GlobalVariables.cantAsteroides=0; // reinicio la cantidad de asteroides
        PoolManager.Instance.DisableAllAsteroids(); // desactivo todos los asteroides
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recargo la escena actual
    }
}
