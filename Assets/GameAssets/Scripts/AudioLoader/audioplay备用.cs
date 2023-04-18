using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioplay : MonoBehaviour
{
    public AudioSource boring;
    public AudioSource falldown;

    /// <summary>≤•∑≈∑≈“Ù¿÷</summary>
    /// 
    private void Update()
    {
        playMusic();
    }
    private void playMusic()
    {

        if (GameCtrl.instance.Pressure > 50)
        {
                boring.Stop();
               
        }
        else
        {
            boring.Play();
        }


      
    }




    }



