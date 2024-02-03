// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Dependency : MonoBehaviour
    {
        #region Parameters



        #endregion

        #region API

        protected virtual void BindAll(MonoBehaviour mono) { }

        protected void FindAllObjectToBind()
        {
            MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < monoInScene.Length; i++)
            {
                BindAll(monoInScene[i]);
            }
        }

        protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
        {
            if (target is IDependency<T>)
                (target as IDependency<T>).Construct(bindObject as T);
        }

        #region Unity API

        

        #endregion

        #region Public API

        

        #endregion

        #endregion
    }
}