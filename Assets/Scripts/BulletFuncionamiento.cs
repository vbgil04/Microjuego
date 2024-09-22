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
    public Transform bulletspawn;
    public float multiplicador = 15f;

    void Start()
    {
        bulletspawn = GameObject.Find("BulletSpawner").transform;
    }
    void OnEnable()
    {
        // Destroy(gameObject, life);
        nacimiento = Time.time;
    }
    void Update()
    {
        if(Time.time > nacimiento + life){
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chiquito")) {
            GlobalVariables.IncrementarPuntos();
            GlobalVariables.DecrementAsteroides();
            collision.gameObject.SetActive(false); 
            gameObject.SetActive(false);
            Debug.Log("Asteroid destroyed. Current asteroid count: " + GlobalVariables.cantAsteroides);
        }
        if (collision.gameObject.CompareTag("Grande")) {
            var aster = collision.gameObject;
            Vector3 position = aster.transform.position;
            var tr1 = PoolManager.Instance.GetAsteroidChiq1();
            var tr2 = PoolManager.Instance.GetAsteroidChiq2();
            
            if(tr1 == null || tr2 == null){
                Debug.Log("No hay asteroides en el pool");
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                GlobalVariables.DecrementAsteroides();
                GlobalVariables.IncrementarPuntos();
                return;
            }
            tr1.transform.position = position + new Vector3(0.25f,0.25f,0);
            tr2.transform.position = position + new Vector3(-0.25f,-0.25f,0);

            Vector3 distancia= collision.transform.position - bulletspawn.position;
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            tr1.SetActive(true);
            tr2.SetActive(true);
            // rotate the two asteroids 30º in the z axis
            tr1.transform.Rotate(0, 0, 30);
            tr2.transform.Rotate(0, 0, -30);
            // add force to the two asteroids
            tr1.GetComponent<Rigidbody>().velocity = (distancia.normalized + new Vector3 (1,1,0)) * multiplicador;
            tr2.GetComponent<Rigidbody>().velocity = (distancia.normalized + new Vector3 (-1,-1,0)) * multiplicador;

            GlobalVariables.DecrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementarPuntos();
        }
    }
    
}
