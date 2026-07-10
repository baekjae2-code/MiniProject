using UnityEngine;

public class HealEffect : MonoBehaviour
{
    public GameObject healEffect;
    int heal;
    void Start()
    {
        heal = 2;
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Unit>().TakeHeal(heal);
            Instantiate(healEffect, collision.transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
