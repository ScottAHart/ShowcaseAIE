using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CUI
{
   
    // [RequireComponent(typeof(CanvasGroup))]
    //Extends Unity's button to be able to apply tweens on actions
    public class CUIButton : Button, ICUIElement
    {
        public StateActions m_onOver;
        public StateActions m_onExit;
        public StateActions m_onDown;
        public StateActions m_onUp;
        public StateActions m_onEnabled; //Make own enable/disable functions that do enable/disable after tweens? 
        public StateActions m_onDisabled;

        private bool m_enabled;
        protected override void Awake()
        {
            m_enabled = enabled;
        }

        protected override void OnDisable()
        {
            m_enabled = false;
            base.OnDisable();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            m_enabled = true;
        }
        //On Over
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerEnter(eventData);
                CUIFunctions.PlayAction(m_onOver, this.gameObject);
                //PlayAction(m_onOver);
            }   
        }

        //On Exit
        public override void OnPointerExit(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerExit(eventData);
                CUIFunctions.PlayAction(m_onExit, this.gameObject); ;
            }
        }

        //On down
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerDown(eventData);
                CUIFunctions.PlayAction(m_onDown, this.gameObject);
            }
        }
        //On up
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (m_enabled)
            {
                base.OnPointerUp(eventData);
                CUIFunctions.PlayAction(m_onUp, this.gameObject);
            }
        }
        //Enable
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
        
        //My disable function 
        public float Disable()
        {
            if (m_enabled)
            {
                m_enabled = false;
                //Use return value from play actions being called to start coroutine
                float time = CUIFunctions.PlayAction(m_onDisabled, this.gameObject);
                StartCoroutine(CUIFunctions.DisableAfter(time,this.gameObject));
                return time;
            }
            return 0;
        }


        //private float PlayAction(StateActions action)
        //{
        //    float delay = 0;
        //    foreach (Tween t in action.m_tweens)
        //    {
        //        if (t != null)
        //            t.Apply(this.gameObject, delay);
        //        delay += action.m_delay + (action.m_squence ? t.GetDuration() : 0);
        //    }
        //    action.m_event.Invoke();

        //    return delay;
        //}
    }
}
