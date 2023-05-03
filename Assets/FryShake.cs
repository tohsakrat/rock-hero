using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryShake : MonoBehaviour
{   
    public UniGifImage unigif;
    public Vector3 anchor;
    void Start()
    {
        anchor=transform.localPosition;
    }
    void Update()
    {
        //如果index是奇数
        if(unigif.m_gifTextureIndex%2==1)
            transform.localPosition = anchor + Vector3.down*0.3f;
        else 
            transform.localPosition = anchor;

    }
}
