using UnityEngine;
using System.Collections;

public class TileObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPosition(float pos_y)
    {
        gameObject.transform.localPosition = new Vector3(0.0f, pos_y, 0.0f);
    }
}
