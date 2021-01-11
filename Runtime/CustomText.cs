using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomText
    /// </summary>
    [AddComponentMenu("CustomUI/CustomText", order:20)]
    public class CustomText : Text
    {
        /// <summary>
        /// FontType
        /// </summary>
        public enum TextType
        {
            Normal,
            Bold,
            Warn,
            Attention,
            Special,
        }

        /// <summary>
        /// 文字のタイプ
        /// </summary>
        [SerializeField]
        private TextType textType;

        /// <summary>
        /// Awake
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            if (Application.isPlaying)
            {
                SetupCustomText();
            }
            else
            {
                SetupCustomTextInEditorMode();
            }
        }

        /// <summary>
        /// Setup CustomText parameter according to textType
        /// </summary>
        private void SetupCustomText()
        {
            if (WordManager.Instance == null)
            {
                if (Application.isPlaying)
                {
                    Debug.LogError("WordManager is not found. You need to place one WordManager.");
                }
                else
                {
                    Debug.LogWarning("WordManager is not found. You need to place one WordManager.");
                }
                return;
            }
            var textTypeData = WordManager.Instance.GetTextTypeData(textType);
            font = textTypeData.FontData;
            fontSize = textTypeData.TextSize;
            color = textTypeData.TextColor;
        }
        
        #if UNITY_EDITOR
        private void SetupCustomTextInEditorMode()
        {
            WordManager.Instance.LoadWordsData();
            var textTypeData = WordManager.Instance.GetTextTypeData(textType);
            font = textTypeData.FontData;
            fontSize = textTypeData.TextSize;
            color = textTypeData.TextColor;
        }
        #endif
    }
}