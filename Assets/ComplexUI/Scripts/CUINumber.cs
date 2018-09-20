using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CUI
{
    public class CUINumber : MonoBehaviour, ICUIElement
    {
        [SerializeField] private float m_value;
        [Tooltip("int = 0, float = 0.000")]
        [SerializeField] private string m_format;
        private Text m_text;

        [SerializeField] StateActions m_increaseValue;
        [SerializeField] StateActions m_decreaseValue;
        [SerializeField] StateActions m_onEnabled;
        [SerializeField] StateActions m_onDisabled;

        private bool m_enabled;

        private void Awake()
        {
            m_text = GetComponentInChildren<Text>();
            m_text.text = m_value.ToString(m_format);
        }
        void Start()
        {
           
        }
        //
        private void OnDisable()
        {
            m_enabled = false;
        }
        private void OnEnable()
        {
            m_enabled = true;
        }
        //Change by amount
        public void ChangeValue(float amount)
        {
            m_value += amount;
            if(amount > 0)
                CUIFunctions.PlayAction(m_increaseValue, this.gameObject);
            else if (amount < 0)
                CUIFunctions.PlayAction(m_decreaseValue, this.gameObject);
            m_text.text = m_value.ToString(m_format);
        }

        public void SetValue(float value)
        {
            if(m_value > value)
                CUIFunctions.PlayAction(m_decreaseValue, this.gameObject);
            else if (m_value < value)
                CUIFunctions.PlayAction(m_increaseValue, this.gameObject);
            m_value = value;
            m_text.text = m_value.ToString(m_format);
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
    }
}
