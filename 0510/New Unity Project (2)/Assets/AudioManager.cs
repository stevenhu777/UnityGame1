using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = bgm;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
