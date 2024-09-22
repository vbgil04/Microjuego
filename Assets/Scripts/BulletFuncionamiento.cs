using System.Collections;
using System.Collections.Generic;
// using System.Numerics; // Remove this line as it's not needed
using Unity.VisualScripting;
using UnityEngine;

public class BulletFuncionamiento : MonoBehaviour
{
    public float life = 3; // tiempo de vida de la bala
    public float nacimiento; // cuando se dispara la bala
    public GameObject astPChiq1Prefab; // prefabs para cuando se destruye un asteroide grande se divida en dos chiquitos
    public GameObject astPChiq2Prefab;
    public Transform bulletspawn; // punto de salida de la bala
    public float multiplicador = 15f; // multiplicador de la velocidad de los asteroides chiquitos

    void Start()
    {
        bulletspawn = GameObject.Find("BulletSpawner").transform; // obtengo el punto de salida de la bala
    }
    void OnEnable() {
        nacimiento = Time.time; // guardo el tiempo de nacimiento de la bala
    }
    void Update() {
        if(Time.time > nacimiento + life) { // si el tiempo actual es mayor al tiempo de nacimiento más el tiempo de vida
            gameObject.SetActive(false); // desactivo la bala
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Chiquito")) { // si colisiona con un asteroide chiquito
            GlobalVariables.IncrementarPuntos(); // incremento los puntos
            GlobalVariables.DecrementAsteroides(); // decremento la cantidad de asteroides
            collision.gameObject.SetActive(false);  // desactivo el asteroide
            gameObject.SetActive(false); // desactivo la bala
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        }
        if (collision.gameObject.CompareTag("Grande")) { // si colisiona con un asteroide grande
            var aster = collision.gameObject; 
            Vector3 position = aster.transform.position; // guardo la posición del asteroide
            var tr1 = PoolManager.Instance.GetAsteroidChiq1(); // obtengo un asteroide chiquito del pool
            var tr2 = PoolManager.Instance.GetAsteroidChiq2(); // obtengo el otro asteroide chiquito del pool
            
            if(tr1 == null || tr2 == null){ // si no hay asteroides en el pool desactivo la bala y el asteroide y hago las operaciones correspondientes
                Debug.Log("No hay asteroides en el pool");
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                GlobalVariables.DecrementAsteroides();
                GlobalVariables.IncrementarPuntos();
                return;
            }
            tr1.transform.position = position + new Vector3(0.25f,0.25f,0); 
            tr2.transform.position = position + new Vector3(-0.25f,-0.25f,0); // posiciono los asteroides chiquitos con un ligero offset

            Vector3 distancia= collision.transform.position - bulletspawn.position; // guardo la distancia entre el asteroide y el punto de salida de la bala
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false); // desactivo la bala y el asteroide grande
            tr1.SetActive(true);
            tr2.SetActive(true); // activo los asteroides chiquitos
            
            tr1.transform.Rotate(0, 0, 30);
            tr2.transform.Rotate(0, 0, -30); // roto los asteroides chiquitos para que no vayan en la misma dirección

            tr1.GetComponent<Rigidbody>().velocity = (distancia.normalized + new Vector3 (1,1,0)) * multiplicador;
            tr2.GetComponent<Rigidbody>().velocity = (distancia.normalized + new Vector3 (-1,-1,0)) * multiplicador; // asigno la velocidad a los asteroides chiquitos con un offset

            GlobalVariables.DecrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementarPuntos(); // hago las operaciones correspondientes
        }
    }
    
}
