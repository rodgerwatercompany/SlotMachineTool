using UnityEngine;
using System.Collections;

namespace Game5000
{
    public class SlotLine : Rodger.SlotLineBase
    {

        TweenPosition m_tweener;

        public VOIDCB onTweenFinishCB;
        protected override  void Awake()
        {
            base.Awake();
            
            m_tweener = _GetComponent<TweenPosition>();
            m_tweener.onFinished.Clear();
            m_tweener.AddOnFinished(OnTweenFinish);
        }
        public override void StartMoveUp()
        {
            Vector3 localPos = transform.localPosition;
            m_tweener.from = new Vector3(localPos.x, -205, 0);
            m_tweener.to = new Vector3(localPos.x, -100, 0);

            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }

        public void OnTweenFinish()
        {
            if (onTweenFinishCB != null)
                onTweenFinishCB();
        }
        public override void StartMoveDown()
        {
            Vector3 localPos = transform.localPosition;
            m_tweener.from = new Vector3(localPos.x, -205, 0);
            m_tweener.to = new Vector3(localPos.x, -305, 0);

            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }
    }
}