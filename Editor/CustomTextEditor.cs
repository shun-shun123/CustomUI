using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomText Editor
    /// </summary>
    [CustomEditor(typeof(CustomText))]
    public class CustomTextEditor : Editor
    {
        /// <summary>
        /// Create New CustomText
        /// </summary>
        [MenuItem("GameObject/CustomUI/Custom Text", false, 10)]
        public static void CreateCustomText()
        {
            CreateCustomUi.CreateNewUi<CustomText>();
        }
    }
}