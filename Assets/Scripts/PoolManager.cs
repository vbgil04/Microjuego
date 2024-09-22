using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public GameObject astGPrefab;
    public GameObject astPChiq1Prefab;
    public GameObject astPChiq2Prefab;
    public GameObject bulletPrefab;

    public int AsteroidPoolSize = GlobalVariables.maxAsteroides;
    public int AstChiquitoPoolSize = GlobalVariables.maxAsteroides * 2;
    public int BulletPoolSize = 20;

    public List<GameObject> asteroideGPool;
    public List<GameObject> asteroideChiq1Pool;
    public List<GameObject> asteroideChiq2Pool;
    public List<GameObject> bulletPool;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        asteroideGPool = CreatePool(astGPrefab, AsteroidPoolSize);
        asteroideChiq1Pool = CreatePool(astPChiq1Prefab, AstChiquitoPoolSize);
        asteroideChiq2Pool = CreatePool(astPChiq2Prefab, AstChiquitoPoolSize);
        bulletPool = CreatePool(bulletPrefab, BulletPoolSize);
    }

    List<GameObject> CreatePool(GameObject prefab, int size){
        var lista = new List<GameObject>();
        for (int i = 0; i < size; i++) {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            lista.Add(obj);
        }
        return lista;
    }

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

    public void DisableAllAsteroids()
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
