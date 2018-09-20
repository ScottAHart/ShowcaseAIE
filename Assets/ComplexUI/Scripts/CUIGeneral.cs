using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CUI
{
    public class CUIGeneral : MonoBehaviour, ICUIElement
    {

        private bool m_enabled;
        public StateActions m_onEnabled;
        public StateActions m_onDisabled;

        private void OnDisable()
        {
            m_enabled = false;
        }
        private void OnEnable()
        {
            m_enabled = true;
        }

        public float Disable()
        {
            if (m_enabled)
            {
                m_enabled = false;
                //Use return value from play actions being called to start coroutine
                float time = CUIFunctions.PlayAction(m_onDisabled, this.gameObject);
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
                return CUIFunctions.PlayAction(m_onEnabled, this.gameObject);
            }
            return 0;
        }
    }
}
