using System;
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
        /// Play ok se
        /// </summary>
        public static Action PlayOkSe;

        /// <summary>
        /// Play cancel se
        /// </summary>
        public static Action PlayCancelSe;
        
        /// <summary>
        /// SeType enum
        /// </summary>
        private enum SeType
        {
            Ok,
            Cancel,
        }

        /// <summary>
        /// SeType
        /// </summary>
        [SerializeField]
        private SeType seType;
        
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
            longPressHandler.Initialize();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            OnClick();
        }

        private void Update()
        {
            if (_isPressingButton)
            {
                longPressHandler.CountDownLongPress();   
            }
        }

        private void OnClick()
        {
            PlaySe();
        }

        private void PlaySe()
        {
            switch (seType)
            {
                case SeType.Ok:
                    PlayOkSe?.Invoke();
                    break;
                case SeType.Cancel:
                    PlayCancelSe?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}