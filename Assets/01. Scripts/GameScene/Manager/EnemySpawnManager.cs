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
        StartCoroutine(Spawn1wave());
        StartCoroutine(SpawnGrabwave());
        StartCoroutine(SpawnFinalwave());
    }
    IEnumerator Spawn1wave()
    {
        while (true)
        {
            GameObject u = Instantiate(EnemyUnits[1]);
            u.transform.position = transform.position;
            u.SetActive(true);
            u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));

            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator SpawnGrabwave()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject u = Instantiate(EnemyUnits[3]);
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            }
            yield return new WaitForSeconds(15);
        }
    }
    IEnumerator SpawnFinalwave()
    {
        yield return new WaitForSeconds(30f);
        while (true)
        {
            foreach (GameObject unit in EnemyUnits)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject u = Instantiate(unit);
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            }

            yield return new WaitForSeconds(4f);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
