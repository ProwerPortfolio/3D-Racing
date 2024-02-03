// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Пауза AudioSource.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
    {
        #region Parameters

        private AudioSource audioSource;

        private Pauser pauser;

        #endregion

        #region API

        private void OnPauseStateChanged(bool pause)
        {
            if (pause)
                audioSource.Pause();
            
            if (!pause)
                audioSource.Play();
        }

        #region Unity API

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            pauser.PauseStateChange += OnPauseStateChanged;
        }

        private void OnDestroy()
        {
            pauser.PauseStateChange -= OnPauseStateChanged;
        }

        #endregion

        #region Public API

        public void Construct(Pauser t) => pauser = t;

        #endregion

        #endregion
    }
}