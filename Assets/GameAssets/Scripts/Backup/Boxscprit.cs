using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxscprit : MonoBehaviour
{
   public int minrang;
   public int maxrang;

    public static bool ismoyu = false;
    public static Boxscprit instance;

    public GameObject gantan;

    public bool isplayer = false;
    public float handsDistance;//���ߵĳ���(�ֵĽ�����Χ)
    public float rayPositionX;//���ߵ�X��
    public LayerMask ObjectLayer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        randmoyu();
    }

    // Update is called once per frame
    void Update()
    {
         RayCheck();
    }
    public  void moyuxiyin()
    {
        ismoyu = true;
        Debug.Log("moyu" + rdm.timeee);
        gantan.gameObject.SetActive(true);
       
    }
    public void randmoyu()
    {
        rdm.Randomtimeer(minrang, maxrang);
        Invoke("moyuxiyin", rdm.timeee);
    }


    /*************************
    ���ߺ���������ֵΪһ������
    ������λ�á����򡢳��̡�Ŀ����ײ��.
   *************************/
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;//��Ԫ���������hitΪtrue�򷵻�red

        Debug.DrawRay(pos + offset, rayDirection * length, color);
        return hit;
    }
    /*************************
    ��������Ƿ���ײ����
   *************************/
    public void RayCheck()
    {
        RaycastHit2D RTouchRay = Raycast(new Vector2(rayPositionX, 0f), Vector2.left, handsDistance, ObjectLayer);


        if (RTouchRay)
            isplayer = true;
        else
            isplayer = false;
    }



}
