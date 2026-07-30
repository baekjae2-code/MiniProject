using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TeamSpawnManager : MonoBehaviour
{
    public static TeamSpawnManager instance;

    public Collider2D[] ground;

    WaitForSeconds[] unitTime;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Spawn(0, 5);
    }

    public void Spawn(int spawnUnitNum, float spawnTime)
    {
        StartCoroutine(SpawnUnits(spawnUnitNum, spawnTime));
    }

    IEnumerator SpawnUnits(int spawnUnitNum, float spawnTime)
    {
        while (true)
        {
            GameObject u = ObjectPoolManager.instance.GetObject(GameManager.instance.unitsPrefab[spawnUnitNum].name);
            //GameObject u = Instantiate(GameManager.instance.unitsPrefab[spawnUnitNum]);

            Unit unitScript = u.GetComponent<Unit>();
            Rigidbody2D rb = u.GetComponent<Rigidbody2D>();
            Collider2D uCol = u.GetComponent<Collider2D>();
            
            u.layer = LayerMask.NameToLayer("Player");
            unitScript.enabled = true;
            u.transform.position = transform.position;
            u.SetActive(true);
            rb.linearVelocity += new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            UIManager.instance.PrintUnitHPbar(u);

            Physics2D.IgnoreCollision(uCol, ground[0], true);   //맨위
            Physics2D.IgnoreCollision(uCol, ground[1], true);
            Physics2D.IgnoreCollision(uCol, ground[2], true);   //맨아래
            int myGround = Random.Range(0, 3);
            Physics2D.IgnoreCollision(uCol, ground[myGround], false);
            u.transform.position = new Vector3(u.transform.position.x, u.transform.position.y, 2 - myGround);  //맨아래 z좌표 -2면 맨앞에 나옴

            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
