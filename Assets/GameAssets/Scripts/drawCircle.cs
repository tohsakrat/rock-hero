using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawCircle : MonoBehaviour
{
    //画圆圈
    Vector2 v;                   //圆心，Vector2是2D,当然也可以换Vector3
    public float R = 6;					//半径
    public int positionCount = 180;			//完成一个圆的总点数，
    public float width = 1;			//完成一个圆的总点数，
    float angle;				//转角，三个点形成的两段线之间的夹角
    Quaternion q;				//Quaternion四元数
    LineRenderer line;			//LineRenderer组件
    
    void Start()
    {
        
        v = new Vector2(0, 0);
        angle = 360f / (positionCount - 1);
        line = GetComponent<LineRenderer>();
        line.positionCount = positionCount;

    }
    void Update()
    {
        //设置线宽
        line.widthCurve = AnimationCurve.Constant(0,1,1);
        line.widthMultiplier = width;
        //画圆
        DrawCircle();
    }
    void DrawCircle()
    {
        //画圆
        for (int i = 0; i < positionCount; i++)
        {
            if (i != 0)
            {
                q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + angle);
            }
            Vector3 forwardPosition = (Vector3)v + q * Vector3.down * R;
            line.SetPosition(i, forwardPosition);
        }
    }

}
