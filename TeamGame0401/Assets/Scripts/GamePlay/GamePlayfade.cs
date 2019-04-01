using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayfade : MonoBehaviour
{
    Image image;
    float fadetime = 2;
    float fadetriggertime = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        image.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            if (fadetriggertime >= fadetime)
            {
                return;
            }
            fadetriggertime += Time.deltaTime;
            image.color = new Color(0, 0, 0, 1 - fadetriggertime / fadetime);

        }
        print(gameObject.name + ":" + gameObject.activeInHierarchy);
    }
}
