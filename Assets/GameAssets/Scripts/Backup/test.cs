using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed;
    public bool ismove = false;

    bool isplayer = false;
    public float handsDistance;//射线的长度(手的交互范围)
    public float rayPositionX;//射线的X轴
    public float rayPositionY;//射线的X轴
    public LayerMask ObjectLayer;
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
       ismove=playerMove.instance.isTouch;
        if(isplayer&&Input.GetButton("Jump"))
        {
            objectmove();
        }

        RayCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "Book"&& collision.gameObject.name== "bag")
        {
            this.gameObject.SetActive(false);
            GameCtrl.instance.RoomClear+=15 ;
            Boxscprit.instance.randmoyu();
        }

        if (this.gameObject.tag == "rubbish" && collision.gameObject.name == "TrashCan")
        {
            this.gameObject.SetActive(false);
            GameCtrl.instance.RoomClear += 5;
            Boxscprit.instance.randmoyu();
        }

    }

   private void objectmove()
    {
        Vector2 dir = Vector2.zero;//玩家方向
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;

        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            
        }
        GetComponent<Rigidbody2D>().velocity = speed * dir;
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
        RaycastHit2D RTouchRay = Raycast(new Vector2(rayPositionX, -0.1f), Vector2.right, handsDistance, ObjectLayer);
        RaycastHit2D LTouchRay = Raycast(new Vector2(rayPositionY, -0.1f), Vector2.left, handsDistance, ObjectLayer);

        if (RTouchRay|| LTouchRay)
            isplayer = true;
        else
            isplayer = false;
    }

}
