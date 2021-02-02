using UnityEditor;
using UnityEditor.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomButton Editor
    /// </summary>
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : SelectableEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            
            var longPressHandler = serializedObject.FindProperty("longPressHandler");
            EditorGUILayout.PropertyField(longPressHandler);

            var seType = serializedObject.FindProperty("seType");
            EditorGUILayout.PropertyField(seType);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}