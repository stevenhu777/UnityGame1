using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float speed=10;　　　　//移動スピード
    private bool isAttack;　　　　//攻撃中かどうか
    private float h,v;　　　　　　//移動方向（Ｘ軸、Ｙ軸）
    public Rigidbody2D rig;
    private GameObject playerObject;
    //private Collider2D col;
   // private 
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        if (isAttack)
        {            

           // col.isTrigger = false;
            //transform.position = transform.position + new Vector3(h, v, 0) * Time.deltaTime * 10;
            rig.velocity = new Vector3(h, v, 0)*Time.deltaTime*300;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 360 * Time.deltaTime));
        }
        //else if(col.isTrigger==false)
        //{
        //    col.isTrigger = true;
        //}

    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void SetPlayerObject(GameObject playerObject)
    {
        this.playerObject = playerObject;
    }
    public void Attack(float angle)
    {
        isAttack = true;
        float rad = angle * Mathf.Deg2Rad;
        v = Mathf.Sin(rad);
        h = Mathf.Cos(rad);
       // rig.velocity = new Vector3(h, v, 0) * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack == false || playerObject == null)
        {
            return;
        }
        else
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                    if (collision.gameObject != playerObject)
                    {
                        collision.gameObject.GetComponent<PlayerMove>().hp -= 50;
                        Destroy(gameObject);
                    }
                    break;
                case "Block":
                    Debug.Log("Trigger");
                    isAttack = false;
                    break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttack == false)
        {
            return;
        }
        if (collision.gameObject.tag == "Block")
        {
            Debug.Log("Collision");

            isAttack = false;

        }
    }
}