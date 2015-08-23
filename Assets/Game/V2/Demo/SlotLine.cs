using UnityEngine;
using System.Collections;

namespace Game5000
{
    public class SlotLine : Rodger.SlotLineBase
    {

        TweenPosition m_tweener;

        public VOIDCB m_TweenFinishCB;
        protected override  void Awake()
        {
            base.Awake();
            
            m_tweener = _GetComponent<TweenPosition>();
            m_tweener.onFinished.Clear();
            m_tweener.AddOnFinished(OnTweenFinish);
        }
        public override void StartMoveUp()
        {
            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }

        public void OnTweenFinish()
        {
            if (onStopCB != null)
                onStopCB();
        }
        public override void StartMoveDown()
        {
            
        }
    }
}