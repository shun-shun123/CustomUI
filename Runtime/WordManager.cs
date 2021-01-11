using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Mummy.CustomUI.Model;
using UnityEngine;

namespace Mummy.CustomUI
{
    /// <summary>
    /// WordManager controls CustomText data.
    /// </summary>
    public class WordManager : SingletonMonoBehaviour<WordManager>
    {
        /// <summary>
        /// Path to CustomTextSettings from Resources folder
        /// </summary>
        private static readonly string CustomTextSettingsPath = "CustomTextSettings";

        /// <summary>
        /// WordDict data path in Resourced folder
        /// </summary>
        public const string WordDictDataPath = "WordDict";
        
        /// <summary>
        /// TextType to TextData
        /// </summary>
        private readonly Dictionary<CustomText.TextType, TextTypeData> _textTypeToData = new Dictionary<CustomText.TextType, TextTypeData>();

        protected override void OnInitialize()
        {
            LoadWordsData();
        }

        /// <summary>
        /// Load WordBook data from Resources
        /// </summary>
        public void LoadWordsData()
        {
            try
            {
                var settings = Resources.Load<TextAsset>(CustomTextSettingsPath).ToString();
                var doc = XDocument.Parse(settings);
                var root = doc.Element("root");
                if (root == null)
                {
                    throw new InvalidDataException($"{CustomTextSettingsPath}.xml does not have root tag.");
                }

                foreach (CustomText.TextType textType in Enum.GetValues(typeof(CustomText.TextType)))
                {
                    var textTypeCompose = root.Element(textType.ToString());
                    if (textTypeCompose == null)
                    {
                        Debug.LogError($"{textType} setting is not written in {CustomTextSettingsPath}.xml");
                        continue;
                    }

                    var fontData = textTypeCompose.Element("FontData");
                    var textSize = textTypeCompose.Element("TextSize");
                    var colorData = textTypeCompose.Element("Color");
                    if (fontData == null || textSize == null || colorData == null)
                    {
                        Debug.LogError($"Something about textTypeComponents is missing.");
                        continue;
                    }

                    var font = Resources.Load<Font>(fontData.Value);
                    var size = int.Parse(textSize.Value);
                    var r = int.Parse(colorData.Element("R").Value);
                    var g = int.Parse(colorData.Element("G").Value);
                    var b = int.Parse(colorData.Element("B").Value);
                    var color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
                    
                    var textTypeData = new TextTypeData(font, size, color);
                    _textTypeToData[textType] = textTypeData;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"LoadWordsData throws exception: {ex}");
            }
        }

        /// <summary>
        /// Get TextTypeData by TextType
        /// </summary>
        /// <param name="textType">TextType</param>
        /// <returns>TextTypeData</returns>
        public TextTypeData GetTextTypeData(CustomText.TextType textType)
        {
            if (_textTypeToData.ContainsKey(textType))
            {
                return _textTypeToData[textType];
            }
            Debug.LogError($"{textType} is not found in textTypeToData");
            return null;
        }
    }
}