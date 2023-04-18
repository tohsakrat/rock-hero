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
    public float handsDistance;//射线的长度(手的交互范围)
    public float rayPositionX;//射线的X轴
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
    射线函数，返回值为一条射线
    参数：位置、方向、长短、目标碰撞层.
   *************************/
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;//三元运算符，若hit为true则返回red

        Debug.DrawRay(pos + offset, rayDirection * length, color);
        return hit;
    }
    /*************************
    检查射线是否碰撞物体
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
