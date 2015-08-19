 using UnityEngine;
using System.Collections;

public class SymbolLine : MonoBehaviour {

    public string symbol_prefix;

    public Transform[] m_symbols;

    public Transform[] m_symbols_stop;

    public float m_dis;

    public float m_dis_interval;

    bool m_move;
    bool m_break;

    bool m_state_moveup;

    public VOIDCB OnSymbolLineStopCB;


    TweenPosition m_tweener;

    string[] Names;

    void Awake()
    {
        //string name = "TileObject_1_";
        
        m_symbols = new Transform[8];
        m_symbols_stop = new Transform[8];

        for (int i = 0; i < m_symbols.Length; i++)
            m_symbols[i] = GameObject.Find(symbol_prefix + (i + 1).ToString()).transform;

        m_dis_interval = 190;

        m_tweener = GameObject.Find(symbol_prefix).GetComponent<TweenPosition>();
    }

	// Use this for initialization
	void Start () {
        m_move = false;
        m_break = false;
        m_tweener.AddOnFinished(Listener);
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
    
    void Listener()
    {
        if(m_state_moveup)
        {

            m_move = true;
            m_break = false;
            m_state_moveup = false;

        }
        else
        {
            /*
            if (OnSymbolLineStopCB != null)
                OnSymbolLineStopCB();*/
        }
    }
    public void StartRun()
    {

        m_break = false;

        m_state_moveup = true;
        m_tweener.from = m_tweener.gameObject.transform.localPosition;
        m_tweener.to = new Vector3(transform.localPosition.x, -100, 0);
        m_tweener.ResetToBeginning();
        m_tweener.PlayForward();
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

    public void SetSprite(string[] names)
    {
        Names = names;
    }
    void Move()
    {

        for (int i = 0; i < m_symbols.Length; i++)
            m_symbols[i].localPosition = new Vector3(0,m_symbols[i].localPosition.y + m_dis,0);

        if (m_symbols[0].localPosition.y < -200)
        {
            if (m_break)
            {
                if(m_symbols[0] == m_symbols_stop[0])
                {
                    m_symbols[0].GetComponent<UISprite>().spriteName = Names[2];
                }
                else if (m_symbols[0] == m_symbols_stop[1])
                {
                    m_symbols[0].GetComponent<UISprite>().spriteName = Names[1];
                }
                else if (m_symbols[0] == m_symbols_stop[2])
                {
                    m_symbols[0].GetComponent<UISprite>().spriteName = Names[0];
                }
            }
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


        m_tweener.from = m_tweener.gameObject.transform.localPosition;
        m_tweener.to = new Vector3(transform.localPosition.x, -255, 0);
        m_tweener.ResetToBeginning();
        m_tweener.PlayForward();
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
