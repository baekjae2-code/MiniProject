using System.Linq;
using UnityEngine;

public class TeamHealUnit : Unit
{
    public GameObject healObj;
    float healCooltime;
    float healSpeed;
    void Start()
    {
        moveSpeed = 2f;
        rb = GetComponent<Rigidbody2D>();
        healObj = transform.Find("HealMagicEffect").gameObject;
        healSpeed = 2f;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        healCooltime += Time.deltaTime;

        CheckEnemy();
        if (target != null && healCooltime > healSpeed)
        {
            Heal();
        }
        if (canMove)
        {
            Move();
        }
    }
    void CheckEnemy()
    {
        target = null;
        int layer = LayerMask.NameToLayer("Enemy");
        int targetLayer = 1 << layer;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 2, targetLayer);
        if (collider.Length == 0)
        {
            canMove = true;
        }
        else
        {
            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;

            canMove = false;
        }
    }
    void Heal()
    {
        GameObject obj = Instantiate(healObj, transform.position, healObj.transform.rotation);
        obj.SetActive(true);
        healCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
