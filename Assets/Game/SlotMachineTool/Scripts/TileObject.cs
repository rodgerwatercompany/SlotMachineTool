using UnityEngine;
using System.Collections;


namespace Rodger
{
    public class TileObject : MonoBehaviour
    {

        private UISprite uisprite;

        // Use this for initialization
        void Awake()
        {
            uisprite = gameObject.GetComponent<UISprite>();
        }        
        public void SetPosition(float pos_y)
        {
            gameObject.transform.localPosition = new Vector3(0.0f, pos_y, 0.0f);
        }
        public void SetSprite(string name)
        {
            uisprite.spriteName = name;
        }
        public void SetAlpha(float value)
        {
            uisprite.alpha = value;
        }
        public void SetChild(bool sw)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(sw);
        }

    }
}