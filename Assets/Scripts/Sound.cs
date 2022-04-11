using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sound : MonoBehaviour
{
    public static Sound Instance;
    public AudioClip[] clip;
    public AudioSource[] source;
    //public AudioClip[] music_clip;
    //public float sd, ms;
    //int clip_id;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //if (!GetComponent<AudioSource>().isPlaying)
        //{            
        //    clip_id = GetComponent<AudioSource>().clip == music_clip[2] ? 0 : clip_id + 1;
        //    GetComponent<AudioSource>().clip = music_clip[clip_id];
        //    GetComponent<AudioSource>().Play();
        //}
    }
    //public void Set_voll(int id, float count)
    //{
    //    if (id == 0)
    //    {
    //        transform.GetChild(0).gameObject.GetComponent<AudioSource>().volume = count;
    //        transform.GetChild(1).gameObject.GetComponent<AudioSource>().volume = count;
    //    }
    //    else
    //    {
    //        GetComponent<AudioSource>().volume = count;
    //    }
    //}
    public void Play_Sound(string name)
    {

        for(int i = 0; i < clip.Length; i++)
        {
            if (clip[i].name == name)
            {
                if (name != "host")
                    source[0].PlayOneShot(clip[i]);             
                else
                {
                    source[1].PlayOneShot(clip[i]);
                    source[1].pitch += .1f;
                }
               
            }               
        }        
    } 
    public void PlayUpgrade()
    {
        source[2].PlayOneShot(clip[13]);
        source[2].pitch += .1f;
    }
    public void Click()
    {
        transform.GetChild(1).GetComponent<AudioSource>().Play();
    }
}