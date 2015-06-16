using UnityEngine;
using System.Collections;

public class ShowTweenPosition : MonoBehaviour {


    bool sw_on;
    float cnt_timer;
    string str;
    // Use this for initialization
    void Start () {
        
        sw_on = true;
        cnt_timer = 0.0f;
        str = "";
    }
	
	// Update is called once per frame
	void Update () {

        if (sw_on)
        {
            cnt_timer += Time.deltaTime;
            float slope = transform.localPosition.y / cnt_timer;
            str += "t = " + cnt_timer + " , y = " + transform.localPosition.y + " , slope = " + slope + "\n";
            if (cnt_timer >= 1.0f)
            {
                sw_on = false;
                print("ShowTweenPosition : " + str);           
            }
        }
    }
}
