using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    public class UIFade : MonoBehaviour
    {
        [SerializeField]
        private float m_targetAlpha;
        [SerializeField]
        private float m_currentAlpha;
        private float m_initalAlpha;
        private float m_time;
        private float m_timer;
        private float m_delay;

        private CanvasRenderer m_canvasRenderer;
        private CanvasGroup m_canvasGroup;
        public void setData(float targetAlpha, float time, float delay)
        {
            m_targetAlpha = targetAlpha;
            m_time = time;
            m_delay = delay;
            m_canvasGroup = this.GetComponent<CanvasGroup>();
            m_initalAlpha = m_canvasGroup.alpha;
            m_currentAlpha = m_initalAlpha;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //ADD DELAY
            if (m_timer > (m_time + m_delay))
            {
                //m_canvasRenderer.SetAlpha(m_targetAlpha);
                m_canvasGroup.alpha = m_targetAlpha;
                //Make colour set to target
                Destroy(this);
            }
            //Wait till delay
            if (m_timer > m_delay)
            {
                m_currentAlpha = Mathf.Lerp(m_initalAlpha, m_targetAlpha, (m_timer - m_delay) / (m_time));
                m_canvasGroup.alpha = m_currentAlpha;
            }

            m_timer += Time.deltaTime;

        }
    }
}
