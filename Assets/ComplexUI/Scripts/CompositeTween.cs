using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUI
{
    //Applies multi tweens to an object on the apply call
    [CreateAssetMenu(fileName = "Tween", menuName = "ComplexUI/Tween/Composite Tween", order = 0)]
    public class CompositeTween : Tween
    {
        [SerializeField] private Tween[] m_tweens;
        [Tooltip("Put delay between them")]
        [SerializeField] private float m_delayBetween;
        [Tooltip("Play one after the other")]
        [SerializeField] private bool m_sequence;
        public override void Apply(GameObject obj)
        {
            Apply(obj, 0);
        }

        public override void Apply(GameObject obj, float delay)
        {
            float totalDelay = 0;
            foreach (Tween t in m_tweens)
            {
                if (t != null && t != this)
                    t.Apply(obj, totalDelay);
                totalDelay += delay;
                if (m_sequence)
                {
                    totalDelay += t.GetDuration();
                }
            }
        }
        //Different
        public override float GetDelay()
        {
            return m_delayBetween;
        }

        public override float GetTime()
        {
            float time = 0;
            if (m_sequence)
            {
                foreach (Tween t in m_tweens)
                {
                    time += t.GetDuration();
                    time += m_delayBetween;
                }
                time -= m_delayBetween; // last one wont have delay
                    
            }
            else
            {
                int index = 0;
                int i = 0;
                foreach(Tween t in m_tweens)
                {
                    if(t.GetDuration() + i * m_delayBetween > time)
                    {
                        time = t.GetDuration();
                        index = i;
                    }
                    i++;
                }
                time += index * m_delayBetween;
            }
            return time;
        }
    }
}
