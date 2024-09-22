using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaAst : MonoBehaviour
{
    void Update()
    {
        Vector3 position = transform.position; //cojo la posicion del objeto
        if (position.x > GlobalVariables.borderX) //si la posicion en x es mayor que el borde x
        {
            gameObject.SetActive(false);
            // Destroy(gameObject);
            GlobalVariables.DecrementAsteroides();
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        } //guardo el objeto en el pool y muestro el mensaje
        else if (position.x < -GlobalVariables.borderX) //si la posicion en x es menor que el borde x
        {
            gameObject.SetActive(false);
            // Destroy(gameObject);
            GlobalVariables.DecrementAsteroides();
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        } //guardo el objeto en el pool y muestro el mensaje
        if (position.y > GlobalVariables.bordery) //si la posicion en y es mayor que el borde y
        {
            gameObject.SetActive(false);
            // Destroy(gameObject);
            GlobalVariables.DecrementAsteroides();
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        } //guardo el objeto en el pool y muestro el mensaje
        else if (position.y < -GlobalVariables.bordery) //si la posicion en y es menor que el borde y
        {
            gameObject.SetActive(false);
            // Destroy(gameObject);
            GlobalVariables.DecrementAsteroides();
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        }   //guardo el objeto en el pool y muestro el mensaje
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (gameObject.CompareTag("Chiquito")) {
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.velocity = rb.velocity.normalized * GlobalVariables.vcelocidadGrande;
        //     // collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity*GlobalVariables.vcelocidadGrande;
        // }
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * GlobalVariables.vcelocidadGrande; //esto es únicamente para que no pierdan velocidad al chocarse los asteroides
    }
    // void OnDisable(){
    //     GlobalVariables.DecrementAsteroides();
    //     Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
    // }
}
