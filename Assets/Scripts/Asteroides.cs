using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroides : MonoBehaviour
{
    float rangoBajo=0f;
    float rangoAlto=5f;
    float velocidad;
    public GameObject astGPrefab;
    public GameObject astPChiq1Prefab;
    public GameObject astPChiq2Prefab;
    public int dondeSpawn = 0;
    public float spawnRate = 5f;
    public float qPrefab;

    void Start(){
        Spawn();
    }

    void Update()
    {
        if(Time.time>spawnRate){
            if(GlobalVariables.cantAsteroides<GlobalVariables.maxAsteroides){
                Spawn();
            }
            spawnRate =Time.time + (2f-0.2f*GlobalVariables.score);
        } 
    }
    
    void Spawn(){
        // while(GlobalVariables.cantAsteroides<maxAsteroides){
            qPrefab = Random.Range(rangoBajo, rangoAlto);
            Vector3 astGSpawnPoint = GetRandomEdgePosition();
            GameObject ast = null;
            if(qPrefab<2){
                // ast = Instantiate(astPChiq2Prefab, astGSpawnPoint, Quaternion.identity);
                ast = PoolManager.Instance.GetAsteroidChiq2();
                velocidad = GlobalVariables.vcelocidadChiq;
            } else if(qPrefab<4){
                // ast = Instantiate(astPChiq1Prefab, astGSpawnPoint, Quaternion.identity);
                ast = PoolManager.Instance.GetAsteroidChiq1();
                velocidad = GlobalVariables.vcelocidadChiq;
            } else {
                // ast = Instantiate(astGPrefab, astGSpawnPoint, Quaternion.identity);
                ast = PoolManager.Instance.GetAsteroidG();
                velocidad = GlobalVariables.vcelocidadGrande;
            }
            if(ast == null){
                Debug.Log("No hay asteroides en el pool");
                return;
            }
            ast.transform.position = astGSpawnPoint;
            ast.SetActive(true);
            
            if(dondeSpawn == 1){
                ast.GetComponent<Rigidbody>().velocity = -transform.up * velocidad;
            } else if(dondeSpawn == 2){
                ast.GetComponent<Rigidbody>().velocity = transform.up * velocidad;
            } else if(dondeSpawn == 3){
                ast.GetComponent<Rigidbody>().velocity = transform.right * velocidad;
            } else {
                ast.GetComponent<Rigidbody>().velocity = -transform.right * velocidad;
            }

            Debug.Log("Spawning asteroid at: " + astGSpawnPoint);
            GlobalVariables.IncrementAsteroides();
            Debug.Log("Asteroid count: " + GlobalVariables.cantAsteroides);
        // }
    }

    Vector3 GetRandomEdgePosition(){
        float x, y;
        int edge = Random.Range(0, 4); // 0: top, 1: bottom, 2: left, 3: right
        switch (edge){
            case 0: // Top edge
                x = Random.Range(-GlobalVariables.borderX, GlobalVariables.borderX);
                y = GlobalVariables.bordery;
                dondeSpawn = 1;
                break;
            case 1: // Bottom edge
                x = Random.Range(-GlobalVariables.borderX, GlobalVariables.borderX);
                y = -GlobalVariables.bordery;
                dondeSpawn = 2;
                break;
            case 2: // Left edge
                x = -GlobalVariables.borderX;
                y = Random.Range(-GlobalVariables.bordery, GlobalVariables.bordery);
                dondeSpawn = 3;
                break;
            case 3: // Right edge
                x = GlobalVariables.borderX;
                y = Random.Range(-GlobalVariables.bordery, GlobalVariables.bordery);
                dondeSpawn = 4;
                break;
            default:
                x = 0;
                y = 0;
                dondeSpawn = 0;
                break;
            }
        return new Vector3(x, y, 0);
    }
}
