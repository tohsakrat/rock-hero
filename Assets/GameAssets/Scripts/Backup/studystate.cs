using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class studystate : MonoBehaviour
{
    public bool isstudy = false;
    bool isTh = false;
    public GameObject playerobject;
    public GameObject deskobject;

    public float handsDistance ;//射线的长度(手的交互范围)
    public float rayPositionX ;//射线的X轴
    public LayerMask ObjectLayer;

   static public float homework = 0;

 bool isrubbish = true;
    public GameObject[] Rubbishprefabs;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       RayCheck();

       
       

        if (isTh && Input.GetKey(KeyCode.Z))//进入学习状态
        {
            deskobject.gameObject.SetActive(true);
            playerobject.gameObject.SetActive(false);
            isstudy = true;

            playerMove.instance.state_walk = false;
            playerMove.instance.state_study = true;

         

            InvokeRepeating("SpawnRandomRubbish", 1.5f, 4f);
        }
        if (isstudy && Input.GetKey(KeyCode.X))//退出学习状态
        {
            playerobject.gameObject.SetActive(true);
            deskobject.gameObject.SetActive(false);
            isstudy = false;

            playerMove.instance.state_walk = true;
            playerMove.instance.state_study = false;

            CancelInvoke("SpawnRandomRubbish");

        }
        if(isstudy)
        {
            homework += Time.deltaTime;
          //  Debug.Log((int)homework);
        }

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
        RaycastHit2D RTouchRay = Raycast(new Vector2(rayPositionX, -1f), Vector2.left, handsDistance, ObjectLayer);
       

        if (RTouchRay)
            isTh = true;
        else
            isTh = false;
    }

    void SpawnRandomRubbish()//随机生成垃圾

    {
        Vector2 spawnPos = new Vector2( Random.Range(-5f, +3f), Random.Range(-2f, +0.75f));
        //在x轴的范围内随机生成

        GameCtrl.instance.RoomClear = GameCtrl.instance.RoomClear - 5;//扔草稿纸 房间整洁度-5; 

        int rubbishIndex = Random.Range(0, 2);  
        Instantiate(Rubbishprefabs[rubbishIndex], spawnPos,
        Rubbishprefabs[rubbishIndex].transform.rotation);


        isrubbish  =  !isrubbish;

    }

}
