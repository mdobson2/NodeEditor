using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBaseClass : MonoBehaviour {

    public int id;
    public Rect rect;
    public string title;

    public delegate void voidNodeFunction(NodeBaseClass node); //declaration of what the delegate is
    public voidNodeFunction CloseFunction;

    public List<NodeBaseClass> linkedNodes = new List<NodeBaseClass>();

    public NodeBaseClass(Rect r, int i)
    {
        rect = r;
        id = i;
        title = "";
    }

    public NodeBaseClass(Rect r, int Id, string Title)
    {
        rect = r;
        id = Id;
        title = Title;
    }

    public void BaseDraw(int winID)
    {
        Color temp = GUI.backgroundColor;
        GUI.backgroundColor = Color.red;

        if (GUI.Button(new Rect(rect.width - 18, -1, 18, 18), "X"))
        {
            CloseFunction(this);
        }
        GUI.backgroundColor = temp;
        GUI.DragWindow();
    }
    
    public virtual void DrawGUI(int winID)
    {
        GUILayout.Label("Node: " + id);
        BaseDraw(winID);
    }

    public virtual void AttachNode(NodeBaseClass node)
    {
        linkedNodes.Add(node);
    }
}
