using UnityEngine;

public class HealEffect : MonoBehaviour
{
    public GameObject healEffect;
    int heal;
    void Start()
    {
        heal = 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Unit>().TakeHeal(heal);

            GameObject u = ObjectPoolManager.instance.GetObject(healEffect.name);
            u.transform.position = transform.position;
            ObjectPoolManager.instance.ReturnObject(healEffect.name, u, 1);
        }
    }
}
