using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow {

    //Rect myRect = new Rect(10, 10, 100, 100);

    List<NodeBaseClass> myWindows = new List<NodeBaseClass>();

    [MenuItem("Window/Node Editor")]
	public static void GetWindow()
    {
        EditorWindow.GetWindow<NodeEditor>();
    }

    void OnGUI()
    {
        for(int i = 0; i < myWindows.Count; i++)
        {
            for(int j = 0; j < myWindows[i].linkedNodes.Count; j++)
            {
                DrawNodeConnection(myWindows[i].rect, myWindows[i].linkedNodes[j].rect);
            }
        }

        BeginWindows();

        if(GUILayout.Button("Add Node"))
        {
            myWindows.Add(new NodeBaseClass(new Rect(10, 10, 100, 100), myWindows.Count));
            myWindows[myWindows.Count - 1].CloseFunction = RemoveNode;
            if(myWindows.Count > 1)
            {
                myWindows[myWindows.Count - 2].AttachNode(myWindows[myWindows.Count - 1]);
            }
        }
        //begins a new window in the window that we are drawing
        for(int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].rect = GUI.Window(i, myWindows[i].rect, myWindows[i].DrawGUI , myWindows[i].title);
        }

        EndWindows();
    }

    void DrawNodeConnection(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 2);
    }

    public void RemoveNode(NodeBaseClass node)
    {
        for(int i = 0; i < myWindows.Count; i++)
        {
            for(int j = 0; j < myWindows[i].linkedNodes.Count; j++)
            {
                if(myWindows[i].linkedNodes[j].id == node.id)
                {
                    myWindows[i].linkedNodes.RemoveAt(j);
                }
            }
        }
        myWindows.RemoveAt(node.id);
        ReassignNodes();
    }

    public void ReassignNodes()
    {
        for(int i = 0; i < myWindows.Count; i++)
        {
            myWindows[i].id = i;
        }
    }
}
