using UnityEngine;
using System.Collections;

public class TweenControl : MonoBehaviour {

    public UITweener m_tweener;

    void OnGUI()
    {
        if(GUILayout.Button("Play Tween"))
        {
            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }
    }
}
