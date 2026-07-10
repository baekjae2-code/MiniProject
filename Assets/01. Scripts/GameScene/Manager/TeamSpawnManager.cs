using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TeamSpawnManager : MonoBehaviour
{
    public static TeamSpawnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject[] teamUnits;

    private void Start()
    {
        StartCoroutine(SpawnUnits());
    }

    public void Spawn()
    {
        StartCoroutine(SpawnUnits());        
    }

    IEnumerator SpawnUnits()
    {
        while (true)
        {
            foreach (GameObject unit in teamUnits)
            {
                GameObject u = Instantiate(unit);
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity += new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void GameOver()
    {
        StopAllCoroutines();
    }
}
