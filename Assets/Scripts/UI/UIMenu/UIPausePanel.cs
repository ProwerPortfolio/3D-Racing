// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ѕанель паузы в интерфейсе.
    /// </summary>
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        #region Parameters

        /// <summary>
        /// —сылка на объект панели.
        /// </summary>
        [SerializeField] private GameObject panel;

        private Pauser pauser;

        #endregion

        #region API

        private void OnPauseStateChanged(bool isPause)
        {
            panel.SetActive(isPause);
        }

        #region Unity API

        private void Start()
        {
            panel.SetActive(false);
            pauser.PauseStateChange += OnPauseStateChanged;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauser.ChangePauseState();
            }
        }

        private void OnDestroy()
        {
            pauser.PauseStateChange -= OnPauseStateChanged;
        }

        #endregion

        #region Public API

        public void Construct(Pauser t) => pauser = t;

        public void UnPause()
        {
            pauser.UnPause();
        }

        #endregion

        #endregion
    }
}