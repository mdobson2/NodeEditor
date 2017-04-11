using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class ReflectionExample : EditorWindow {

    public GameObject go;
    public Component targetComp;

    public int compSelected = 0;
    public int methodSelected = 0;

    List<Component> comps = new List<Component>();
    List<string> compNames = new List<string>();
    List<MethodInfo> methods = new List<MethodInfo>();
    List<string> methodNames = new List<string>();

    [MenuItem("Window/Reflect")]
	public static void ShowWindow()
    {
        GetWindow<ReflectionExample>();
    }

    public void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        go = (GameObject)EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        if(EditorGUI.EndChangeCheck())
        {
            compNames.Clear();

            comps = new List<Component>(go.GetComponents(typeof(Component)));
            foreach(Component co in comps)
            {
                compNames.Add(co.GetType().Name);
            }
        }
        if(go != null)
        {
            if(compNames.Count > 0)
            {
                EditorGUI.BeginChangeCheck();
                compSelected = EditorGUILayout.Popup(compSelected, compNames.ToArray());
                if(EditorGUI.EndChangeCheck())
                {
                    methodNames.Clear();
                    targetComp = comps[compSelected];
                    BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

                    methods = new List<MethodInfo>(targetComp.GetType().GetMethods(flags));
                    foreach(MethodInfo info in methods)
                    {
                        methodNames.Add(info.Name);
                    }
                }
            
                if(methodNames.Count > 0)
                {
                    methodSelected = EditorGUILayout.Popup(methodSelected, methodNames.ToArray());
                    if (GUILayout.Button("Go, Baby, Go"))
                    {
                        methods[methodSelected].Invoke(targetComp, null);
                        
                    }
                }
            }
        }
    }
}
