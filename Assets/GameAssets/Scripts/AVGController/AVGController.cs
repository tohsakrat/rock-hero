using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AVGController : MonoBehaviour

{   


    //播放设置
    private float textSpeed = .1f;//几秒跳一个字,会被读取到的数据覆盖掉
    public bool isPause = false;//是否暂停

    //控制播放五变量
    public float timer = 0;//一个dialogue的总计时器
    public float timer1s = 0;//一个1s计时器
    public float frame = 0;
    public string nowPlayingFile;
    public List<json.dialogue> nowPlayingList;
    public int nowPlayingFrame;
    public bool isPlaying;
    public string playState;//预留

    //控制用
    private string textBar;//用来存放未入栈文字
    private string textVisual;//不知道为什么我的unity的文本组件text属性读出来是个object，只能写不能读。那我用一个变量暂存一下文本。
    private float textTimer;//用来给打字机记时
    private bool flagPause ;
    public float playNextTimer=0f;

    //IO接口
    public Text textBox;
    public GameObject video;
    public GameObject dialogueBox;//对话框

    //角色立绘字典
    public static Dictionary<string, Charactor> charactorsDic = new Dictionary<string, Charactor>();


    // Start is called before the first frame update
    void Start()
    {   
        // 演示代码，读取一个文件并播放

        //loadToPlay(nowPlayingFile);

        ///演示代码，自定义一段消息序列并播放
       /* List<json.dialogue> list = new List<json.dialogue>() ;
        list.Add(new json.dialogue(0f, 10f, "", "box","","","fadeIn",true));//第一条演出，给对话框本身也加个渐入
        list.Add(new json.dialogue(0f, 4f, "你妈走进了房间", "system"));//系统消息。这是演示一下省略后四个参数的用法。
        list.Add(new json.dialogue(.5f, 0f, "", "mom","","info","fadeIn",true));//妈妈的立绘进入，不说话
        list.Add(new json.dialogue(0f, 10f, "又在打游戏！", "mom","fight","info","",true));//妈妈说666+打人。fight这个动效还没做，意思一下
        list.Add(new json.dialogue(0.2f, 5f, "下不为例", "mom"));//等待0.2秒，妈妈说一边说下不为例一边离开。这是演示一下省略后四个参数的用法。
        list.Add(new json.dialogue(0f, 2f, "", "mom","","perform","fadeOutRight",true));//等待0.2秒，妈妈说一边说下不为例一边离开
        list.Add(new json.dialogue(0f, 6f, "妈妈甩下这句话，头也不回地离开了", "system","","info","",false));//系统提示,这一条演示不自动播放的情况（鼠标点击屏幕播放下一条）
        list.Add(new json.dialogue(0f, 2f, "", "perform","sanOff","perform","",true));//演出效果，san值降低。san值降低这个动效还没做，意思一下
        list.Add(new json.dialogue(0f, 10f, "你默默地蛄蛹进了被窝", "info","","","",true));
        list.Add(new json.dialogue(.25f, 10f, "...", "","","","",true));//单纯等0.25秒，给人一种留白的感觉
        defToPlay(list);*/
      
    }

    // Update is called once per frame
    void Update()
    {   
        
        textBox.text=textVisual;

        //主计时器，控制大多数动画的播放



        if(isPlaying ){

            timer += Time.deltaTime;
            if(timer>nowPlayingList[nowPlayingFrame].delay){    //等待结束

            if(nowPlayingList[nowPlayingFrame].type == "video"){
                video.SetActive(false);
                goToNextDialogue();
            }//视频播放完毕，在这里关掉。

            //在一句话还没打完时，触摸屏幕，快进。
            if(!isPause&&Input.GetMouseButtonDown(0)){        
                textVisual=textVisual+textBar;
                textBar = "";
            }

            //播放文字类型的消息，实现打字机效果
            textTimer=textTimer-Time.deltaTime;

            if(textTimer<=0){
                textTimer=textSpeed;
                if(textBar.Length>0 ){
                    textVisual=textVisual+textBar[0];//入栈
                    textBar = textBar.Substring(1,textBar.Length - 1);//出栈
                } else {

                    if((nowPlayingList[nowPlayingFrame].autoPlay) && playNextTimer<=0f){
                        playNextTimer=1f;
                        }else if(!nowPlayingList[nowPlayingFrame].autoPlay && !isPause){
                        pausePlaying();
                    }
                } 
            }
            
            }




        }


        //播放视频类型的消息    
        if( nowPlayingList[nowPlayingFrame].type == "video" && isPlaying){
            video.SetActive(true);
        }



        if(playNextTimer>0)playNextTimer = playNextTimer -Time.deltaTime;

        //在两断对话之间的休息，通过触摸屏幕可以提前进入下一句话
        if(playNextTimer>0.01f && playNextTimer<.1f){
                playNextTimer=0f;
                goToNextDialogue();
                
            }else if(playNextTimer>0f){
                if(Input.GetMouseButtonDown(0)){        
               goToNextDialogue();
               }
            }

        //处理非自动播放时的屏幕点击事件
        if(isPause){
            if(Input.GetMouseButtonDown(0)){        
                resumePlaying();
                }
        }

       




    }

///暂停播放（一般用在非自动播放时）
     void pausePlaying()
    {   textVisual=textVisual+"\n";
        isPause=true;
        flagPause=false;
        this.InvokeRepeating("pauseShrink",1,1);
        Debug.Log("pause");
    }


     void resumePlaying()
    {           
        isPause=false;
        CancelInvoke("pauseShrink"); 
        goToNextDialogue();
        Debug.Log("resume");
    }

 ///处理对话暂停时光标闪烁
private void pauseShrink(){
        if(isPause){

                if(!flagPause){
                    textVisual=textVisual+">>>";
                }else{
                    textVisual = textVisual.Substring(0,textVisual.Length - 3);
                    ///Debug.Log("trim");
                }
                
                flagPause=!flagPause;
        }
    
}

////播放控制相关函数

    ///播放一个自定义对话
    public void defToPlay(List<json.dialogue> playList)
    {   
        nowPlayingList = playList;
        nowPlayingFrame = 0;
        isPlaying = true ; 
        nowPlayingFrame=-1;
        goToNextDialogue();
    }


    //读取文件并播放
    public void loadToPlay(string filename)
    {   
        nowPlayingFile=filename;
        nowPlayingList = json.j.load(filename);
        nowPlayingFrame = 0;
        isPlaying = true ; 
        nowPlayingFrame=-1;
        goToNextDialogue();
    }

 ///播放本对话中的下一段
    void goToNextDialogue()
    {  
        dialogueBox.SetActive(true);
        timer=0;
        frame = 0;

        nowPlayingFrame++;
        
        if(nowPlayingFrame>=nowPlayingList.Count){
        stopPlaying();
        return;
        }
        if(nowPlayingList[nowPlayingFrame].speed==0)nowPlayingList[nowPlayingFrame].speed=4;//排除非法数据。
        if(nowPlayingList[nowPlayingFrame].charactor!="system"){
            playAnim(nowPlayingList[nowPlayingFrame]);
        }
        




        textSpeed = 1/nowPlayingList[nowPlayingFrame].speed;
        textTimer = textSpeed;
        textBar = nowPlayingList[nowPlayingFrame].content;
        textVisual = "";

    }

///停止播放
    void stopPlaying()
    {   
        timer=0;
        frame = 0;
        isPlaying = false;
        nowPlayingFile="";
        nowPlayingList = new List<json.dialogue>();
        nowPlayingFrame = 0;
        isPlaying = false ;
        //dialogueBox.SetActive(false);
        

        //所有角色退场
        foreach (var key in charactorsDic.Keys)
        {
        charactorsDic[key].fadeOut();
        }

    }


 

////////角色动画相关函数
///c#通过函数名动态加载函数好像挺麻烦，先用if来写…


    void playAnim(json.dialogue d){
        float nSpeed;



        if(!(nowPlayingList[nowPlayingFrame].speed>0)) {
            nSpeed=4;//避免出现0分母，当遇到非法的speed，手动重置为一秒跳四个字。
        }else{
             nSpeed=nowPlayingList[nowPlayingFrame].speed;
        }

        charactorsDic[nowPlayingList[nowPlayingFrame].charactor].loadFunction(d.anim,nSpeed);



    }



    void fadeOutCharactor(json.dialogue d){

    }



}
