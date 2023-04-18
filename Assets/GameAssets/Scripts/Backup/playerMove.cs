using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMove : MonoBehaviour
{
    public float speed;
    public static playerMove instance;//实例化
    private Animator animator;
    public Vector2 dir;
    public  GameObject avgControllerObject;
    public  AVGController avgController;
    /******判断玩家是否接触物体******/
    public bool isTouch = false;//判断玩家是否接触物体
    public float handsDistance = 0.4f;//射线的长度(手的交互范围)
    public float rayPositionX = -0.3f;//射线的X轴
    public float rayPositionY = 0.3f;//射线的X轴
    public LayerMask ObjectLayer;

    /***********判断玩家状态*******
     * 优化建议 用数组判断状态******/
    
    public bool state_study = false;
    public bool state_walk = false;
    public bool state_fun = false;
    public bool state_JK = false;

    private void Start()
    {
        avgControllerObject.SetActive(true);

      animator = GetComponent<Animator>();
        instance = this;
        state_walk = true;
    }
    private void Update()
    {
     
    }
    private void FixedUpdate()
    {
        PlayerMove();
        RayCheck();
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
        RaycastHit2D leftTouchRay = Raycast(new Vector2(rayPositionX, -0.5f), Vector2.left, handsDistance, ObjectLayer);
        RaycastHit2D rightTouchRay = Raycast(new Vector2(rayPositionY, -0.5f), Vector2.right, handsDistance, ObjectLayer);


        if (leftTouchRay || rightTouchRay)
            isTouch = true;
        else
            isTouch = false;
    }

    void PlayerMove()
    {
        float horizontalFaceDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalFaceDirection != 0)   // 若按下了左右，使得朝向更新
        {
            this.transform.localScale = new Vector3(-horizontalFaceDirection, 1, 1);    // 更新当前物体的Scale.x的值，yz值不变
        }
        Vector2 dir = Vector2.zero;//玩家方向

        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetBool("isrun", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
           animator.SetBool("isrun", true);
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
           animator.SetBool("isrun", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
            animator.SetBool("isrun", true);
        }

        if(!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            animator.SetBool("isrun", false);
        }
       
            

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}

