using UnityEngine;
using UnityEngine.UI;

namespace Mummy.CustomUI.Model
{
    /// <summary>
    /// CustomText data
    /// </summary>
    public class TextTypeData
    {
        public readonly Font FontData;

        public readonly int TextSize;

        public readonly Color TextColor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fontData">FontData</param>
        /// <param name="textSize">TextSize</param>
        /// <param name="textColor">TextColor</param>
        public TextTypeData(Font fontData, int textSize, Color textColor)
        {
            FontData = fontData;
            TextSize = textSize;
            TextColor = textColor;
        }
    }
}