using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NaveFuncionamiento : MonoBehaviour
{
    public Transform bulletSpawnPoint; // punto de salida de la bala
    public float bulletSpeed = 10; // velocidad de la bala
    public float rotationSpeed = 120f; // velocidad de rotación de la nave
    public float thrustforce = 20f; // fuerza de empuje de la nave
    private Rigidbody _rigid; // rigidbody de la nave
    public float brakingFactor = 1.0f; // factor de frenado
    void Start() {
        _rigid = GetComponent<Rigidbody>(); // obtengo el rigidbody de la nave
    }
    void Update() {
        Vector3 position = transform.position; //cojo la posicion del objeto
        if (position.x > GlobalVariables.borderX)
        {
            position.x = -GlobalVariables.borderX+1;
        }
        else if (position.x < -GlobalVariables.borderX)
        {
            position.x = GlobalVariables.borderX-1;
        }
        if (position.y > GlobalVariables.bordery)
        {
            position.y = -GlobalVariables.bordery+0.5f;
        }
        else if (position.y < -GlobalVariables.bordery)
        {
            position.y = GlobalVariables.bordery-0.5f;
        }
        transform.position = position; //si se sale de los bordes, lo pongo en el borde opuesto

        Shooting();
        Puntaje();
        
        float rotation = Input.GetAxis("Horizontal")  * Time.deltaTime; // obtengo el input de rotación
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime; // obtengo el input de empuje
        Vector3 thrustDirection = transform.right; // obtengo la dirección de empuje
        _rigid.AddForce(thrustDirection * thrustforce * thrust); // aplico la fuerza de empuje
        transform.Rotate(0, 0, -rotation * rotationSpeed); // aplico la rotación
        if (Input.GetKey(KeyCode.F)){ // si se presiona la tecla F
            Vector3 brakingForce = -_rigid.velocity * brakingFactor;
            _rigid.AddForce(brakingForce);
        } // aplico la fuerza de frenado
    }
 
    void Shooting() {
        if(Input.GetKeyDown(KeyCode.Space)) { // si se presiona la tecla espacio
            var bullet = PoolManager.Instance.GetBullet(); // obtengo una bala del pool
            bullet.transform.position = bulletSpawnPoint.position; // la posiciono en el punto de salida
            bullet.transform.rotation = bulletSpawnPoint.rotation; // la roto en la dirección del punto de salida
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>(); // obtengo el rigidbody de la bala
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero; //estas dos líneas son para que la bala no gire de forma extraña
            bullet.SetActive(true); // activo la bala
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.right * bulletSpeed; // aplico la velocidad a la bala
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Chiquito") || collision.gameObject.CompareTag("Grande")) {  // si colisiona con un asteroide
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reinicio la escena
            GlobalVariables.score=0; // reinicio el puntaje
            GlobalVariables.cantAsteroides=0; // reinicio la cantidad de asteroides
            PoolManager.Instance.DisableAllAsteroids(); // desactivo todos los asteroides del pool (esto porque el pool no se reinicia con los loadscene)
        }
    }
    void Puntaje() { // función para mostrar el puntaje en pantalla
        GameObject.FindObjectOfType<UnityEngine.UI.Text>().text = "Puntuación    " + GlobalVariables.score;
    }
}
