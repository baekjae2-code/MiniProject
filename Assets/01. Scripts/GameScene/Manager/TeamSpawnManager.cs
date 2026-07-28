using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TeamSpawnManager : MonoBehaviour
{
    public static TeamSpawnManager instance;

    public Collider2D[] ground;
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
            //GameObject u = Instantiate(GameManager.instance.unitsPrefab[spawnUnitNum]);
            GameObject u = ObjectPoolManager.instance.GetObject(GameManager.instance.unitsPrefab[spawnUnitNum].name);
            u.layer = 7;
            u.GetComponent<Unit>().enabled = true;
            u.transform.position = transform.position;
            u.SetActive(true);
            u.GetComponent<Rigidbody2D>().linearVelocity += new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            UIManager.instance.PrintUnitHPbar(u);

            Physics2D.IgnoreCollision(u.GetComponent<Collider2D>(), ground[0], true);   //맨위
            Physics2D.IgnoreCollision(u.GetComponent<Collider2D>(), ground[1], true);
            Physics2D.IgnoreCollision(u.GetComponent<Collider2D>(), ground[2], true);   //맨아래
            int myGround = Random.Range(0, 3);
            Physics2D.IgnoreCollision(u.GetComponent<Collider2D>(), ground[myGround], false);
            u.transform.position = new Vector3(u.transform.position.x, u.transform.position.y, 2-myGround);  //맨아래 z좌표 -2면 맨앞에 나옴

            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
