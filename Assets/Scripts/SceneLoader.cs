// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Загрузчик сцен.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        #region Parameters

        private const string MainMenuSceneTitle = "MainMenu";

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #endregion

        #endregion
    }
}