using Mummy.CustomUI.Module;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// CustomButton
    /// </summary>
    [AddComponentMenu("CustomUI/CustomButton", 20)]
    public class CustomButton : Button
    {
        /// <summary>
        /// longPressHandler
        /// </summary>
        [SerializeField]
        private LongPressHandler longPressHandler;

        /// <summary>
        /// isPressingButton flag
        /// </summary>
        private bool _isPressingButton;

        /// <summary>
        /// LongPressHandler
        /// </summary>
        public LongPressHandler.OnLongPressed OnLongPressed
        {
            get => longPressHandler.OnLongPressedHandler;
            set => longPressHandler.OnLongPressedHandler = value;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            // check for mouse control
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _isPressingButton = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            // check for mouse control
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _isPressingButton = false;
        }

        private void Update()
        {
            if (_isPressingButton)
            {
                longPressHandler.CountDownLongPress();   
            }
        }
    }
}