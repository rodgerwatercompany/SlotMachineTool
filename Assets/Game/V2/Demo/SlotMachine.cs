using UnityEngine;
using System.Collections;

namespace Game5000
{
    public class SlotMachine :Rodger.SlotmachineBase
    {
        void Awake()
        {
        }
        // Use this for initialization
        void Start()
        {
        string[] spritenames = { "Apple", "Bell", "BlueS", "Orange", "strawberry", "Vine" };
            for(int i = 0; i < 5; i++)
            {
                m_slotLines[i].onStopCB = Listener;
                m_slotLines[i].Init(spritenames);
            }
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

        public void StartRun()
        {
            base.StartRun();
        }
        protected override IEnumerator DoStartRun()
        {
            for (int i = 0; i < 5; i++)
            {
                m_slotLines[i].StartMoveUp();

                m_WaitCB = true;
                while (m_WaitCB)
                    yield return new WaitForEndOfFrame();
                
                m_slotLines[i].StartRun();
            }

        }
        protected override IEnumerator DoStartStop()
        {
            for (int i = 0; i < 5; i++)
            {
                int[] specifiedSymbols = new int[3]{Random.Range(0,6),Random.Range(0,6),Random.Range(0,6)};

                m_slotLines[i].SpecifiedSpriteData(specifiedSymbols);
                m_slotLines[i].StartStop();

                m_WaitCB = true;
                while (m_WaitCB)
                    yield return new WaitForEndOfFrame();
            }
        } 
    }
}