using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Charactor : MonoBehaviour
{   
    public Vector3 target;

    public float speed = 1000f ;//本角色演出时滑入滑出速度基数。最终速度与json中乘算。
    private float nowSpeed = 0.25f;//本句台词的速度。
    private float s;

    public string charactorName;//这个角色的名字，如果不填那就默认填写当前gameObject对象名字
    private RawImage rawImage;
    private RectTransform rectTransform;
    private Vector4 targetColor;
    private Vector3 originalPos;

    void Awake(){
        if(charactorName.Length==0)charactorName=this.gameObject.name;
        AVGController.charactorsDic.Add(charactorName, this) ;
        rawImage = this.GetComponent<RawImage>();
        rectTransform=this.GetComponent<RectTransform >();
        target = rectTransform.localPosition;
        targetColor = convertColorToVector4(rawImage.color);
        this.gameObject.SetActive(false);//开场先隐藏自己
    }

    // Start is called before the first frame update
    void Start()
    {

       // fadeInRight();
    }


    // Update is called once per frame


    void Update()
    {   
    /*
      Debug.Log("--");
      Debug.Log(speed);
      Debug.Log((Time.deltaTime));
      Debug.Log(rectTransform.localPosition);
      Debug.Log( Vector3.MoveTowards(rectTransform.localPosition, target, speed * Time.deltaTime));
      Debug.Log("--");
    */
       
        rawImage.color = convertVector4ToColor(Vector4.MoveTowards(convertColorToVector4(rawImage.color), targetColor , nowSpeed* Time.deltaTime/300));
        rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, target, nowSpeed * Time.deltaTime);

        if( rawImage.color == new Color(0f,0f,0f,0f) ){
            rectTransform.localPosition = originalPos;
            this.gameObject.SetActive(false);}//当角色完成渐隐后，复位。然后失活
        }





///动画相关函数
   public  void fadeIn(){
        nowSpeed=s*speed;
        this.gameObject.SetActive(true);
        rawImage.color = new Color(0f,0f,0f,0f);
        targetColor =  new Vector4(1,1,1,1);
    }

    public void fadeOut(){
        nowSpeed=s*speed;
        //Debug.Log("FadeOut");
         targetColor =  new Vector4(0,0,0,0);
    }


   public  void fadeInRight(){
        nowSpeed=s*speed;
        this.gameObject.SetActive(true);
        rawImage.color = new Color(0f,0f,0f,0f);
         rectTransform.localPosition = rectTransform.localPosition + (new Vector3(400,0,0));
        //Debug.Log("FadeIn");
        target = new Vector3( rectTransform.localPosition.x-400, rectTransform.localPosition.y,rectTransform.localPosition.z);
        targetColor =  new Vector4(1,1,1,1);
    }

    public void fadeOutRight(){
        nowSpeed=s*speed;
        //Debug.Log("FadeOut");
        target = new Vector3( rectTransform.localPosition.x+400, rectTransform.localPosition.y,rectTransform.localPosition.z);
        //target = new Vector3(0,0,0);
         originalPos = rectTransform.localPosition;
         targetColor =  new Vector4(0,0,0,0);
    }

    public void fadeInLeft(){
        nowSpeed=s*speed;
        this.gameObject.SetActive(true);
        rawImage.color = new Color(0f,0f,0f,0f);
       rectTransform.localPosition = rectTransform.localPosition - (new Vector3(400,0,0));
        //Debug.Log("FadeIn");
        target = new Vector3( rectTransform.localPosition.x+400, rectTransform.localPosition.y,rectTransform.localPosition.z);
        targetColor =  new Vector4(1,1,1,1);
    }

    public void fadeOutLeft(){
        nowSpeed=s*speed;
        //Debug.Log("FadeOut");
        target = new Vector3( rectTransform.localPosition.x-400, rectTransform.localPosition.y,rectTransform.localPosition.z);
        //target = new Vector3(0,0,0);
        originalPos = rectTransform.localPosition;
         targetColor =  new Vector4(0,0,0,0);
    }
    
    //通过字符串读取对应动画函数

    public void loadFunction(string animName, float inSpeed)

    {
    s=inSpeed;
    this.Invoke(animName,0f);//
    }


    Vector4 convertColorToVector4(Color c){
        Vector4 color4 = new Vector4(c.r,c.g,c.b,c.a);
        return color4;
    }

    Color convertVector4ToColor(Vector4 c){
        Vector4 color = new Color(c.x,c.y,c.z,c.w);
        return color;
    }
}
