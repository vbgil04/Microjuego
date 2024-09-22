using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPausa : MonoBehaviour
{
    public GameObject panelPausa;
    public void Despasuar(){
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
        Pausa.pausado = false;
    }
    public void Salir(){
        Application.Quit();
    }
    public void Reiniciar(){
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
        Pausa.pausado = false;
        GlobalVariables.score=0;
        GlobalVariables.cantAsteroides=0;
        PoolManager.Instance.DisableAllAsteroids();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
