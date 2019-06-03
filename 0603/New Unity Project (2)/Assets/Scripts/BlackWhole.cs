using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWhole : MonoBehaviour
{
    CircleCollider2D colliders;
     private float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponent<CircleCollider2D>();
        float r = Random.Range(5,8);
        transform.localScale = new Vector3(r, r, r);
        RotateSpeed = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, RotateSpeed, Space.World);
        Destroy(gameObject, Random.Range(3, 5));
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 vel = transform.position - collision.transform.position;
            vel = Vector3.Normalize(vel);
            collision.transform.position += vel * Time.deltaTime * 8;
        }
    }
}
