// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Зона, при попадании в которую уровень перезапускается.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class KillZone : MonoBehaviour
    {
        #region Parameters



        #endregion

        #region API



        #region Unity API

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>())
                SceneManager.LoadScene(0);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}