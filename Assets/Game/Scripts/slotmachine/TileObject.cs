﻿using UnityEngine;
using System.Collections;

namespace Game5835
{
    public class TileObject : MonoBehaviour
    {

        private UISprite uisprite;

        // Use this for initialization
        void Start()
        {

            uisprite = gameObject.GetComponentInChildren<UISprite>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPosition(float pos_y)
        {
            gameObject.transform.localPosition = new Vector3(0.0f, pos_y, 0.0f);
        }

        public void SetSprite(string name)
        {
            uisprite.spriteName = name;
        }

    }
}