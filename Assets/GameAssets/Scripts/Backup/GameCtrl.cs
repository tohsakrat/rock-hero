using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour
{
    public Text Time_txt, Clear_txt, Pressure_txt;
    public Text MOm_temper_txt;

    public static GameCtrl instance;

    public float nowTime;//����ʱ��
    public int resTime;//ʣ��ʱ��
    public float timea;
    public int RoomClear=100;//���������
    public int Pressure=30;//ѹ��ֵ
    public int MOmTemper = 50;//��������

    public bool audibool = false;

    AudioSource bgmAudio;
    public AudioClip[] audios;

    public float randompos;

    public GameObject bookPF;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        bgmAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Boxscprit.instance.isplayer && Input.GetButtonDown("Jump")&&Boxscprit.ismoyu)
        {
            playerMove.instance.speed = 0;
            Invoke("Rand_appear",5);//5�����
           Boxscprit.instance.gantan.gameObject.SetActive(false);
            playerMove.instance.state_walk = false;
            playerMove.instance.state_fun = true;

            Boxscprit.ismoyu = false;
        }
        nowTime +=Time.deltaTime;
        timea += Time.deltaTime;
        resTime = 120 - (int)timea;
        Time_txt.text ="" + resTime;
        Clear_txt.text = "��������ȣ�" + RoomClear;

        MOmTemper =50- 100+ RoomClear +(int)studystate.homework;
        Pressurechange();
       
        MOm_temper_txt.text = "�������飺" + MOmTemper;
    }

    /******��Ʒ������ֺ���
     * ������ɽ�������ҷ���*******/
    void Rand_appear()
    {
        float posx = transform.position.x + Random.Range( - 5f,  + 3f) ;
        float posy = transform.position.y + Random.Range( - 2f,  + 0.75f) ;
        Vector2 randomNewpos = new Vector2(posx, posy);
      
        Instantiate(bookPF, randomNewpos, Quaternion.identity);//Quaternion.identity ��Ԫ����ת ����ת
        bookPF.SetActive(true);

        playerMove.instance.speed = 3;
        playerMove.instance.state_walk = true;
        playerMove.instance.state_fun = false;


        RoomClear = RoomClear - 15;//��һ������ ���������-15; 
        Pressure -= 5;//��һ������-5ѹ��
       
    }

  
    void Pressurechange()
    {
        if(  playerMove.instance.state_study)
        {
           if((int)nowTime  > 5)
            {
                Pressure += 5;
                Debug.Log("ѧϰ����ѹ���Ӣ�");
                nowTime = 0;
            }
           
        }
        if (playerMove.instance.state_walk)/********bug������������·������ʱѧϰ ��ѧϰ��*********/
        {
            if ((int)nowTime > 5)
            {
                Pressure += 3;
                Debug.Log("��·����ѹ����3");
                nowTime = 0;
            }

        }

        if(Pressure >= 50 && !audibool)
        {
            bgmAudio.clip = audios[0];
            bgmAudio.Play();
            audibool = true;
        }
        else if(Pressure < 50 && audibool)
        {
            bgmAudio.clip = audios[1];
            bgmAudio.Play();
            audibool = false;
        }
        

        if (Pressure>=100)
        {
            Pressure_txt.text = "���ˣ�" ;
        }
        else
        
        Pressure_txt.text = "ѹ��ֵ��" + Pressure;
    }
   

}
