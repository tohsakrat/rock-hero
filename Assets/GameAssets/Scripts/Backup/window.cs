using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class window : MonoBehaviour
{
    public Animator ani;

    public Text mamtxt;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        InvokeRepeating("rdmmom", 5, 15);//ÿ10�����������������

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void rdmmom()//���һ��ʱ���ʼcoming
    {
        rdm.Randomtimeer(8,14);
        Invoke("momcoming", rdm.timeee);
    }
    void momcoming()
    { 
        ani.SetTrigger("tig");
        rdm.Randomtimeer(5, 10);
        Debug.Log("mom is coming"+rdm.timeee);
        Invoke("momcame", rdm.timeee);

    }
    void momcame()
    {
        mamtxt.gameObject.SetActive(true);

        Debug.Log("mom!!!" );
        Invoke("momgo", 1f);

    }
    void momgo()
    {
        mamtxt.gameObject.SetActive(false);

        Debug.Log("mom go");

    }
    
}
