using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(TowerGenerator))]
public class TowerGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        TowerGenerator towerGenerator = (TowerGenerator) target;
        
        if (DrawDefaultInspector()) {
           
        }
        
        if (GUILayout.Button("Generate")) {
            towerGenerator.GenerateTower();
        }
        
    }

}
