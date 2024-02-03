// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Рестартер сцены.
    /// </summary>
    public class SceneRestarter : MonoBehaviour
    {
        #region Parameters



        #endregion

        #region API



        #region Unity API

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}