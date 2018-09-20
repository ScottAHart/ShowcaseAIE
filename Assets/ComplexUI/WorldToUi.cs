using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CUI
{
    public class WorldToUi : MonoBehaviour
    {
        public RectTransform m_UITargetRect;
        public RectTransform m_cameraPlane;
        private Vector3 m_startPos;
        private Vector3 m_targetPos;

        [SerializeField] private GameObject m_worldCanvas;
        
        [SerializeField] private float m_lerpTime = 2;
        private float m_timer = 0;

        [SerializeField] private UnityEvent m_onHit;
        // Use this for initialization
        void Start()
        {
            if (m_cameraPlane == null)
            {
                //See if camera already has one
                m_cameraPlane = Camera.main.gameObject.GetComponentInChildren<RectTransform>();
                //If it doesnt have one add one
                if (m_cameraPlane == null)
                {
                    m_cameraPlane = Instantiate<GameObject>(m_worldCanvas, Camera.main.transform).GetComponent<RectTransform>();
                }
            }
            
            m_startPos = transform.position;            
            //RectTransformUtility.ScreenPointToWorldPointInRectangle(canvRect, (Vector2)canvRect.position, Camera.main, out m_targetPos);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_cameraPlane, (Vector2)m_UITargetRect.position, Camera.main, out m_targetPos);

        }

        // Update is called once per frame
        void Update()
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_cameraPlane, (Vector2)m_UITargetRect.position, Camera.main, out m_targetPos);
            transform.position = Vector3.Lerp(m_startPos, m_targetPos, m_timer / m_lerpTime);
            m_timer += Time.deltaTime;
            if (transform.position == m_targetPos)
                Completed();
        }

        private void Completed()
        {
            m_onHit.Invoke();
            Destroy(gameObject);
        }


        
    }
}