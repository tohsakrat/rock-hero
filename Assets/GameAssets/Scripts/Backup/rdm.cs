using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rdm : MonoBehaviour
{
    // public static rdm instance;//ʵ����
    static public int timeee;
    // Start is called before the first frame update
    void Start()
    {
    //    instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static  public  void Randomtimeer(int minrang,int maxrang)//���ʱ�亯��������Ϊ��С��Χ�����Χ����ʼ����ʱ��
    {
        timeee= Random.Range(minrang, maxrang);
    }

}
