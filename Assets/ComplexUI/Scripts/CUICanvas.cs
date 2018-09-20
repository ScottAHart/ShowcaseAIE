using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    public class CUICanvas : MonoBehaviour
    {
        bool m_enabled;
        Canvas m_canvas;

        ICUIElement[] childrenElements = new ICUIElement[0];
        // Use this for initialization
        void Start()
        {
            m_canvas = GetComponent<Canvas>();
            m_enabled = enabled;
            childrenElements = GetComponentsInChildren<ICUIElement>();
        }

        private void Awake()
        {
            childrenElements = GetComponentsInChildren<ICUIElement>();
        }
        private void OnDisable()
        {
            m_enabled = false;
        }
        private void OnEnable()
        {
            m_enabled = true;
        }
        // Update is called once per frame
        void Update()
        {

        }

        public float Disable()
        {
            if (m_enabled)
            {
                m_enabled = false;
                //ICUIElement[] childrenElements = GetComponentsInChildren<ICUIElement>();
                float time = 0;
                foreach (ICUIElement element in childrenElements)
                {
                    float duration = element.Disable();
                    if (time < duration)
                        time = duration;
                }
                StartCoroutine(CUIFunctions.DisableAfter(time, this.gameObject));
                return time;
            }
            return 0;
        }

        public float Enable()
        {
            if (m_enabled == false)
            {
                m_enabled = true;
                gameObject.SetActive(true);
               // ICUIElement[] childrenElements = GetComponentsInChildren<ICUIElement>();
                float time = 0;
                foreach (ICUIElement element in childrenElements)
                {

                    time += element.Enable();
                }
                return time;
            }
            return 0;
        }
    }
}
