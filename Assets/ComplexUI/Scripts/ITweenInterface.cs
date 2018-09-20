using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;

namespace CUI
{

    public delegate void SetInfoDel(ITweenInterface tween);

    [CreateAssetMenu(fileName = "Tween", menuName = "ComplexUI/Tween/New Tween", order = 0)]
    public class ITweenInterface : Tween
    {
        [SerializeField]
        public enum Type
        {
            ShakePos,
            ShakeScale,
            ShakeRot,
            ScaleTo,
            RotateTo,
            MoveTo, //Look into other parameters
            ScaleAdd,
            RotateAdd,
            MoveAdd,
            FadeTo, //Alpha fade
            ClearAll
        }
        public Type m_type;

        public string m_name = "Tween"; //Name of tween
        public Vector3 m_amount = new Vector3(0, 0, 0); //Change amount or Target depends on tween
        public float m_time = 1.0f; //Time of tween
        public float m_delay = 0.0f;//Delay before tween
        public bool m_local = true;//Local coordinates n stuff
        public Space m_space = Space.Self;//Local
        public iTween.LoopType m_loopType;
        public bool m_useEase; //TO DO
        public iTween.EaseType m_easeType;
        public float m_alpha;//Used in FadeTo 

        /*
        #region Menu&Creation
        public void CreateAsset()
        {
            CreateAsset("Tween", null);
        }
        [ContextMenu("Something")]
        public void AddSomething()
        {
            m_time = -1.0f;
            AssetDatabase.Refresh();
        }
        #region ShakeRotation
        [MenuItem("Assets/Create/ComplexUI/Tween/ShakeRotation")]
        public static void MakeShakeRot()
        {
            SetInfoDel pointer = new SetInfoDel(SetShakeRot);
            CreateAsset("ShakeRotation", pointer);

        }
        [ContextMenu("Shake Rotation")]
        public void SetShakeRot()
        {
            SetShakeRot(this);
        }
        public static void SetShakeRot(ITweenInterface set)
        {
            set.m_type = Type.ShakeRot;
        }
        #endregion
        #region ShakePosition
        [MenuItem("Assets/Create/ComplexUI/Tween/ShakePosition")]
        public static void MakeShakePos()
        {
            SetInfoDel pointer = new SetInfoDel(SetShakePos);
            CreateAsset("ShakePosition", pointer);

        }
        [ContextMenu("Shake Position")]
        public void SetShakePos()
        {
            SetShakePos(this);
        }
        public static void SetShakePos(ITweenInterface set)
        {
            set.m_type = Type.ShakePos;
        }
        #endregion
        #region ShakeScale
        [MenuItem("Assets/Create/ComplexUI/Tween/ShakeScale")]
        public static void MakeShakeScale()
        {
            SetInfoDel pointer = new SetInfoDel(SetShakeScale);
            CreateAsset("ShakePosition", pointer);

        }
        [ContextMenu("Shake Position")]
        public void SetShakeScale()
        {
            SetShakeScale(this);
        }
        public static void SetShakeScale(ITweenInterface set)
        {
            set.m_type = Type.ShakeScale;
        }
        #endregion



        public static void CreateAsset(string name, SetInfoDel preset)
        {
            ITweenInterface asset = ScriptableObject.CreateInstance<ITweenInterface>();
            if (preset != null)
                preset(asset);

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

        }
        #endregion
        */
        public override void Apply(GameObject obj)
        {
            Apply(obj, 0);
        }

        public override void Apply(GameObject obj, float delay)
        {
            switch (m_type)
            {
                case Type.ShakeRot:
                    iTween.ShakeRotation(obj, iTween.Hash("name", m_name, "amount", m_amount, "space", m_space, "time", m_time, "delay", m_delay + delay, "looptype", m_loopType));
                    break;
                case Type.ShakePos:
                    iTween.ShakePosition(obj, iTween.Hash("name", m_name, "amount", m_amount, "islocal", m_local, "time", m_time, "delay", m_delay + delay, "looptype", m_loopType));
                    break;
                case Type.ShakeScale:
                    iTween.ShakeScale(obj, iTween.Hash("name", m_name, "amount", m_amount, "time", m_time, "delay", m_delay + delay, "looptype", m_loopType));
                    break;
                case Type.ScaleTo:
                    iTween.ScaleTo(obj, iTween.Hash("name", m_name, "scale", m_amount, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));
                    break;
                case Type.RotateTo:
                    iTween.RotateTo(obj, iTween.Hash("name", m_name, "rotation", m_amount, "islocal", m_local, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));
                    break;
                case Type.MoveTo:
                    iTween.MoveTo(obj, iTween.Hash("name", m_name, "position", m_amount, "islocal", m_local, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));//If local goes to anchor 
                    break;
                case Type.ScaleAdd:
                    iTween.ScaleAdd(obj, iTween.Hash("name", m_name, "amount", m_amount, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));
                    break;
                case Type.RotateAdd:
                    iTween.RotateAdd(obj, iTween.Hash("name", m_name, "amount", m_amount, "space", m_space, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));
                    break;
                case Type.MoveAdd:
                    iTween.MoveAdd(obj, iTween.Hash("name", m_name, "amount", m_amount, "space", m_space, "time", m_time, "delay", m_delay + delay, "easetype", m_easeType, "looptype", m_loopType));
                    break;
                case Type.FadeTo:
                    obj.AddComponent<UIFade>().setData(m_alpha, m_time, m_delay + delay);//My fade script
                    break;
                case Type.ClearAll:
                    iTween.Stop(obj);
                    break;
                default:
                    break;
            }
        }

        public override float GetDelay()
        {
            return m_delay;
        }

        public override float GetTime()
        {
            return m_time;
        }
    }
}
