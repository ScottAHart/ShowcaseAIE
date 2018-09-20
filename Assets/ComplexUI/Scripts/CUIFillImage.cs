using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Used for images that are used as fill images
namespace CUI
{
    [RequireComponent(typeof(Image))]
    public class CUIFillImage : MonoBehaviour, ICUIElement
    {
        private Image m_image;
        [SerializeField] private float m_amount;
        [SerializeField] private float m_maxValue = 1;
        [SerializeField] private float m_minValue = 0;

        public float Value
        {
            get
            {
                return m_amount;
            }
            set
            {
                if(value > m_maxValue)
                {
                    m_amount = m_maxValue;
                }
                else if(value < m_minValue)
                {
                    m_amount = m_minValue;
                }
                else
                {
                    m_amount = value;
                }
                m_image.fillAmount = (m_amount - m_minValue) / (m_maxValue - m_minValue);
                
            }
        }

        //lerp options
        [SerializeField] private float m_lerpTime;
        [SerializeField] private float m_targetValue;
        private float m_lerpTimer;

        private bool m_enabled;
        public StateActions m_onEnabled; 
        public StateActions m_onDisabled;

        public StateActions m_onIncrease;
        public StateActions m_onDecrease;


        // Use this for initialization
        void Start()
        {
            m_image = GetComponent<Image>();
            m_image.fillAmount = (m_amount - m_minValue) / (m_maxValue - m_minValue);
        }

        // Update is called once per frame
        void Update()
        {
            if (m_lerpTime > 0)
            {
                if (m_amount != m_targetValue)
                {

                    Value = Mathf.Lerp(Value, m_targetValue, m_lerpTimer / m_lerpTime);

                    m_lerpTimer += Time.deltaTime;
                }
                else
                {
                    m_lerpTimer = 0;
                }
            }
            else
            {
                Value = m_targetValue;
            }
        }

        //Seems to be causing frame issues
        //Could be crappy laptop
        IEnumerator LerpToOver(float value, float time)
        {
            
                float totalTime = 0;
                float current  = m_amount;
                while (totalTime < time)
                {
                    Value = Mathf.Lerp(current, value, (totalTime / time));
                    totalTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                
        }

        public void ChangeBy(float amount)
        {
            if (amount != 0)
            {
                m_lerpTimer = 0;
                if (amount > 0)
                {
                    CUIFunctions.PlayAction(m_onIncrease, this.gameObject);
                }
                else
                {
                    CUIFunctions.PlayAction(m_onDecrease, this.gameObject);
                }
                m_targetValue += amount;
            }
        }

        public void SetValue(float value)
        {
            if(value != m_targetValue)
            {
                m_lerpTimer = 0;
                if(value > m_targetValue)
                {
                   CUIFunctions.PlayAction(m_onIncrease, this.gameObject);   
                }
                else
                {
                    CUIFunctions.PlayAction(m_onDecrease, this.gameObject);
                }
                m_targetValue = value;
            }
            //StartCoroutine(LerpToOver(value, m_lerpTime));
        }

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

        public void SetMin(float min)
        {
            m_minValue = min;
        }

        public void SetMax(float max)
        {
            m_maxValue = max;
        }

    }

}