using UnityEngine;
using System;
using System.Collections;

public class Slotmachine : MonoBehaviour {

    public SymbolLine[] m_symbolLine;

    public float m_symbolspeed;

    bool m_run;

    bool m_waitCB;

    int m_cnt_stop;

    string[] spritenames = { "Apple", "Bell", "BlueS", "Orange", "strawberry", "Vine" };
    
    void Awake()
    {
        for (int i = 0; i < m_symbolLine.Length; i++)
        {
            m_symbolLine[i].OnSymbolLineStopCB = OnSymbolStop;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
        if(m_run)
        {
            float dis = -1 * m_symbolspeed * Time.deltaTime;
            
            for (int i = 0; i < m_symbolLine.Length; i++ )
                m_symbolLine[i].SetMove(dis);
        }
	}
    
    void OnGUI()
    {
        if (GUILayout.Button("Tween"))
        {
            StartRun();
        }
        if (GUILayout.Button("Tween"))
        {
            StartStop();
        }
    }
    
    void StartRun()
    {
        m_run = true;
        
        for (int i = 0; i < 5; i++)
        {
            m_symbolLine[i].StartRun();
        }

        //StartCoroutine(DoStartRun());
    }

    void StartStop()
    {
        m_cnt_stop = 0;


        OnSymbolStop();
        //StartCoroutine(DoStopMachine());
    }

    IEnumerator DoStartRun()
    {

        for (int i = 0; i < 5; i++)
        {
            m_symbolLine[i].StartRun();
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator DoStopMachine()
    {
        for (int i = 0; i < m_symbolLine.Length; i++)
        {
            m_symbolLine[i].StartStop();

            m_waitCB = true;
            while(m_waitCB)
                yield return new WaitForEndOfFrame();
        }
        m_run = false;
    }
    void OnSymbolStop()
    {
        if (m_cnt_stop < m_symbolLine.Length)
        {
            string[] strs = new string[3];

            for (int i = 0; i < 3; i++)
            {
                int rand = UnityEngine.Random.Range(0, 6);
                strs[i] = spritenames[rand];
            }

            //print(m_cnt_stop + " " + strs[0] + " " + strs[1] + " " + strs[2]);

            m_symbolLine[m_cnt_stop].SetSprite(strs);    
            m_symbolLine[m_cnt_stop++].StartStop();
        }
    }

}
