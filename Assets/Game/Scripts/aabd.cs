using UnityEngine;

public class aabd : MonoBehaviour {

    public float fspeed;
    public bool bdir;

    bool sw_on, sw_break;

	// Use this for initialization
	void Start () {

        bdir = true;

        sw_on = false; sw_break = false;
    }
	
	// Update is called once per frame
	void Update () {

        Move();
	}

    void Move()
    {

        if (sw_on)
        {
            
            float dis = fspeed * Time.deltaTime;

            float t = (60.0f - Mathf.Abs(transform.localPosition.y)) / 60.0f;
            float dis_lerp = dis * Mathf.Lerp(0.6f,0.8f,t);
            
            float new_pos;

            new_pos = transform.localPosition.y - dis;

            if (sw_break)
            {
                if (bdir)
                {

                    if (new_pos <= -60.0f)
                        bdir = false;
                }
                else
                {
                    print(Mathf.Lerp(0.6f, 0.8f, t));
                    new_pos = transform.localPosition.y + dis_lerp;

                    if (new_pos >= 0.0f)
                    {
                        sw_on = false;
                    }
                }

                transform.localPosition = new Vector3(0.0f, new_pos, 0.0f);
            }
            else
            {
                if (new_pos <= -100.0f)
                    new_pos = 500.0f;

                transform.localPosition = new Vector3(0.0f, new_pos, 0.0f);
            }

        }
    }

    public void OnClick_Start()
    {
        sw_on = true;
        sw_break = false;
        bdir = true;
    }

    public void OnClick_StartStop()
    {
        sw_break = true;
    }
}
