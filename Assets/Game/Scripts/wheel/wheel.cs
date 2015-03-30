using UnityEngine;
using System.Collections;

public class wheel : MonoBehaviour {

    public bool sw_move;
    public bool bBreak_1,bBreak_2;

    public float speed;

    public float specifyNum;

	// Use this for initialization
	void Start () {

        speed = 200;
        bBreak_1 = false;
        bBreak_2 = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(bBreak_1)
        {
            if(speed >= 100.0f)
                speed -= 10.0f * Time.deltaTime;
            else
            {
                bBreak_1 = false;
                bBreak_2 = true;
            }
        }

        if (sw_move)
        {
            float dz = speed * Time.deltaTime;
            float old_z = gameObject.transform.localRotation.eulerAngles.z;
            float next_z = old_z + dz;
            if (next_z >= 360.0f)
                next_z -= 360.0f;
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, next_z);
        }
	}

}
