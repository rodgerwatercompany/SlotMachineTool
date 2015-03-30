using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {

    public UILabel uilabel;

    int cnt_fps;
    float cnt_times;
	// Use this for initialization
	void Start () {

        cnt_fps = 0;
        cnt_times = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        cnt_times += Time.deltaTime;
        if (cnt_times < 1.0f)
        {
            cnt_fps++;
        }
        else
        {

            uilabel.text = "FPS : " + cnt_fps;
            cnt_times = 0.0f;
            cnt_fps = 0;
        }
    }
}
