using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Generator)),CanEditMultipleObjects]
public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
            Generator gen = (Generator)target;
            if( DrawDefaultInspector())
            {
                if(gen.Autoupdate)
                {
                    gen.Generisi();
                }
            }
            if(GUILayout.Button("Generisi mapu"))
            {
                gen.Generisi();
            }
    }
}
