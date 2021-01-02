using UnityEngine;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomText
    /// </summary>
    [AddComponentMenu("CustomUI/CustomText")]
    public class CustomText : Text
    {
        /// <summary>
        /// FontType
        /// </summary>
        public enum FontType
        {
            Normal,
            Bold,
            Warn,
            Attention,
            Special,
        }
    }
}