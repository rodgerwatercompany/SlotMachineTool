using UnityEngine;
using System.Collections;

namespace Rodger
{
    public class SlotLines : MonoBehaviour
    {
        public Transform[] m_trans_symbols;

        Transform[] m_trans_stopOrder;

        float m_framespeed_moveDis;

        bool m_moving;
        bool m_breaking;

        string[] ChangeSpriteNames;

        string[] m_SpriteName_Show;

        void Init()
        {
            if (m_trans_symbols.Length != 8)
                Debug.LogError("SlotLines Initailize failed !");
        }
        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {

        }

        public void SlotMachineUpdate()
        {

        }

        void Move()
        {

            for (int i = 0; i < m_trans_symbols.Length; i++)
                m_trans_symbols[i].localPosition = new Vector3(0, m_trans_symbols[i].localPosition.y + m_framespeed_moveDis, 0);

            if (m_trans_symbols[0].localPosition.y < -200)
            {
                if (m_breaking)
                {
                    if (m_trans_symbols[0] == m_trans_stopOrder[0])
                    {
                        m_trans_symbols[0].GetComponent<UISprite>().spriteName = m_SpriteName_Show[2];
                    }
                    else if (m_trans_symbols[0] == m_trans_stopOrder[1])
                    {
                        m_trans_symbols[0].GetComponent<UISprite>().spriteName = m_SpriteName_Show[1];
                    }
                    else if (m_trans_symbols[0] == m_trans_stopOrder[2])
                    {
                        m_trans_symbols[0].GetComponent<UISprite>().spriteName = m_SpriteName_Show[0];
                    }
                }

                TurnAround();
                float y = m_trans_symbols[m_trans_symbols.Length - 2].localPosition.y + 190;
                m_trans_symbols[m_trans_symbols.Length - 1].localPosition = new Vector3(0, y, 0);
            }
        }

        // 換掉第一個
        void TurnAround()
        {
            for (int i = 1; i < m_trans_symbols.Length; i++)
            {
                Transform temp = m_trans_symbols[i - 1];
                m_trans_symbols[i - 1] = m_trans_symbols[i];
                m_trans_symbols[i] = temp;
            }
        }
    }
}