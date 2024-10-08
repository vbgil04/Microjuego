using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance; // Singleton

    public GameObject astGPrefab; // prefabs de los asteroides que luego activaremos al spawnear
    public GameObject astPChiq1Prefab;
    public GameObject astPChiq2Prefab;
    public GameObject bulletPrefab; // prefab de las balas

    public int AsteroidPoolSize = GlobalVariables.maxAsteroides; // tamaño de los pools
    public int AstChiquitoPoolSize = GlobalVariables.maxAsteroides * 2;
    public int BulletPoolSize = 20;

    public List<GameObject> asteroideGPool; // pools de asteroides
    public List<GameObject> asteroideChiq1Pool;
    public List<GameObject> asteroideChiq2Pool;
    public List<GameObject> bulletPool; // pool de balas

    void Awake() {
        if (Instance == null) { 
            Instance = this;
        } else {
            Destroy(gameObject);
        } // me aseguro de que solo haya una instancia de PoolManager, incluso si se carga una escena nueva
        asteroideGPool = CreatePool(astGPrefab, AsteroidPoolSize);
        asteroideChiq1Pool = CreatePool(astPChiq1Prefab, AstChiquitoPoolSize);
        asteroideChiq2Pool = CreatePool(astPChiq2Prefab, AstChiquitoPoolSize);
        bulletPool = CreatePool(bulletPrefab, BulletPoolSize); // creo los pools
    }

    List<GameObject> CreatePool(GameObject prefab, int size){ 
        var lista = new List<GameObject>(); // creo una lista de gameobjects
        for (int i = 0; i < size; i++) { // por cada gameobject que quiero en el pool
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            lista.Add(obj); // lo instancio, lo desactivo y lo agrego a la lista
        }
        return lista; // devuelvo la lista
    }

    //tengo tres métodos para obtener un asteroide de cada tipo, revisando si en el pool hay alguno desactivado y devolviéndolo
    //si no hay ninguno devuelvo null
    public GameObject GetAsteroidG(){
        foreach (GameObject obj in asteroideGPool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }
    public GameObject GetAsteroidChiq1(){
        foreach (GameObject obj in asteroideChiq1Pool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }
    public GameObject GetAsteroidChiq2(){
        foreach (GameObject obj in asteroideChiq2Pool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        return null;
    }

    //método para obtener una bala del pool, revisando si hay alguna desactivada y devolviéndola y si no hay ninguna instanciando una nueva
    //este método es distinto para que no afecte a la jugarilidad, ya que si no hay balas en el pool, se instanciará una nueva
    public GameObject GetBullet(){
        foreach (GameObject obj in bulletPool) {
            if (!obj.activeInHierarchy) {
                return obj;
            }
        }
        GameObject objN = Instantiate(bulletPrefab);
        objN.SetActive(false);
        bulletPool.Add(objN);
        return objN;
    }

    public void DisableAllAsteroids() // método para desactivar todos los asteroides del pool ya que si no se hace, al reiniciar la escena los asteroides seguirán activos
    {
        // Disable all asteroids in the asteroideGPool
        foreach (GameObject asteroid in asteroideGPool)
        {
            asteroid.SetActive(false);
        }

        // Disable all asteroids in the asteroideChiq1Pool
        foreach (GameObject asteroid in asteroideChiq1Pool)
        {
            asteroid.SetActive(false);
        }

        // Disable all asteroids in the asteroideChiq2Pool
        foreach (GameObject asteroid in asteroideChiq2Pool)
        {
            asteroid.SetActive(false);
        }
    }
}
