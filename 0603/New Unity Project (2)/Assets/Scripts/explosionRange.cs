using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionRange : MonoBehaviour
{

    //// Start is called before the first frame update
    //void Start()
    //{


    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //print(transform.parent.name);
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (GetComponentInParent<Item>().state == Item.State.OnBomb)
    //    {
    //        if (collision.tag =="Player")
    //        {
    //            collision.gameObject.GetComponent<PlayerController>().Hp -= transform.GetComponentInParent<Firebomb>().Damage;
    //        }
    //        Destroy(transform.parent.gameObject);
    //    }
    //}
    public bool isAction;
    private bool hasAttackPlayer;
    private  float damage=25;
    public float damageCofficient;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (isAction == false)
        {
            return;
        }
        if (col.tag == "Player" && hasAttackPlayer == false)
        {
            col.gameObject.GetComponent<PlayerController>().Hp -= damage*damageCofficient;
            hasAttackPlayer = true;
        }
        if (col.tag == "Wall")
        {
            Destroy(col.gameObject);
        }
    }
}
