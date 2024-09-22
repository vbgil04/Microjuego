using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroides : MonoBehaviour
{
    float rangoBajo=0f; //el rango que utilizo para determinar qué tipo de asteroide voy a spawnear
    float rangoAlto=5f;
    float velocidad; //la velocidad a la que se mueven los asteroides según su tipo
    public GameObject astGPrefab; //prefabs de los asteroides que luego activaremos al spawnear
    public GameObject astPChiq1Prefab;
    public GameObject astPChiq2Prefab;
    public int dondeSpawn = 0; //variable que utilizo para determinar desde qué borde voy a spawnear el asteroide para poder asignarle la dirección correcta
    public float spawnRate = 5f; //la tasa de spawn de los asteroides
    public float qPrefab; //variable que utilizo para determinar qué tipo de asteroide voy a spawnear

    void Start(){
        Spawn(); //hago un spawn inicial para que haya un asteroide al inicio
    }

    void Update()
    {
        if(Time.time>spawnRate){ //si el tiempo actual es mayor a la tasa de spawn
            if(GlobalVariables.cantAsteroides<GlobalVariables.maxAsteroides){ //y si la cantidad de asteroides es menor a la cantidad máxima de asteroides
                Spawn(); //hago un spawn
            }
            spawnRate =Time.time + (2f-0.2f*GlobalVariables.score); //actualizo la tasa de spawn, haciéndola más rápida según la cantidad de asteroides destruidos
        } 
    }
    
    void Spawn(){
        // while(GlobalVariables.cantAsteroides<maxAsteroides){
            qPrefab = Random.Range(rangoBajo, rangoAlto); //determino qué tipo de asteroide voy a spawnear
            Vector3 astGSpawnPoint = GetRandomEdgePosition(); //determino desde qué borde voy a spawnear el asteroide
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
            } //instancio el asteroide según el tipo que haya determinado y le asigno la velocidad correspondiente
            if(ast == null){ //si no hay asteroides en el pool no hago nada
                Debug.Log("No hay asteroides en el pool");
                return;
            }
            ast.transform.position = astGSpawnPoint; //le asigno la posición de spawn al asteroide
            ast.SetActive(true); //activo el asteroide
            
            if(dondeSpawn == 1){
                ast.GetComponent<Rigidbody>().velocity = -transform.up * velocidad;
            } else if(dondeSpawn == 2){
                ast.GetComponent<Rigidbody>().velocity = transform.up * velocidad;
            } else if(dondeSpawn == 3){
                ast.GetComponent<Rigidbody>().velocity = transform.right * velocidad;
            } else {
                ast.GetComponent<Rigidbody>().velocity = -transform.right * velocidad;
            } //le asigno la dirección de movimiento al asteroide dependiendo de desde qué borde se haya spawnado

            Debug.Log("Spawning asteroid at: " + astGSpawnPoint);
            GlobalVariables.IncrementAsteroides(); //incremento la cantidad de asteroides
            Debug.Log("Asteroid count: " + GlobalVariables.cantAsteroides);
        // }
    }

    Vector3 GetRandomEdgePosition(){
        float x, y;
        int edge = Random.Range(0, 4); // 0: top, 1: bottom, 2: left, 3: right
        switch (edge){ //un switch para determinar desde qué borde voy a spawnear el asteroide y asinarle una posición aleatoria dentro de ese borde
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
