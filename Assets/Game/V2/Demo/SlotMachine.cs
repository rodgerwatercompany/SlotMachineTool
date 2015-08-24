using UnityEngine;
using System.Collections;

namespace Game5000
{
    public class SlotMachine : Rodger.SlotmachineBase
    {
        private string[] spriteNames;

        bool m_waitLineTweenCB;
        bool m_waitLineStopCB;

        void Awake()
        {
            string[] spritenames = { "Apple", "Bell", "BlueS", "Orange", "strawberry", "Vine" };

            spriteNames = spritenames;

            m_slotLines = new SlotLine[5];

            for (int i = 0; i < 5; i++)
            {
                SlotLine slotLine = GameObject.Find("SLOTLINE_" + (i + 1)).GetComponent<SlotLine>();
                m_slotLines[i] = slotLine;
                slotLine.onTweenFinishCB = Listener_Tween;

                m_slotLines[i].onStopCB = Listener_SlotStop;
                m_slotLines[i].Init(spritenames);
            }
        }
        // Use this for initialization
        void Start()
        {
        }
        void OnGUI()
        {
            if(GUILayout.Button("StartRun"))
            {
                StartRun();
            }
            if (GUILayout.Button("StartStop"))
            {
                StartStop();
            }
        }
        // Update is called once per frame
        void Update()
        {
            float distance = -1200 * Time.deltaTime;

            m_slotLines[0].Update_Sync(distance);
            m_slotLines[1].Update_Sync(distance);
            m_slotLines[2].Update_Sync(distance);
            m_slotLines[3].Update_Sync(distance);
            m_slotLines[4].Update_Sync(distance);
        }
        void Listener_Tween()
        {
            m_waitLineTweenCB = false;
        }
        void Listener_SlotStop()
        {
            m_waitLineStopCB = false;
        }
        protected override IEnumerator DoStartRun()
        {
            for (int i = 0; i < 5; i++)
            {
                m_slotLines[i].StartMoveUp();

                m_waitLineTweenCB = true;
                while (m_waitLineTweenCB)
                    yield return new WaitForEndOfFrame();
                
                m_slotLines[i].StartRun();
            }

        }
        protected override IEnumerator DoStartStop()
        {
            string str_spriteData = "";
            for (int i = 0; i < 5; i++)
            {
                int[] specifiedSymbols = new int[3]{Random.Range(0,6),Random.Range(0,6),Random.Range(0,6)};

                foreach (int num in specifiedSymbols)
                    str_spriteData += spriteNames[num];

                str_spriteData += "\n";

                m_slotLines[i].SpecifiedSpriteData(specifiedSymbols);
                m_slotLines[i].StartStop();

                m_waitLineStopCB = true;
                while (m_waitLineStopCB)
                    yield return new WaitForEndOfFrame();

                m_slotLines[i].StartMoveDown();
            }
            print(str_spriteData);
        } 
    }
}