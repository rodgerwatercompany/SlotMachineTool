using UnityEngine;

using System.Collections;


namespace Rodger
{
    public class TileLine : MonoBehaviour
    {

        private float fspeed;

        public TileObject[] tileObjects;

        private bool sw_Move_all;

        protected int[] randomsprites;
        protected string prefix_Symbolname;
        protected string format_SymbolNum;

        private bool sw_Break;
        private bool sw_Rebound;

        private float timer_rebound;
        
        public delegate void CallBack();

        CallBack tilefinishspin;

        int soundStopIdx;

        // Use this for initialization
        void Start()
        {
            this.sw_Move_all = false;
            this.sw_Break = false;
        }

        public void Init(string prefix, string format_num ,int[] rands,int soundstopidx)
        {
            prefix_Symbolname = prefix;
            format_SymbolNum = format_num;
            randomsprites = rands;
            soundStopIdx = soundstopidx;
        }

        public void Update_Sync()
        {
            if (sw_Move_all)
            {
                MoveSymbol();
            }
        }

        public void SetSpeed(float speed)
        {
            this.fspeed = speed;
        }

        public float GetSpeed()
        {
            return this.fspeed;
        }

        public void MoveSymbol()
        {
            float dis = this.fspeed * Time.deltaTime;

            // 在此判斷為反彈不執行位置更改
            if (!sw_Rebound)
            {
                float pos_new_y;
                for (int idx = 0; idx < 8; idx++)
                {
                    pos_new_y = tileObjects[idx].transform.localPosition.y - dis;

                    tileObjects[idx].SetPosition(pos_new_y);
                }
            }
            else
                timer_rebound += Time.deltaTime;

            if (!sw_Break)
            {
                if ((tileObjects[0].transform.localPosition.y - dis) < -190.0f)
                    Turnround();
            }
            else
            {
                if (!sw_Rebound)
                {
                    if (tileObjects[5].transform.localPosition.y <= -40.0f)
                    {
                        sw_Rebound = true;
                        timer_rebound = 0.0f;
                        //SoundManager.Instance.StopAndPlay(soundStopIdx);
                    }
                }
                else
                {
                    if (timer_rebound >= 0.2f)
                    {
                        float _pos_new_y;
                        for (int idx = 0; idx < 8; idx++)
                        {
                            _pos_new_y = -950.0f + idx * 190.0f; ;

                            tileObjects[idx].SetPosition(_pos_new_y);
                        }

                        sw_Move_all = false;

                        tilefinishspin();
                    }
                    else
                    {
                        // 執行取得反彈對應位置
                        float pos_y_5 = SlotMachine.moveMethod.GetOutput(timer_rebound);

                        float pos_new_y;
                        for (int idx = 0; idx < 8; idx++)
                        {
                            pos_new_y = pos_y_5 + ((idx - 5) * 190);

                            tileObjects[idx].SetPosition(pos_new_y);
                        }
                    }

                }
            }
        }        

        public void StartRun()
        {
            sw_Move_all = true;
            
            sw_Break = false;
            sw_Rebound = false;

            //SetAlpha(0.7f);
        }
        public void StopRun(CallBack finishspin)
        {
            this.sw_Break = true;
            //SetAlpha(1);
            tilefinishspin = new CallBack(finishspin);
        }

        public void SetSprites(string[] idxs)
        {
            for (int i = 7, j = 0; j < idxs.Length; i--, j++)
                tileObjects[i].SetSprite(idxs[j]);
        }

        public void JackPotChangeCard(CallBack cb)
        {
            StartCoroutine(ChangeCardFlow(cb));
        }
        // 5 6 7 不動
        private void ResetPosition()
        {
            TileObject[] temps = new TileObject[8];

            for (int i = 0; i < 8; i++)
            {
                temps[i] = tileObjects[i];
            }

            for (int i = 0; i < 3; i++)
            {
                tileObjects[i] = temps[i + 5];
            }

            for (int i = 0; i < 5; i++)
            {
                tileObjects[i + 3] = temps[i];
                tileObjects[i + 3].SetPosition(570.0f + 190.0f * i);
            }

        }

        private void SetAlpha(float value)
        {
            for (int i = 0; i < tileObjects.Length; i++)
                tileObjects[i].SetAlpha(value);
        }
        // 處理最前面掉頭(不使用List)
        private void Turnround()
        {
            TileObject temp = tileObjects[0];
            for (int idx = 0; idx < tileObjects.Length; idx++)
            {
                if (idx != tileObjects.Length - 1)
                    tileObjects[idx] = tileObjects[idx + 1];
                else
                {
                    //print(idx + " " + temp.name);
                    tileObjects[idx] = temp;
                    float pos_y = tileObjects[idx - 1].transform.localPosition.y;
                    tileObjects[idx].SetPosition(pos_y + 190);

                    int rand = Random.Range(0, randomsprites.Length);
                    tileObjects[idx].SetSprite(prefix_Symbolname + randomsprites[rand].ToString(format_SymbolNum));
                }
            }
            /*
            string str = "";
            for (int i = 0; i < tileObjects.Length; i++)
                str += tileObjects[i].name + "\n";

            print(str);
            */
        }

        IEnumerator ChangeCardFlow(CallBack cb)
        {
            bool sw_forward = true;
            float dis = 150.0f * Time.deltaTime;

            while(true)
            {
                // 先正轉至90度換圖
                if (sw_forward)
                {
                    tileObjects[6].transform.Rotate(new Vector3(0.0f, dis, 0.0f));

                    if (tileObjects[6].transform.localRotation.eulerAngles.y > 90.0f)
                    {
                        sw_forward = false;
                        tileObjects[6].SetSprite("symbol_013");
                    }
                }
                // 反轉復原角度
                else
                {
                    tileObjects[6].transform.Rotate(new Vector3(0.0f, -dis, 0.0f));

                    // 因為尤拉角沒有負值，y 會永遠大於 0，故不採用尤拉角判別。
                    if (tileObjects[6].transform.localRotation.y < 0.0f)
                    {
                        tileObjects[6].transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        break;
                    }
                }
                yield return new WaitForEndOfFrame();
            }

            cb();
        }
    }
}