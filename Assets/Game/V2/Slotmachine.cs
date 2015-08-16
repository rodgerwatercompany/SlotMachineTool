using UnityEngine;
using System.Collections;

public class Slotmachine : MonoBehaviour {

    public SymbolLine m_symbolLine;

    public float m_symbolspeed;

    bool m_run;

    void Awake()
    {
        m_symbolLine.OnSymbolLineStopCB = OnSymbolStop;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(m_run)
        {
            float dis = -1 * m_symbolspeed * Time.deltaTime;
            m_symbolLine.SetMove(dis);
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
        m_symbolLine.StartRun();
    }
    void StartStop()
    {
        m_symbolLine.StartStop();
    }

    void OnSymbolStop()
    {
        m_run = false;
        print("OnSymbolStop");
    }

}
