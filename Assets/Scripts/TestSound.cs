﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;

    private void OnTriggerEnter(Collider other){


        i++;
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
        if(i%2 ==0){
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm);

        }else{
             Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Bgm);
        }
    }
}
