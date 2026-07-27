using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    public GameObject wall;

    public Collider2D[] ground;
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
            SpawnUnit(u);

            yield return new WaitForSeconds(5f);
        }   
    }
    IEnumerator SpawnGrabwave()
    {
        yield return new WaitForSeconds(10f);
        UIManager.instance.PrintWaveText("! GrabWave !");
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject u = ObjectPoolManager.instance.GetObject("Enemy Grab Unit");
                SpawnUnit(u);
            }
            yield return new WaitForSeconds(7);
        }
    }
    IEnumerator SpawnFinalwave()
    {
        yield return new WaitForSeconds(30f);
        UIManager.instance.PrintWaveText("! FinalWave !");
        while (true)
        {
            foreach (GameObject unit in EnemyUnits)
            {
                yield return new WaitForSeconds(0.1f);
                //GameObject u = Instantiate(unit);
                GameObject u = ObjectPoolManager.instance.GetObject(unit.name);
                SpawnUnit(u);
            }

            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator WallBrokeSpawn()
    {
        UIManager.instance.PrintWaveText("! Culse !");
        while (true)
        {
            //GameObject u = Instantiate(EnemyUnits[5]);
            GameObject u = ObjectPoolManager.instance.GetObject("Enemy Fly Unit");
            SpawnUnit(u);

            yield return new WaitForSeconds(5f);
        }
    }

    void SpawnUnit(GameObject unit)
    {
        unit.layer = 8;
        unit.GetComponent<Unit>().enabled = true;
        unit.transform.position = transform.position;
        unit.SetActive(true);
        unit.GetComponent<Rigidbody2D>().linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
        UIManager.instance.PrintUnitHPbar(unit);

        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), ground[0], true);
        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), ground[1], true);
        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), ground[2], true);
        int myGround = Random.Range(0, 3);
        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), ground[myGround], false);
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
