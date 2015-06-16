using UnityEngine;
using System.Collections;

public class MyTweenPosition : MonoBehaviour {

    bool sw_up;
    float cnt_timer;
    string str;
    MoveMethod moveMethod;

    public  float dis_start;
    public float dis_end, costtime;
    // Use this for initialization
    void Start()
    {

        sw_up = true;
        cnt_timer = 0.0f;
        str = "";

        moveMethod = new XplusXMethod();
        /*
        dis_start = 0.0f;
        dis_end = -50.0f;
        costtime = 0.5f;
        */
        moveMethod.Init(dis_start, dis_end, costtime);
    }

    // Update is called once per frame
    void Update () {

        if (sw_up)
        {
            cnt_timer += Time.deltaTime;
            if(cnt_timer >= costtime)
            {
                sw_up = false;
                cnt_timer = costtime;
            }
        }
        else
        {
            cnt_timer -= Time.deltaTime;

            if(cnt_timer <= 0.0f)
            {
                sw_up = true;
                cnt_timer = 0.0f;
            }
        }

        float pos_y = moveMethod.GetOutput(cnt_timer);

        transform.localPosition = new Vector3(transform.localPosition.x, pos_y, 0.0f);
	}

    void SetDir(bool up)
    {

        if(up)
            moveMethod.Init(dis_start, dis_end, costtime);
        else
            moveMethod.Init(dis_end, dis_start, costtime);

    }
}
