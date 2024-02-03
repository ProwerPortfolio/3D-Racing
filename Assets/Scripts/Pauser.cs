// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Пауза.
    /// </summary>
    public class Pauser : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Событие, которое вызывается, если сосотояние паузы изменилось.
        /// </summary>
        public event UnityAction<bool> PauseStateChange;

        private bool isPause;

        #endregion

        #region API

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            UnPause();
        }

        #region Unity API

        private void Awake()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        #endregion

        #region Public API

        public bool IsPause => isPause;

        public void ChangePauseState()
        {
            if (isPause)
                UnPause();
            else
                Pause();
        }

        public void Pause()
        {
            if (isPause) return;

            Time.timeScale = 0.0f;
            isPause = true;
            PauseStateChange?.Invoke(isPause);
        }

        public void UnPause()
        {
            if (!isPause) return;

            Time.timeScale = 1.0f;
            isPause = false;
            PauseStateChange?.Invoke(isPause);
        }

        #endregion

        #endregion
    }
}