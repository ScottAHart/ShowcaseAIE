using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace CUI
{
    [System.Serializable]
    public struct StateActions
    {
        public Tween[] m_tweens;
        public float m_delay;//Tweens get delay for each position in the list, first 0 delay, 2nd 1xdelay, 3rd 2xdelay.. 
        public bool m_squence; //Play tweens in a sequence., first 0 delay, 2nd 1xdelay+previous delay, 3rd 2xdelay + all previous delay. 
        public UnityEvent m_event; //fire off event when done
    }

    public interface ICUIElement
    {
        float Enable();//When you want to enable (call this)
        float Disable();//When you want to diabled (call this)
        
    }

    

    public static class CUIFunctions
    {
        public static float PlayAction(StateActions action, GameObject gameObject)
        {
            float duration = 0;
            float delay = 0;
            
            foreach (Tween t in action.m_tweens)
            {
                if (action.m_squence)
                {
                    if (t != null)
                    {
                        t.Apply(gameObject, duration + delay);
                        duration += t.GetDuration();
                    }
                }
                else
                {
                    t.Apply(gameObject, delay);

                    if (t.GetDuration() + delay > duration)
                        duration = t.GetDuration() + delay;

                    delay += delay;
                }
            }
            action.m_event.Invoke();

            return duration;
        }
        //Play this one on the element
        public static float PlayTween(Tween tween, GameObject gameObject)
        {
            tween.Apply(gameObject);
            return tween.GetDuration();
        }

        public static IEnumerator DisableAfter(float seconds, GameObject gameObject)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}
