using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            title.GetComponent<fade>().isactive = true;
        }
        if (title.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}
