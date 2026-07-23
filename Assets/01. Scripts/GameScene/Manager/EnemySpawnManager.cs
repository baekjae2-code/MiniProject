using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    public GameObject wall;

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
            //GameObject u = Instantiate(EnemyUnits[0]);
            GameObject u = ObjectPoolManager.instance.GetObject("Enemy Melee Unit");
            print("getObj :" + u.name);
            u.transform.position = transform.position;
            u.SetActive(true);
            u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            UIManager.instance.PrintUnitHPbar(u);

            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator SpawnGrabwave()
    {
        yield return new WaitForSeconds(0);
        UIManager.instance.PrintWaveText("! GrabWave !");
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject u = ObjectPoolManager.instance.GetObject("Enemy Grab Unit");
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
                UIManager.instance.PrintUnitHPbar(u);
            }
            yield return new WaitForSeconds(15);
        }
    }
    IEnumerator SpawnFinalwave()
    {
        yield return new WaitForSeconds(60f);
        UIManager.instance.PrintWaveText("! FinalWave !");
        while (true)
        {
            foreach (GameObject unit in EnemyUnits)
            {
                yield return new WaitForSeconds(0.1f);
                //GameObject u = Instantiate(unit);
                GameObject u = ObjectPoolManager.instance.GetObject(unit.name);
                u.transform.position = transform.position;
                u.SetActive(true);
                u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
                UIManager.instance.PrintUnitHPbar(u);
            }

            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator WallBrokeSpawn()
    {
        UIManager.instance.PrintWaveText("! Culse !");
        while (true)
        {
            //GameObject u = Instantiate(EnemyUnits[5]);
            GameObject u = ObjectPoolManager.instance.GetObject("Enemy Fly Unit");
            u.transform.position = transform.position;
            u.SetActive(true);
            u.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            UIManager.instance.PrintUnitHPbar(u);

            yield return new WaitForSeconds(5f);
        }
    }

    public void SpawnWall()
    {
        StartCoroutine(WallBrokeSpawn());
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
