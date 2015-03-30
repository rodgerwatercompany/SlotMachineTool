using UnityEngine;
using System.Collections;

public class TileLine : MonoBehaviour {

    private float fspeed;


    public TileObject[] tileObjects;

    private bool sw_Move;
    private bool sw_Break;

    private bool sw_Timercount;

	// Use this for initialization
	void Start () {

        this.sw_Move = false;
        this.sw_Break = false;
        this.sw_Timercount = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (this.sw_Move)
            this.Move();

	}

    public void SetSpeed(float speed)
    {
        this.fspeed = speed;
    }

    public void Move()
    {

        float dis = this.fspeed * Time.deltaTime;

        bool btunround = false;

        for (int i = 0; i < tileObjects.Length; i++)
        {
            float pos_old = tileObjects[i].transform.localPosition.y;
            float pos_new = tileObjects[i].transform.localPosition.y - dis;
            float limit = -750.0f + i * 150.0f;

            if (sw_Break)
            {
                if (pos_new <= limit)
                {
                    //tileObjects[i].transform.localPosition = new Vector3(0.0f, limit, 0.0f);
                    tileObjects[i].SetPosition(limit);
                    this.sw_Move = false;
                }
                else
                {
                    //tileObjects[i].transform.localPosition = new Vector3(0.0f, pos_new, 0.0f);
                    tileObjects[i].SetPosition(pos_new);
                }
            }
            else
            {
                //tileObjects[i].transform.localPosition = new Vector3(0.0f, pos_new, 0.0f);
                tileObjects[i].SetPosition(pos_new);

                if (pos_new < -150.0f)
                {
                    //print("btrunround");
                    btunround = true;
                }

            }
        }

        if (btunround)
            this.Turnround();
        
        if (!this.sw_Move)
            this.ResetPosition();
        
    }

    public void StartRun()
    {

        this.sw_Move = true;
        this.sw_Break = false;
    }
    public void StopRun()
    {

        this.sw_Break = true;
    }

    private void TimerCountDown()
    {

    }
    // 5 6 7 不動
    private void ResetPosition()
    {
        TileObject[] temps = new TileObject[8];

        for (int i = 0; i < 8; i++)
        {
            temps[i] = tileObjects[i];
        }

        for (int i = 0; i < 3; i++)
        {
            tileObjects[i] = temps[i + 5];
        }

        for (int i = 0; i < 5; i++)
        {
            tileObjects[i + 3] = temps[i];
            tileObjects[i + 3].SetPosition(450.0f + 150.0f * i);
        }

    }

    // 處理最前面掉頭(不使用List)
    private void Turnround()
    {
        TileObject temp = tileObjects[0];
        for (int idx = 0; idx < tileObjects.Length ; idx++)
        {
            if (idx != tileObjects.Length - 1)
                tileObjects[idx] = tileObjects[idx + 1];
            else
            {
                //print(idx + " " + temp.name);
                tileObjects[idx] = temp;
                float pos_y = tileObjects[idx - 1].transform.localPosition.y;
                tileObjects[idx].SetPosition(pos_y + 150.0f);
            }
        }
        /*
        string str = "";
        for (int i = 0; i < tileObjects.Length; i++)
            str += tileObjects[i].name + "\n";

        print(str);
        */
    }
}
