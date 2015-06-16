using UnityEngine;
using System.Collections;

public class FunctionPrint : MonoBehaviour {

	// Use this for initialization
	void Start () {

        float x = 0.0f , y = 0.0f;
        string str = "";
        while(y < 1.0f)
        {
            y = x * x;
            str += "x:" + x + " , y =  " +  y + "\n";
            x += 0.1f;
        }

        print(str);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
