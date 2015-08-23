using UnityEngine;
using System.Collections;

namespace Rodger
{

    public abstract class SlotmachineBase : MonoBehaviour
    {

        public SlotLineBase[] m_slotLines;

        bool m_Moving;
        bool m_Breaking;

        protected bool m_WaitCB;

        protected void Listener()
        {
            m_WaitCB = false;
        }
        
        protected void StartRun()
        {
            StartCoroutine(DoStartRun());
        }
        protected abstract IEnumerator DoStartRun();
        
        protected void StartStop()
        {
            StartCoroutine(DoStartStop());
        }
        protected abstract IEnumerator DoStartStop();
        protected void StartFastStop()
        {
            
        }
    }
}