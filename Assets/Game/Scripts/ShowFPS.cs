using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {

    public UILabel uilabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        uilabel.text = "FPS : " + (1.0f / Time.deltaTime).ToString();
	}
}
