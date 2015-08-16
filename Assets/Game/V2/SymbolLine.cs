 using UnityEngine;
using System.Collections;

public class SymbolLine : MonoBehaviour {

    public Transform[] m_symbols;

    public Transform[] m_symbols_stop;

    public float m_dis;

    public float m_dis_interval;

    bool m_move;
    bool m_break;

    public VOIDCB OnSymbolLineStopCB;


    void Awake()
    {
        string name = "TileObject_1_";
        m_symbols = new Transform[8];
        m_symbols_stop = new Transform[8];

        for (int i = 0; i < m_symbols.Length; i++)
            m_symbols[i] = GameObject.Find(name + (i+1).ToString()).transform;

        m_dis_interval = 190;
    }

	// Use this for initialization
	void Start () {
        m_move = false;
        m_break = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (m_move)
        {
            if (m_break)
                Move_OnBreak();
            else
                Move();
        }
	}
    
    public void StartRun()
    {
        m_move = true;
        m_break = false;
    }
    public void StartStop()
    {
        for (int i = 0; i < m_symbols.Length; i++)
            m_symbols_stop[i] = m_symbols[i];

        m_break = true;

    }

    public void SetMove(float dis)
    {
        m_dis = dis;
    }

    void Move()
    {
        //float m_dis = -1 * m_speed * Time.deltaTime;

        for (int i = 0; i < m_symbols.Length; i++)
            m_symbols[i].localPosition = new Vector3(0,m_symbols[i].localPosition.y + m_dis,0);
            //m_symbols[i].Translate(new Vector2(0, m_dis), Space.Self);

        if (m_symbols[0].localPosition.y < -200)
        {
            TurnAround();
            float y = m_symbols[m_symbols.Length - 2].localPosition.y + 190;
            m_symbols[m_symbols.Length - 1].localPosition = new Vector3(0, y, 0);
        }
    }

    void Move_OnBreak()
    {
        float temp = m_symbols_stop[0].localPosition.y + m_dis;

        if (temp < 0 && m_symbols_stop[0].localPosition.y > 0)
        {
            MoveToStop();

            m_move = false;
            m_break = false;

            if (OnSymbolLineStopCB != null)
                OnSymbolLineStopCB();
        }
        else
        {
            print(m_symbols_stop[0].localPosition.y + " " + temp);
            Move();
        }
    }

    void MoveToStop()
    {
        m_symbols_stop[0].localPosition = Vector3.zero;
        m_symbols_stop[1].localPosition = new Vector3(0, 190, 0);
        m_symbols_stop[2].localPosition = new Vector3(0, 380, 0);
        m_symbols_stop[3].localPosition = new Vector3(0, 570, 0);
        m_symbols_stop[4].localPosition = new Vector3(0, 760, 0);
        m_symbols_stop[5].localPosition = new Vector3(0, 950, 0);
        m_symbols_stop[6].localPosition = new Vector3(0, 1140, 0);
        m_symbols_stop[7].localPosition = new Vector3(0, 1330, 0);
        
        TurnAround();

    }

    // 換掉第一個
    void TurnAround()
    {
        for (int i = 1; i < m_symbols.Length; i++)
        {
            Transform temp = m_symbols[i - 1];
            m_symbols[i - 1] = m_symbols[i];
            m_symbols[i] = temp;
        }
    }
}
