using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    public GameObject panelPausa;
    public static bool pausado = false;
    void Start()
    {
        panelPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            if (pausado){
                Reanudar();
            } else {
                Pausar();
            }
        }
    }
    public void Pausar(){
        Time.timeScale = 0f;
        panelPausa.SetActive(true);
        pausado = true;
    }
    public void Reanudar(){
        Time.timeScale = 1f;
        panelPausa.SetActive(false);
        pausado = false;
    }
}
