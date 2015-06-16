﻿using UnityEngine;


namespace Game5835
{
    public class SlotMachine : MonoBehaviour
    {
        public static MoveMethod moveMethod = new XplusXMethod();

        ISlotMachine2GM Islotmachine2GM;

        public SoundManager soundMgr;

        public TileLine[] tileLines;
        public float SlotSpeed_max;

        private float[] fspeeds;

        private bool bBreak;

        private bool bAutoBreak;

        private string[] tilesprites;

        private int cnt_finishstop;

        // Use this for initialization
        void Start()
        {
            moveMethod.Init(-40.0f, 0.0f, 0.5f);

            //Islotmachine2GM = GameManager.Instance;

            foreach (TileLine tileline in tileLines)
                tileline.SetSpeed(this.SlotSpeed_max);

            bBreak = false;
            bAutoBreak = false;
            fspeeds = new float[5] { 0, 0, 0, 0, 0 };
        }

        // Update is called once per frame
        void Update()
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

        public void OnClick_Spin()
        {
            Islotmachine2GM.OnClick_Spin();
        }

        public void OnClick_AutoSpin()
        {
            Islotmachine2GM.OnClick_AutoSpin();
        }

        public void OnClick_StopAutoSpin()
        {
            Islotmachine2GM.OnClick_StopAutoSpin();
        }

        public void OnClick_GetScore()
        {
            Islotmachine2GM.OnClick_GetScore();
        }

        public void StartSpin()
        {
            this.StartRun();
        }

        void StartRun()
        {
            soundMgr.Play(0, false);
            soundMgr.AddPlay(2);

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
            soundMgr.Play(3, false);

            cnt_finishstop++;

            if (cnt_finishstop == tileLines.Length)
            {
                cnt_finishstop = 0;

                //Islotmachine2GM.OnStop();
            }
        }

    }
}