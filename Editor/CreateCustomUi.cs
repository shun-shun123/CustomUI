using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mummy.CustomUI
{
    /// <summary>
    /// Create CustomUI
    /// </summary>
    public class CreateCustomUi : Editor
    {
        /// <summary>
        /// MenuItemName
        /// </summary>
        public const string MENU_ITEM_NAME = "GameObject/CustomUI";
        
        /// <summary>
        /// Create new ui part
        /// </summary>
        /// <typeparam name="T">type of customUi</typeparam>
        public static void CreateNewUi<T>()
        {
            var selectObject = Selection.activeGameObject;
            if (selectObject == null)
            {
                var canvas = FindObjectOfType<Canvas>();
                if (canvas == null)
                {
                    canvas = CreateNewCanvas();
                }

                var eventSystem = FindObjectOfType<EventSystem>();
                if (eventSystem == null)
                {
                    eventSystem = CreateNewEventSystem();
                }

                CreateNewCustomUi<T>(canvas.transform);
                return;
            }

            CreateNewCustomUi<T>(selectObject.transform);
        }
        
        /// <summary>
        /// Create new canvas
        /// </summary>
        /// <returns>Canvas</returns>
        private static Canvas CreateNewCanvas()
        {
            var canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(CanvasRenderer));
            // Init canvas
            var canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 0;

            // Init canvasScaler 
            var canvasScaler = canvasObject.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;

            // Init GraphicRaycaster
            var graphicRaycaster = canvasObject.GetComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;
            graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
            
            return canvas;
        }

        /// <summary>
        /// Create new EventSystem
        /// </summary>
        /// <returns>EventSystem component</returns>
        private static EventSystem CreateNewEventSystem()
        {
            var eventSystemObject = new GameObject("EventSystem", typeof(EventSystem));
            var eventSystem = eventSystemObject.GetComponent<EventSystem>();
            return eventSystem;
        }

        /// <summary>
        /// Create new customUi
        /// </summary>
        /// <param name="parent">Parent transform</param>
        /// <typeparam name="T">type of customUi</typeparam>
        /// <returns>T</returns>
        private static T CreateNewCustomUi<T>(Transform parent)
        {
            var customUiObject = new GameObject(typeof(T).Name, typeof(T));
            var customUiCompo = customUiObject.GetComponent<T>();

            // SetParent
            customUiObject.transform.SetParent(parent);
            
            // Init Transform
            customUiObject.transform.localScale = Vector3.one;
            customUiObject.transform.localRotation = Quaternion.identity;
            
            return customUiCompo;
        }
    }
}