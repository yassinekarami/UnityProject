using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CameraPlayer
{
    public class CursorAffordance : MonoBehaviour
    {
        public Texture2D attack;
        public Texture2D move;
        public Texture2D forbidden;
        public Texture2D health;
        public Texture2D pistol;
        public Texture2D coin;

        CursorMode CursorMode = CursorMode.Auto;

        RaycastHit hit;
        Ray ray;
        int layer;

        // Update is called once per frame
        void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500))
            {
                layer = hit.transform.gameObject.layer;
                switch (layer)
                {
                    case 8: // Ground layer ==> 8
                        Cursor.SetCursor(move, Vector2.zero, CursorMode);
                        break;
                    case 9: // Enemy layer ==> 9
                        Cursor.SetCursor(attack, Vector2.zero, CursorMode);
                        break;
                    case 10: // Health layer ==> 9
                        Cursor.SetCursor(health, Vector2.zero, CursorMode);
                        break;
                    case 11: // Pistol layer ==> 9
                        Cursor.SetCursor(pistol, Vector2.zero, CursorMode);
                        break;
                    case 12:
                        Cursor.SetCursor(coin, Vector2.zero, CursorMode);
                        break;
                    default:
                        Cursor.SetCursor(forbidden, Vector2.zero, CursorMode);
                        break;
                }
            }
        }

    }
}

