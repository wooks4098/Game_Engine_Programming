﻿using UnityEditor;
using UnityEngine;

namespace MalbersAnimations
{ 
    public class MalbersMenu : EditorWindow
    {
        const string URPL_Shader_Path = "Assets/Malbers Animations/Common/Shaders/URP_MalbersShaders.unitypackage";
        const string HRPL_Shader_Path = "Assets/Malbers Animations/Common/Shaders/HDRP_MalbersShaders 2019 LTS.unitypackage";
        const string HRPL21_Shader_Path = "/Malbers Animations/Common/Shaders/HDRP_MalbersShaders 2020.unitypackage";

        [MenuItem("Tools/Malbers Animations/Upgrade Malbers shaders to URP", false, 0)]
        public static void UpgradeMaterialsURPL() => AssetDatabase.ImportPackage(URPL_Shader_Path, true);

        [MenuItem("Tools/Malbers Animations/Upgrade Malbers shaders to HDRP 2019 LTS", false, 0)]
        public static void UpgradeMaterialsHRPL() => AssetDatabase.ImportPackage(HRPL_Shader_Path, true);

        [MenuItem("Tools/Malbers Animations/Upgrade Malbers shaders to HDRP 2020+", false, 0)]
        public static void UpgradeMaterialsHRPL2020() => AssetDatabase.ImportPackage(HRPL21_Shader_Path, true);



        [MenuItem("Tools/Malbers Animations/Create Test Plane", false, 2)]
        public static void CreateTestPlane()
        {
            var TestPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            TestPlane.transform.localScale = new Vector3(20, 1, 20);
            TestPlane.GetComponent<MeshRenderer>().sharedMaterial = AssetDatabase.LoadAssetAtPath("Assets/Malbers Animations/Common/Shaders/Ground_20.mat", typeof(Material)) as Material;
        }


        [MenuItem("Tools/Malbers Animations/Integrations", false, 2)]
        public static void OpenIntegrations() => Application.OpenURL("https://malbersanimations.gitbook.io/animal-controller/annex/integrations");


    }

        [CreateAssetMenu()]
        public class PrefabReferenceFixer : ScriptableObject
        {
            [MenuItem("Assets/Force Reserialize")]
            private static void ForceReserialize()
            {
                GameObject[] selection = Selection.gameObjects;
                string[] objectPaths = new string[selection.Length];

                for (int i = 0; i < selection.Length; ++i)
                {
                    objectPaths[i] = AssetDatabase.GetAssetPath(selection[i]);
                }

                AssetDatabase.ForceReserializeAssets(objectPaths);
            }
        }
}
