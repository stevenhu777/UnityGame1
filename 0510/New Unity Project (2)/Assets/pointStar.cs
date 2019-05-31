using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointStar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

       // GetPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5);
        //GetPoint();
    }
  
}
