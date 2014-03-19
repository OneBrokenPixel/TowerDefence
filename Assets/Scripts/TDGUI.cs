using UnityEngine;
using System.Collections;

public class TDGUI : MonoBehaviour
{

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - Screen.width / 5 + 5, 10, Screen.width / 5 - 25, Screen.height - 20), "Towers");


        if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
        {
            print("Button pressed");
        }
    }
}
