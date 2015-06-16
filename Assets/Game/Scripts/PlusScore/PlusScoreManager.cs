using UnityEngine;
using System;
using System.Collections;

public class PlusScoreManager : MonoBehaviour {

    string finalscore;
    int score;
    bool sw_startplusscore;

    int[] powertable = { 1, 10, 100, 1000, 10000, 100000, 1000000 , 10000000, 100000000};

    float fcnt;
	// Use this for initialization
	void Start () {

        finalscore = "99999";
        score = 1234;
        sw_startplusscore = false;

        char cc = '0';
        print(Convert.ToInt32(cc)- '0');
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(sw_startplusscore)
        {
            if (fcnt < 0.15f)
            {
                fcnt += Time.deltaTime;
            }
            else
            {
                fcnt = 0.0f;

                if (!string.IsNullOrEmpty(finalscore))
                {
                    int ifscore = Convert.ToInt32(finalscore);

                    if (score < ifscore)
                    {
                        int diff = ifscore - score;

                        char[] arr_char = diff.ToString().ToCharArray();

                        int inc = powertable[arr_char.Length - 1];

                        score += inc;
                    }
                    else
                    {
                        score = ifscore;
                        sw_startplusscore = false;
                    }
                }
                else
                {
                    sw_startplusscore = false;
                }
            }
        }
	}

    void OnGUI()
    {
        finalscore = GUILayout.TextField(finalscore, GUILayout.Width(100.0f), GUILayout.Height(100.0f));

        GUILayout.Label("Score : " + score, GUILayout.Width(500.0f), GUILayout.Height(100.0f));

        if(GUILayout.Button("PlusScore", GUILayout.Width(100.0f), GUILayout.Height(100.0f)))
        {
            sw_startplusscore = true;

            fcnt = 0.0f;
        }
    }

}
