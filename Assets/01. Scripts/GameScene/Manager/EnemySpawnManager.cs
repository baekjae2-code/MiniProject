using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject[] EnemyUnits;

    private void Start()
    {
        StartCoroutine(SpawnUnits());
    }
    IEnumerator SpawnUnits()
    {
        while (true)
        {
            foreach (GameObject unit in EnemyUnits)
            {
                GameObject u = Instantiate(unit);
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            }
            yield return new WaitForSeconds(3f);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
