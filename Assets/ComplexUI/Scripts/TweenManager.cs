using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CUI {
    public class TweensManager : ScriptableObject {
        private static TweensManager m_instance = null;
        private List<Tween> m_tweens;
        public static TweensManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = CreateInstance<TweensManager>();
                }
                return m_instance;
            }
        }

        private void Start()
        {
           
        }

        public List<Tween> GetTweens()
        {
            m_tweens = new List<Tween>(Resources.LoadAll<Tween>("Tweens"));
            return m_tweens;
        }

        public void AddTween(Tween t)
        {
            m_tweens.Add(t);
        }

    }
}
