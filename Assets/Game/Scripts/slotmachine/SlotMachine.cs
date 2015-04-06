using UnityEngine;

public class SlotMachine : MonoBehaviour {

    public TileLine[] tileLines;
    public float SlotSpeed_max;

    private float[] fspeeds;

    private bool bBreak;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

        foreach (TileLine tileline in tileLines)
            tileline.SetSpeed(this.SlotSpeed_max);

        bBreak = false;
        fspeeds = new float[5] { 1000, 1000 , 1000 , 1000  , 1000 };
    }
	
	// Update is called once per frame
	void Update () {

	    if(bBreak)
        {

            int idx = GetNowIdx();
            if (idx != -1)
            {

                fspeeds[idx] -= (1000 * Time.deltaTime);

                if (fspeeds[idx] <= 600.0f)
                {

                    fspeeds[idx] = 600.0f;
                    tileLines[idx].StopRun();
                }

                tileLines[idx].SetSpeed(fspeeds[idx]);
            }
            else
                bBreak = false;
        }
	}
    
    public void OnClick_StartRotate()
    {
        tileLines[0].StartRotate("Bell");
    }

    public void OnClick_StartRun()
    {

        for (int i = 0; i < 5; i++)
        {
            fspeeds[i] = this.SlotSpeed_max;
            tileLines[i].SetSpeed(this.SlotSpeed_max);
        }

        foreach (TileLine tileline in tileLines)
            tileline.StartRun();
    }

    public void OnClick_StartStop()
    {

        bBreak = true;
        /*
        foreach (TileLine tileline in tileLines)
            tileline.StopRun();
         */
    }

    private int GetNowIdx()
    {

        for(int i = 0; i < 5; i++)
        {
            if (fspeeds[i] > 600.0f)
                return i;            
        }
        return -1;
    }
}
