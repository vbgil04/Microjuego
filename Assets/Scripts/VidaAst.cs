using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaAst : MonoBehaviour
{
    public int vida = 10;

    void Update()
    {
        Vector3 position = transform.position;
        if (position.x > GlobalVariables.borderX)
        {
            gameObject.SetActive(false);
            GlobalVariables.DecrementAsteroides();
        }
        else if (position.x < -GlobalVariables.borderX)
        {
            gameObject.SetActive(false);
            GlobalVariables.DecrementAsteroides();
        }
        if (position.y > GlobalVariables.bordery)
        {
            gameObject.SetActive(false);
            GlobalVariables.DecrementAsteroides();
        }
        else if (position.y < -GlobalVariables.bordery)
        {
            gameObject.SetActive(false);
            GlobalVariables.DecrementAsteroides();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // if (gameObject.CompareTag("Chiquito")) {
        //     Rigidbody rb = GetComponent<Rigidbody>();
        //     rb.velocity = rb.velocity.normalized * GlobalVariables.vcelocidadGrande;
        //     // collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity*GlobalVariables.vcelocidadGrande;
        // }
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * GlobalVariables.vcelocidadGrande;
    }
    // void OnDisable(){
    //     GlobalVariables.DecrementAsteroides();
    //     Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
    // }
}
