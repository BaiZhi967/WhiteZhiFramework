using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace WhiteZhi
{
    /// <summary>
    /// Unity编辑器扩展
    /// </summary>
    public static class UnityExtension
    {
        /// <summary>
        /// 获取或添加组件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T">要获取的组件类型</typeparam>
        /// <returns>对应的组件</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        

        /// <summary>
        /// 贝塞尔移动动画
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="start">开始位置</param>
        /// <param name="mid">控制点位置</param>
        /// <param name="end">目标位置</param>
        /// <param name="time">动画时间</param>
        /// <param name="cnt">动画间隔数</param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static IEnumerator BezierAnimationCoroutine(this GameObject gameObject, Vector3 start, Vector3 mid, Vector3 end,
            float time, int cnt = 10,UnityAction callback = null)
        {
            
            Vector3[] vector3s = Utils.Bezier.GetBeizerList(start, mid, end, 10);
            gameObject.transform.position = start;
            foreach (var pos in vector3s)
            {
                gameObject.transform.DOMove(pos, time/cnt);
                yield return new WaitForSeconds(time/cnt);
            }
            callback?.Invoke();
            Canvas.ForceUpdateCanvases();
        }

        #region Transform 相关

        /// <summary>
        /// 重置 Transform
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="isLocal">是否为LocalTransform</param>
        public static void ResetTransform(this Transform tf, bool isLocal)
        {
            if (!isLocal)
            {
                tf.position = Vector3.zero;
                tf.localScale = Vector3.one;
                tf.rotation = Quaternion.identity;
            }
            else
            {
                tf.localPosition = Vector3.zero;
                tf.localScale = Vector3.one;
                tf.localRotation = Quaternion.identity;
            }
        }
        
        /// <summary>
        /// 重置 Position
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="isLocal">是否为LocalPosition</param>
        public static void ResetPosition(this Transform tf, bool isLocal)
        {
            if (!isLocal)
            {
                tf.position = Vector3.zero;
            }
            else
            {
                tf.localPosition = Vector3.zero;
            }
        }
        
        public static void ResetLocalScale(this Transform tf)
        {
            tf.localScale = Vector3.one;
        }

        public static void ResetLocalRotation(this Transform tf)
        {
            tf.localRotation = Quaternion.identity;
        }
        public static void ResetGlobalRotation(this Transform tf)
        {
            tf.rotation = Quaternion.identity;
        }

        public static void SetPositionX(this Transform tf, float x)
        {
            tf.position = new Vector3(x, tf.position.y, tf.position.z);
        }
        public static void SetPositionY(this Transform tf, float y)
        {
            tf.position = new Vector3(tf.position.x, y, tf.position.z);
        }
        public static void SetPositionZ(this Transform tf, float z)
        {
            tf.position = new Vector3(tf.position.x, tf.position.y, z);
        }
        public static void SetRotationX(this Transform tf, float x)
        {
            tf.rotation = Quaternion.Euler(x, tf.rotation.eulerAngles.y, tf.rotation.eulerAngles.z);
        }
        public static void SetRotationY(this Transform tf, float y)
        {
            tf.rotation = Quaternion.Euler(tf.rotation.eulerAngles.x, y, tf.rotation.eulerAngles.z);
        }
        public static void SetRotationZ(this Transform tf, float z)
        {
            tf.rotation = Quaternion.Euler(tf.rotation.eulerAngles.x, tf.rotation.eulerAngles.y, z);
        }
        public static void SetScaleX(this Transform tf, float x)
        {
            tf.localScale = new Vector3(x, tf.localScale.y, tf.localScale.z);
        }
        public static void SetScaleY(this Transform tf, float y)
        {
            tf.localScale = new Vector3(tf.localScale.x,y, tf.localScale.z);
        }
        public static void SetScaleZ(this Transform tf, float z)
        {
            tf.localScale = new Vector3(tf.localScale.x, tf.localScale.y, z);
        }
        public static Vector3 Get2DPosition(this Transform tf)
        {
            return new Vector3(tf.position.x, tf.position.y, 0);
        }
        
        /// <summary>
        /// 复制其他的Transform复制
        /// </summary>
        /// <param name="selfTf"></param>
        /// <param name="otherTf">复制的目标Transform</param>
        public static void CopyFrom(this Transform selfTf, Transform otherTf)
        {
            selfTf.position = otherTf.position;
            selfTf.localScale = otherTf.localScale;
            selfTf.rotation = otherTf.rotation;
        }
        

        #endregion
        
        
        /// <summary>
        /// 获得全部子物体
        /// </summary>
        /// <param name="tf"></param>
        /// <returns></returns>
        public static List<Transform> GetAllChildren(this Transform tf)
        {
            List<Transform> result = new List<Transform>();
            GetAllChildrenHelper(tf, result);
            return result;
        }
        public static List<Transform> GetAllChildren(this Transform tf, List<Transform> result)
        {
            GetAllChildrenHelper(tf, result);       
            return result;
        }
        
        private static void GetAllChildrenHelper(Transform tf, List<Transform> curRes)
        {
            if (tf.childCount == 0)
                return;
            for (int i = 0; i < tf.childCount; i++)
            {
                curRes.Add(tf.GetChild(i));
                GetAllChildrenHelper(tf.GetChild(i), curRes);
            }
        }
        
    }
}