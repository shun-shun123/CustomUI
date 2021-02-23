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
        [MenuItem(CreateCustomUi.MENU_ITEM_NAME + "/Custom Text", false, 10)]
        public static void CreateCustomText()
        {
            CreateCustomUi.CreateNewUi<CustomText>();
        }

        /// <summary>
        /// Create WordManager
        /// </summary>
        [MenuItem(CreateCustomUi.MENU_ITEM_NAME + "/WordManager", false, 10)]
        public static void CreateWordManager()
        {
            if (WordManager.Instance == null)
            {
                var wordManager = new GameObject("WordManager", typeof(WordManager));
            }
            else
            {
                Debug.LogError("WordManager is already created.");
            }
        }
    }
}