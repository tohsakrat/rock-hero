using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public bool isinterdown,isinterup;
    
    public GameObject pare_player;//������
    public GameObject son_myself;//�Ӷ���


    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       //if(playerMove.instance.isTouch)
       // {
           
       // }
       // Hold_pull();
    }
   
    //void Hold_pull()
    //{
    //    isinterdown = Input.GetButtonDown("Jump");
    //    isinterup = Input.GetButtonUp("Jump");

    //    if (isinterdown)
    //    {

    //        GameObject a = Instantiate(son_myself, transform.position, Quaternion.identity);//ʵ��������
    //        a.transform.parent = pare_player.transform;//��ʵ����������ŵ�������player֮��
    //    }
    //    if (isinterup)
    //    {
    //        //������Ӹ�����player֮���ó���
    //    }
    //}
}
