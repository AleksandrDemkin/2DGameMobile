using System;
using UnityEngine;

namespace Views
{
    public class FollowMouse : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = position;
        }
    }
}