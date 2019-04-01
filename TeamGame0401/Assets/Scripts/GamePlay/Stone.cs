using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float speed=10;
    private bool isAttack;
    private float angle,h,v;
    public Rigidbody2D rig;
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAttack)
        {
            Debug.Log(h + ","+v);
            rig.velocity = new Vector3(h, v, 0)*speed;
        }
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack == false || playerObject == null)
        {
            return;
        }
        else
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject != playerObject)
            {

                collision.gameObject.GetComponent<PlayerMove>().hp -= 50;
                Destroy(gameObject);
            }
        }
        
    }
}