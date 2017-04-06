using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow {

    //Rect myRect = new Rect(10, 10, 100, 100);

    List<Rect> myWindows = new List<Rect>();

    [MenuItem("Window/Node Editor")]
	public static void GetWindow()
    {
        EditorWindow.GetWindow<NodeEditor>();
    }

    void OnGUI()
    {
        if(myWindows.Count > 1)
        {
            DrawNodeConnection(myWindows[0], myWindows[1]);
        }
        if(GUILayout.Button("Add Node"))
        {
            myWindows.Add(new Rect(10, 20, 100, 100));
        }
        //begins a new window in the window that we are drawing
        BeginWindows();
        for(int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i] = GUI.Window(i, myWindows[i], DrawWindow, "Window " + i);
        }
        //myRect = GUI.Window(0, myRect, DrawWindow, "Title");
        EndWindows();
    }

    void DrawWindow(int id)
    {
        Color temp = GUI.backgroundColor;
        GUI.backgroundColor = Color.red;
        if(GUI.Button(new Rect(myWindows[id].width-18,-1, 18, 18),"X"))
        {
            myWindows.RemoveAt(id);
        }
        GUI.backgroundColor = temp;
        GUI.DragWindow();
    }

    void DrawNodeConnection(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 2);
    }
}
