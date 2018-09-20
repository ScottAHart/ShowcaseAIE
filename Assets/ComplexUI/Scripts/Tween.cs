using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CUI
{
    [System.Serializable]
    public abstract class Tween : ScriptableObject
    {
        public abstract void Apply(GameObject obj);
        public abstract void Apply(GameObject obj, float delay);

        public abstract float GetDelay();
        public abstract float GetTime();
        public float GetDuration()
        {
            return GetDelay() + GetTime();
        }
        
    }
}
