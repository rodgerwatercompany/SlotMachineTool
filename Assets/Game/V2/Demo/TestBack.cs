using UnityEngine;

public class TestBack : MonoBehaviour {

    public TweenPosition m_tweener;
    void OnGUI()
    {
        if (GUILayout.Button("StartRun UP"))
        {
            Vector3 localPos = transform.localPosition;
            m_tweener.from = new Vector3(localPos.x, -205, 0);
            m_tweener.to = new Vector3(localPos.x, -100, 0);

            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }
        if (GUILayout.Button("StartRun DW"))
        {
            Vector3 localPos = transform.localPosition;
            m_tweener.from = new Vector3(localPos.x, -205, 0);
            m_tweener.to = new Vector3(localPos.x, -305, 0);

            m_tweener.ResetToBeginning();
            m_tweener.PlayForward();
        }
    }
}
