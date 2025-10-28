using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GeneratorAbstractDung), true)]
public class RandDungGen : Editor
{


    GeneratorAbstractDung gen;

    private void Awake()
    {
        gen = (GeneratorAbstractDung)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Dung"))
        {
            gen.GenDung();
        }
    }
}
