// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ”правл€ет звуками двигател€.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// —сылка на автомобиль.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// —сылка на AudioSource.
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// ћножитель pitch аудиосурса.
        /// </summary>
        [SerializeField] private float pitchModifier;
        /// <summary>
        /// ћножитель volume аудиосурса.
        /// </summary>
        [SerializeField] private float volumeModifier;
        /// <summary>
        /// ћножитель rpm дл€ раст€гивани€ звука.
        /// </summary>
        [SerializeField] private float rpmModifier;

        /// <summary>
        /// Pitch по умолчанию.
        /// </summary>
        [SerializeField] private float basePitch;
        /// <summary>
        /// √ромкость по умолчанию.
        /// </summary>
        [SerializeField] private float baseVolume;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            audioSource.pitch = basePitch + pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
            audioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}