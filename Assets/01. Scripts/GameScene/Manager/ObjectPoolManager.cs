using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    //싱글톤, 미리생성, 필요할때 가져다 쓰기, 사용 후 반납
    public static ObjectPoolManager instance;

    [SerializeField] private List<GameObject> objList = new();
    private Dictionary<string, Queue<GameObject>> pools = new();  //TeamSpawnManager, EnemySpawnManager에서 소환
    //Dictionary<string, IObjectPool<GameObject>> poolss = new();

    //IObjectPool<GameObject> test;

    int poolSize;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        poolSize = 5;

        foreach (GameObject obj in objList)
        {
            pools[obj.name] = new Queue<GameObject>();
            GameObject parentPool = new GameObject($"{obj.name}_Pool");
            parentPool.transform.SetParent(this.transform);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj, parentPool.transform);
                go.SetActive(false);
                pools[obj.name].Enqueue(go);
            }
        }
    }
    public void GameEnd()
    {
        foreach (Transform pool in transform)
        {
            foreach (Transform child in pool)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
        {
            return null;
        }

        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            Transform parent = transform.Find($"{name}_Pool");
            GameObject go = Instantiate(objList.Find(obj => obj.name == name), parent);
            return go;
        }
    }

    public void ReturnObject(string name, GameObject go)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            return;
        }
        go.SetActive(false);
        pools[name].Enqueue(go);
    }
    public void ReturnObject(string name, GameObject go, float time)
    {
        StartCoroutine(ReturnObjectTime(name, go, time));
    }
    IEnumerator ReturnObjectTime(string name, GameObject go, float time)    //EnemyMeleeAttack에서 hitEffect 사라지게할때 사용
    {
        yield return new WaitForSeconds(time);
        if (!pools.ContainsKey(name))
        {
            Destroy(go);
            yield break;
        }
        go.SetActive(false);
        pools[name].Enqueue(go);
    }
}
