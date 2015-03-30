using UnityEngine;
using System.Collections;

public class wheel : MonoBehaviour {

    public bool bClick_Start;
    public bool bClick_Break;
    bool sw_move;
    bool bBreak;
    bool bCountDown;

    public float speed;

    public int SpecifyArea;
    public int SpecifyNum;
    public float SpecifyPos;

    float cnt_times;
    public float waittime;


	// Use this for initialization
	void Start () {

        speed = 200;
        bClick_Start = false;
        bBreak = false;

    }
	
	// Update is called once per frame
	void Update () {
        
        if(bClick_Start)
        {
            bClick_Start = false;
            bClick_Break = false;

            sw_move = true;
            bBreak = false;
            bCountDown = false;
        }

        if(bClick_Break)
        {
            bClick_Break = false;
            //bBreak = true;
            bCountDown = true;
            cnt_times = 0.0f;
        }

        if(bCountDown)
        {
            cnt_times += Time.deltaTime;

            if(cnt_times > waittime)
            {
                bCountDown = false;
                bBreak = true;

                this.SpecifyArea = GetNextArea();
                this.SpecifyPos = CalStopPosition(this.SpecifyNum, this.SpecifyArea);
            }
        }

        if (sw_move)
        {
            float dz = speed * Time.deltaTime;
            float old_z = gameObject.transform.localRotation.eulerAngles.z;
            float next_z = old_z + dz;
            if (next_z >= 360.0f)
                next_z -= 360.0f;

            if (bBreak)
            {

                if(SpecifyArea == GetNowArea())
                {
                    if(next_z >= this.SpecifyPos)
                    {
                        gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, this.SpecifyPos);

                        sw_move = false;
                    }
                    else
                    {
                        // 直接換位置
                        gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, next_z);
                    }
                }
                else
                {
                    // 直接換位置
                    gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, next_z);
                }
            }
            else
            {
                // 直接換位置
                gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, next_z);
            }
        }
	}

    int GetNowArea()
    {

        float pos_z = gameObject.transform.localRotation.eulerAngles.z;

        // id_area 為下一區域
        if (pos_z >= 0.0f && pos_z < 90.0f)
        {
            //id_area = 0;
            return 0;
        }
        else if (pos_z >= 90.0f && pos_z < 180.0f)
        {
            return 1;
        }
        else if (pos_z >= 180.0f && pos_z < 270.0f)
        {
            return 2;
        }
        else if (pos_z >= 270.0f && pos_z <= 360.0f)
        {
            return 3;
        }

        return 5;

    }

    int GetNextArea()
    {

        float pos_z = gameObject.transform.localRotation.eulerAngles.z;

        int id_next_area = -1;

        // id_area 為下一區域
        if (pos_z >= 0.0f && pos_z < 90.0f)
        {
            //id_area = 0;
            id_next_area = 1;
        }
        else if (pos_z >= 90.0f && pos_z < 180.0f)
        {
            id_next_area = 2;
        }
        else if (pos_z >= 180.0f && pos_z < 270.0f)
        {
            id_next_area = 3;
        }
        else if (pos_z >= 270.0f && pos_z < 360.0f)
        {
            id_next_area = 0;
        }
        else
        {
            return -1;
        }

        return id_next_area;

    }

    // 回傳對應位置
    float CalStopPosition(int specifynum, int id_area)
    {
        return GetAngle(1,specifynum) + (90.0f * id_area);
    }
    
    // 輸入指定倍率，計算對應角度
    // Grand Major Minor Mini
    float GetAngle(int JPType,float specifynum)
    {

        if(JPType == 4)
        {
            if(specifynum == 2)
            {
                return 15.0f;
            }
            else if (specifynum == 3)
            {
                return 45.0f;
            }
            else if (specifynum == 1)
            {
                return 75.0f;
            }
        }
        else if (JPType == 3)
        {
            if (specifynum == 3)
            {
                return 15.0f;
            }
            else if (specifynum == 5)
            {
                return 45.0f;
            }
            else if (specifynum == 2)
            {
                return 75.0f;
            }

        }
        else if (JPType == 2)
        {
            if (specifynum == 5)
            {
                return 15.0f;
            }
            else if (specifynum == 8)
            {
                return 45.0f;
            }
            else if (specifynum == 3)
            {
                return 75.0f;
            }

        }
        else if (JPType == 1)
        {
            if (specifynum == 10)
            {
                return 15.0f;
            }
            else if (specifynum == 15)
            {
                return 45.0f;
            }
            else if (specifynum == 5)
            {
                return 75.0f;
            }

        }

        return 0.0f;
    }

}