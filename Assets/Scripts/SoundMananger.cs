using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMananger : MonoBehaviour
{
    public static SoundMananger instance {  get; private set; }
    private AudioSource source;

    private void Awake()
    {
        
        source = GetComponent<AudioSource>();

        if(instance ==  null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);
    }
}
