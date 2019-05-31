using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class Item : MonoBehaviour
{
    public State state = State.OnGround;
    public ItemType type;
    
    public PlayerTeam team = PlayerTeam.Null;
    

    public Vector2 attackVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        OutOfScreen();
    }
    
    public void AttackVelocity(Vector2 vel)
    {
        attackVelocity = vel;
        attackVelocity = Vector3.Normalize(attackVelocity);
        state = State.OnAttack;
    }
    void OutOfScreen()
    {
        if (transform.position.x < -35 ||
            transform.position.x > 35 ||
            transform.position.y < -20 ||
            transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    

    private void OnBecameInvisible()
    {
    }


    public enum ItemType
    {
        Stone, Firebomb, Icebomb,
    }

    public enum State
    {
        OnGround, OnPlayer, OnAttack,OnBomb
    }


}
