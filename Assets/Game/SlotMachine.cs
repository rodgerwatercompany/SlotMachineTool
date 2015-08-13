using UnityEngine;
using System.Collections;

using Rodger;

namespace Game5095
{
    
    public class SlotMachine : Rodger.SlotMachine
    {
        public Transform[] bgboards;
        public Transform[] bingolines;
        public Transform bingoScore;

        public UISprite[] LineNumbers;

        public float speed_extend;
        public float speed_shrink;
        int[] lineIDFindScorePos;

        bool sw_bingoline;
        //BingoLineObject bingoLineObj;

        VOIDCB BingoLineEndingCB;
        int now_bingoLineTimerID;

        bool state_WaitTimer;
        bool state_WaitScoreLabel;

        protected override void Start()
        {
            base.Start();
            /*
            for (int i = 0; i < 5; i++)
                bgboards[i].gameObject.SetActive(false);
            */
            //bingoLineObj = new BingoLineObject();
            /*
            bingoScore.gameObject.SetActive(false);

            bingoScore.localScale = new Vector3(0.8f, 0.8f, 0);
            */
            lineIDFindScorePos = new int[18] 
            {
                5,4,6,
                5,5,4,
                6,6,4,
                11,10,12,
                11,11,12,
                10,10,12
            };

            tileLines[0].Init("symbol_", "000", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },14);
            tileLines[1].Init("symbol_", "000", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },14);
            tileLines[2].Init("symbol_", "000", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },14);
            tileLines[3].Init("symbol_", "000", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },14);
            tileLines[4].Init("symbol_", "000", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },14);

            soundStart_idx = 1;
            soundRun_idx = 13;
        }

        protected override void Update()
        {
            base.Update();            
        }
        //public void PlayBingoLine(int[] idxs,int lineID,int score, VOIDCB _endingCB)
        //{
        //    sw_bingoline = true;

        //    state_WaitScoreLabel = false;
        //    state_WaitTimer = false;

        //    bingoLineObj.idxs = idxs;
        //    bingoLineObj.LineID = lineID;
        //    bingoLineObj.Score = score;
        //    bingoLineObj.idx_now = 0;

        //    BingoLineEndingCB = _endingCB;

        //    // Initial
        //    for (int i = 0; i < idxs.Length; i++)
        //    {
        //        bgboards[i].parent = GetSymbolObject(idxs[i]).transform;
        //        bgboards[i].localPosition = Vector3.zero;
        //        bgboards[i].localScale = Vector3.one;
        //        bgboards[i].GetComponent<UISprite>().spriteName = "win";
        //    }

        //    SetBingoLine(bingoLineObj.LineID, true);
        //    SetLineNumberBG(bingoLineObj.LineID, true);

        //    state_WaitTimer = true;
        //    now_bingoLineTimerID = Timer.Instance.CreateTimer(1, DoExtend);
        //}
        //void DoExtend()
        //{
        //    state_WaitTimer = false;

        //    if (sw_bingoline)
        //    {
        //        if (bingoLineObj.idx_now == bingoLineObj.idxs.Length)
        //            GetSymbolObject(bingoLineObj.idxs[1]).GetComponent<AnimationBase>().Extend(speed_extend, 1, 1.3f, Listener_DoExtendCB);
        //        else
        //        {
        //            bgboards[bingoLineObj.idx_now].gameObject.SetActive(true);

        //            GetSymbolObject(bingoLineObj.idxs[bingoLineObj.idx_now]).GetComponent<AnimationBase>().SetDepth(40);
        //            GetSymbolObject(bingoLineObj.idxs[bingoLineObj.idx_now]).GetComponent<AnimationBase>().Extend(speed_extend, 1, 1.3f, Listener_DoExtendCB);
        //        }
        //    }
        //    else
        //        ClearBingoLine();
        //}
        //void Listener_DoExtendCB()
        //{
        //    if (sw_bingoline)
        //    {
        //        if (bingoLineObj.idx_now < bingoLineObj.idxs.Length)
        //        {
        //            GetSymbolObject(bingoLineObj.idxs[bingoLineObj.idx_now]).GetComponent<AnimationBase>().Shrink(speed_shrink, 1.3f, 1, null);
        //            bingoLineObj.idx_now++;

        //            DoExtend();
        //        }
        //        else
        //        {
        //            bgboards[1].GetComponent<UISprite>().spriteName = "win_glow";

        //            int idx = lineIDFindScorePos[bingoLineObj.LineID - 1] - 1;

        //            float s_x = ((idx / 3) * 210) - 420;
        //            float s_y = 380 - ((idx % 3) * 190);
        //            bingoScore.localPosition = new Vector3(s_x, s_y - 60, 0);
        //            bingoScore.gameObject.SetActive(true);

        //            LabelBase label = bingoScore.GetComponent<LabelBase>();
        //            label.SetLabelText(bingoLineObj.Score.ToString());
        //            label.Extend(speed_extend, 0.8f, 1, Listener_DoExtendScoreCB);
        //            state_WaitScoreLabel = true;
        //        }
        //    }
        //    else
        //        ClearBingoLine();
        //}
        //void Listener_DoExtendScoreCB()
        //{
        //    state_WaitTimer = true;
        //    now_bingoLineTimerID = Timer.Instance.CreateTimer(1, Listener_Ending);
        //}
        //void Listener_Ending()
        //{

        //    if (sw_bingoline)
        //    {
        //        if (BingoLineEndingCB != null)
        //            BingoLineEndingCB();                
        //    }
        //    else
        //        ClearBingoLine();
        //}
        //public void ClearBingoLine()
        //{
        //    // 關閉木板背景
        //    for (int i = 0; i < bingoLineObj.idxs.Length; i++)
        //    {
        //        bgboards[i].gameObject.SetActive(false);
        //        GetSymbolObject(bingoLineObj.idxs[i]).transform.localScale = Vector3.one;
        //        GetSymbolObject(bingoLineObj.idxs[i]).GetComponent<AnimationBase>().SetDepth(2);
        //    }

        //    SetBingoLine(bingoLineObj.LineID, false);
        //    SetLineNumberBG(bingoLineObj.LineID, false);

        //    bingoScore.localScale = new Vector3(0.8f, 0.8f, 1);
        //    bingoScore.gameObject.SetActive(false);
            
        //    state_WaitTimer = false;
        //    state_WaitScoreLabel = false;
        //}
        //public void StopPlayBingoLine()
        //{
        //    sw_bingoline = false;
        //    if(state_WaitTimer)
        //        Timer.Instance.StopTimer(now_bingoLineTimerID);
        //    else if(state_WaitScoreLabel)
        //    {
        //        print("stop lable");
        //        LabelBase label = bingoScore.GetComponent<LabelBase>();
        //        label.StopExtend();
        //    }
        //    state_WaitTimer = false;
        //    state_WaitScoreLabel = false;
        //}
        
        //void SetBingoLine(int _lineID, bool enable)
        //{
        //    if (_lineID > 9)
        //        _lineID -= 9;

        //    bingolines[_lineID - 1].gameObject.SetActive(enable);
        //}
        //void SetLineNumberBG(int _lineID,bool enable)
        //{

        //    if(!enable)
        //        LineNumbers[_lineID - 1].spriteName = "line_number_bg.PNG";
        //    else
        //        LineNumbers[_lineID - 1].spriteName = "line_number_bg_light.PNG";
        //}

    }
}
