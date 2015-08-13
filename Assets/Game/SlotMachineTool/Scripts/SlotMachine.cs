using UnityEngine;

using System.Collections;

namespace Rodger
{
    public class SlotMachine : MonoBehaviour
    {
        static public MoveMethod moveMethod = new XplusXMethod();

        static public SlotMachine Instance;
        
        public VOIDCB onSlotMachineStopCB;
        public VOIDCB onJackPotFinishCB;
        
        public TileLine[] tileLines;
        public float SlotSpeed_max;

        private float[] fspeeds;

        private bool bBreak;

        private bool bAutoBreak;

        private bool sw_waitJProtate;

        private string[] tilesprites;

        private int cnt_finishstop;

        protected int soundStart_idx;
        protected int soundRun_idx;

        protected virtual void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        protected virtual void Start()
        {
            moveMethod.Init(-40.0f, 0.0f, 0.2f);
            
            foreach (TileLine tileline in tileLines)
                tileline.SetSpeed(this.SlotSpeed_max);

            bBreak = false;
            bAutoBreak = false;
            fspeeds = new float[5] { 0, 0, 0, 0, 0 };

            soundStart_idx = 0;
            soundRun_idx = 1;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            if (bBreak)
            {

                int idx = GetNowIdx();
                if (idx != -1)
                {

                    fspeeds[idx] -= (2000 * Time.deltaTime);

                    if (fspeeds[idx] <= 1000.0f)
                    {
                        fspeeds[idx] = 1000.0f;
                        tileLines[idx].SetSprites(GetTileSpriteInfo(idx));
                        tileLines[idx].StopRun(this.FinishSpin);
                    }

                    tileLines[idx].SetSpeed(fspeeds[idx]);
                }
                else
                {
                    bBreak = false;
                }
            }
            else if (bAutoBreak)
            {
                int cnt_over = 0;
                float fspeed = 2000 * Time.deltaTime;
                for (int i = 0; i < tileLines.Length; i++)
                {
                    if (fspeeds[i] > 1000.0f)
                    {
                        fspeeds[i] -= fspeed;

                        if (fspeeds[i] <= 1000.0f)
                        {
                            fspeeds[i] = 1000.0f;
                            tileLines[i].SetSprites(GetTileSpriteInfo(i));
                            tileLines[i].StopRun(this.FinishSpin);
                        }

                        tileLines[i].SetSpeed(fspeeds[i]);
                    }
                    else
                    {
                        cnt_over++;
                    }
                }

                if (cnt_over >= tileLines.Length - 1)
                {
                    bAutoBreak = false;
                }
            }

            tileLines[0].Update_Sync();
            tileLines[1].Update_Sync();
            tileLines[2].Update_Sync();
            tileLines[3].Update_Sync();
            tileLines[4].Update_Sync();
        }       

        public void StartSpin(bool b_playFirstSound)
        {
            this.StartRun(b_playFirstSound);
        }

        void StartRun(bool b_playFirstSound)
        {
            /*
            if(b_playFirstSound)
                SoundManager.Instance.Play(soundStart_idx);

            SoundManager.Instance.Play(soundRun_idx);
            */
            cnt_finishstop = 0;

            for (int i = 0; i < 5; i++)
            {
                fspeeds[i] = this.SlotSpeed_max;
                tileLines[i].SetSpeed(this.SlotSpeed_max);
            }

            foreach (TileLine tileline in tileLines)
                tileline.StartRun();

            bBreak = false;
            bAutoBreak = false;
        }

        // 慢慢的自動停止
        public void OnClick_StartStop()
        {

            bBreak = true;
            /*
            foreach (TileLine tileline in tileLines)
                tileline.StopRun();
             */
        }

        // 自動轉使用的停止
        public void OnClick_StartStop_Immediate()
        {
            bAutoBreak = true;
            bBreak = false;
        }

        // 手動強制停止
        public void OnClick_Stop()
        {
            bBreak = false;

            for (int idx = 0; idx < tileLines.Length; idx++)
            {
                tileLines[idx].SetSprites(GetTileSpriteInfo(idx));

                if (tileLines[idx].GetSpeed() > 1000.0f)
                    tileLines[idx].SetSpeed(1000.0f);
                tileLines[idx].StopRun(FinishSpin);
            }
        }

        public void SetTileSpriteInfo(string[] sprites)
        {

            tilesprites = sprites;
            /*
            foreach (string name in tilesprites)
                print("id is " + name);
            */
        }

        // 1 ~ 15
        public GameObject GetSymbolObject(int idx)
        {
            idx = idx - 1;

            return tileLines[(idx / 3)].tileObjects[7 - (idx % 3)].gameObject;
        }

        private string[] GetTileSpriteInfo(int idx)
        {

            int len = tilesprites.Length / tileLines.Length;

            int start = (idx * len);

            string[] ret = new string[len];

            for (int i = 0, j = start; j < start + len; i++, j++)
            {
                ret[i] = tilesprites[j];
                //print("idx : " + idx + " tilesprites[" + j + "] : " + tilesprites[j]);
            }

            return ret;
        }

        private int GetNowIdx()
        {

            for (int i = 0; i < 5; i++)
            {
                if (fspeeds[i] > 1000.0f)
                    return i;
            }
            return -1;
        }

        // 當TileLine完成停止
        public void FinishSpin()
        {

            cnt_finishstop++;

            if (cnt_finishstop == tileLines.Length)
            {
                cnt_finishstop = 0;

                if (onSlotMachineStopCB != null)
                    onSlotMachineStopCB();
            }
        }

        public void StartChangeCardFlow(int type)
        {
            StartCoroutine(ChangeCard(6 - type));
        }

        public void FinishJackPotLineCB()
        {
            sw_waitJProtate = false;
        }

        IEnumerator ChangeCard(int num_changeCard)
        {
            int num_nowid = 0;

            while(num_nowid < num_changeCard)
            {
                tileLines[num_nowid].JackPotChangeCard(FinishJackPotLineCB);
                sw_waitJProtate = true;
                while (sw_waitJProtate)
                {
                    yield return new WaitForEndOfFrame();
                }
                num_nowid++;
            }

            if (onJackPotFinishCB != null)
                onJackPotFinishCB();
        }
    }
}