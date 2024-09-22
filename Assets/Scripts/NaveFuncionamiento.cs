using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NaveFuncionamiento : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10;
    // public float moveSpeed = 2;
    public float rotationSpeed = 120f;
    public float thrustforce = 20f;
    private Rigidbody _rigid;

    public float brakingFactor = 1.0f;
 
 
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 position = transform.position;
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
        transform.position = position;

        Shooting();
        Puntaje();
        
        float rotation = Input.GetAxis("Horizontal")  * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;
        Vector3 thrustDirection = transform.right;
        _rigid.AddForce(thrustDirection * thrustforce * thrust);
        transform.Rotate(0, 0, -rotation * rotationSpeed);
        if (Input.GetKey(KeyCode.F)){
            Vector3 brakingForce = -_rigid.velocity * brakingFactor;
            _rigid.AddForce(brakingForce);
        }

    }
 
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var bullet = PoolManager.Instance.GetBullet();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.right * bulletSpeed;
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Chiquito") || collision.gameObject.CompareTag("Grande")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GlobalVariables.score=0;
            GlobalVariables.cantAsteroides=0;
            PoolManager.Instance.DisableAllAsteroids();
            // SceneManager.LoadScene("GameOver");
        }
    }
    void Puntaje()
    {
        //look for the text in the canvas and change the score
        GameObject.FindObjectOfType<UnityEngine.UI.Text>().text = "Puntuación    " + GlobalVariables.score;
    }
}
