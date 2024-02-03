// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Обеспечивает глобальные зависимости.
    /// </summary>
    public class GlobalDependenciesContainer : Dependency
    {
        #region Parameters

        private static GlobalDependenciesContainer instance;

        [SerializeField] private Pauser pauser;

        #endregion

        #region API

        protected override void BindAll(MonoBehaviour mono)
        {
            Bind<Pauser>(pauser, mono);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            FindAllObjectToBind();
        }

        #region Unity API

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this; 

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}