using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    public GameObject wall;

    public Collider2D[] ground;

    WaitForSeconds wave1Time;
    WaitForSeconds waveGrabTime;
    WaitForSeconds wallBrokenTime;
    WaitForSeconds waveFinalTime;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        wave1Time = new WaitForSeconds(5f);
        waveGrabTime = new WaitForSeconds(7f);
        waveFinalTime = new WaitForSeconds(10f);
        wallBrokenTime = new WaitForSeconds(5f);
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

            yield return wave1Time;
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
            yield return waveGrabTime;
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

            yield return waveFinalTime;
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

            yield return wallBrokenTime;
        }
    }

    void SpawnUnit(GameObject unit)
    {
        Unit unitScript = unit.GetComponent<Unit>();
        Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
        Collider2D unitCol = unit.GetComponent<Collider2D>();

        unit.layer = LayerMask.NameToLayer("Enemy");
        unitScript.enabled = true;
        unit.transform.position = transform.position;
        unit.SetActive(true);
        rb.linearVelocity -= new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
        UIManager.instance.PrintUnitHPbar(unit);

        Physics2D.IgnoreCollision(unitCol, ground[0], true);
        Physics2D.IgnoreCollision(unitCol, ground[1], true);
        Physics2D.IgnoreCollision(unitCol, ground[2], true);
        int myGround = Random.Range(0, 3);
        Physics2D.IgnoreCollision(unitCol, ground[myGround], false);
        unit.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y, 2 - myGround);
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
