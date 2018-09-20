using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Script to attach tweens to objects that can be played using variables or calling the Play Var
namespace CUI
{
    public class TweenAttach : MonoBehaviour
    {
        public Tween m_tween;
        public bool m_nextUpdate;
        public bool m_onStart;
        public bool m_destroyOnPlay;
        public UnityEvent m_event;
        // Use this for initialization
        void Start()
        {
            if (m_onStart)
            {
                PlayTween();
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_nextUpdate)
            {
                PlayTween();
                m_nextUpdate = false;
            }
        }

        public void PlayTween()
        {
            if (m_tween != null)
            {
                m_tween.Apply(this.gameObject);
                m_event.Invoke();
                if (m_destroyOnPlay)
                    Destroy(this);
            }
        }
        //Also if they can to just apply one now
        public static void PlayTween(GameObject obj, Tween tween)
        {
            tween.Apply(obj);
        }
    }
}
