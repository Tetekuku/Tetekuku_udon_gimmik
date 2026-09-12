#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace Tetekuku
{
    /// <summary>
    /// StreetLight のセットアップを自動化するカスタムインスペクタ。
    /// 子オブジェクトから "light_fall" という名前を探して Light Fall に自動配線する。
    /// </summary>
    [CustomEditor(typeof(StreetLight))]
    public class StreetLightEditor : Editor
    {
        private const string LightFallChildName = "light_fall";

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            DrawDefaultInspector();

            EditorGUILayout.Space();
            if (GUILayout.Button("Light Fall を自動検索して配線"))
            {
                AutoWireLightFall((StreetLight)target);
            }
        }

        private void AutoWireLightFall(StreetLight streetLight)
        {
            Transform found = FindChildByName(streetLight.transform, LightFallChildName);
            if (found == null)
            {
                EditorUtility.DisplayDialog(
                    "Street Light",
                    $"子オブジェクトに \"{LightFallChildName}\" という名前の GameObject が見つかりませんでした。名前を確認するか、Light Fall を手動で設定してください。",
                    "OK");
                return;
            }

            SerializedObject serializedObject = new SerializedObject(streetLight);
            serializedObject.FindProperty("lightFall").objectReferenceValue = found.gameObject;
            serializedObject.ApplyModifiedProperties();

            UdonSharpEditorUtility.CopyProxyToUdon(streetLight);
        }

        private static Transform FindChildByName(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (string.Equals(child.name, name, StringComparison.OrdinalIgnoreCase)) return child;

                Transform nested = FindChildByName(child, name);
                if (nested != null) return nested;
            }

            return null;
        }
    }
}
#endif
