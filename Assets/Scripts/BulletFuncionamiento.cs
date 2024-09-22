using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFuncionamiento : MonoBehaviour
{
    public float life = 3; // tiempo de vida de la bala
    public float nacimiento; // cuando se dispara la bala
    public GameObject astPChiq1Prefab; // prefabs para cuando se destruye un asteroide grande se divida en dos chiquitos
    public GameObject astPChiq2Prefab;
    public float multiplicador = 15f;

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
            // var tr1 = Instantiate(astPChiq1Prefab);
            // var tr2 = Instantiate(astPChiq2Prefab);
            if(tr1 == null || tr2 == null){
                Debug.Log("No hay asteroides en el pool");
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                GlobalVariables.DecrementAsteroides();
                GlobalVariables.IncrementarPuntos();
                return;
            }
            tr1.transform.position = position + new Vector3(1,1,0);
            tr2.transform.position = position + new Vector3(-1,-1,0);
            tr1.SetActive(true);
            tr2.SetActive(true);

            //distance entre dos untos
            Vector3 g1 = new Vector3(3,3,0);
            Vector3 g2 = new Vector3(-3,-3,0);
            g1+=transform.position;
            g2+=transform.position;

            tr1.GetComponent<Rigidbody>().AddForce(g1* multiplicador );
            tr2.GetComponent<Rigidbody>().AddForce(g2* multiplicador );

            // tr1.GetComponent<Rigidbody>().velocity = tr1.GetComponent<Rigidbody>().velocity* multiplicador;
            // tr2.GetComponent<Rigidbody>().velocity =  tr2.GetComponent<Rigidbody>().velocity* multiplicador;

            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            // Destroy(collision.gameObject);
            GlobalVariables.DecrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementAsteroides();
            GlobalVariables.IncrementarPuntos();
        }
    }
    
}
