using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SchwerEditor {
    public class AnimationRelinker : EditorWindow {
        private const string _name = "Animation Relinker";

        private static AnimatorController controller;
        private static GameObject originalGameObject;
        private static string attribute = "m_Sprite";

        [MenuItem("Window/" + _name)]
        public static void ShowWindow() => GetWindow<AnimationRelinker>(_name);

        private void OnGUI() {
            controller = (AnimatorController)EditorGUILayout.ObjectField("Animator Controller", controller, typeof(AnimatorController), false);
            originalGameObject = (GameObject)EditorGUILayout.ObjectField("Original Game Object", originalGameObject, typeof(GameObject), true);
            attribute = EditorGUILayout.TextField("Attribute", attribute);

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying || controller == null || originalGameObject == null || string.IsNullOrWhiteSpace(attribute));
            if (GUILayout.Button("Relink")) {
                Relink();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void Relink() {
            try {
                AssetDatabase.StartAssetEditing();
                foreach (var animation in controller.animationClips) {
                    var path = AssetDatabase.GetAssetPath(animation);
                    var lines = File.ReadAllLines(path);

                    for (int i = 0; i + 1 < lines.Length; i++) {
                        if (lines[i].Contains($"attribute: {attribute}")) {
                            if (lines[i + 1].TrimStart() == "path: ") {
                                lines[i + 1] += originalGameObject.name;
                            }
                        }
                    }

                    File.WriteAllLines(path, lines);
                }
            }
            finally {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}
