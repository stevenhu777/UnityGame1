using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;
using static PlayerController;

public class Stone : MonoBehaviour
{
    public float Damage;
    
    float Speed;
    public float damagexishu = 1;
    Vector2 attackVelocity;
    
    private void Start()
    {
        Speed = 40;
        Damage = 20;
    }
    // Start is called before the first frame update
    void Update()
    {
        attackVelocity = GetComponent<Item>().attackVelocity;
        if (GetComponent<Item>().state == State.OnAttack)
        {
            Attack();
        }  
    }
    
    public void Attack()
    {
        transform.position += new Vector3(attackVelocity.x * Speed * Time.deltaTime, attackVelocity.y * Speed * Time.deltaTime, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Item>().state == State.OnAttack)
        {
            if (collision.gameObject.tag == "Wall")
            {
                GetComponent<Item>().state = State.OnGround;
                gameObject.GetComponent<Item>().team = PlayerTeam.Null;

            }
            if (collision.gameObject.tag == "Player"&&collision.gameObject.GetComponent<PlayerController>().state == PlayerState.Normal)
            {
                collision.GetComponent<PlayerController>().Hp -= Damage;
                
                Destroy(gameObject);

            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    

    private void OnBecameInvisible()
    {
    }
}
