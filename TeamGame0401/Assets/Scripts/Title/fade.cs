using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    float fadetime=2;
    float fadetriggertime=0;
    public bool isactive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isactive)
        {
            fadetriggertime += Time.deltaTime;
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (1 - fadetriggertime / fadetime));
    }
}
