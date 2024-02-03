// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ��������� ������� ���������.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// ������ �� AudioSource.
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// ��������� pitch ����������.
        /// </summary>
        [SerializeField] private float pitchModifier;
        /// <summary>
        /// ��������� volume ����������.
        /// </summary>
        [SerializeField] private float volumeModifier;
        /// <summary>
        /// ��������� rpm ��� ������������ �����.
        /// </summary>
        [SerializeField] private float rpmModifier;

        /// <summary>
        /// Pitch �� ���������.
        /// </summary>
        [SerializeField] private float basePitch;
        /// <summary>
        /// ��������� �� ���������.
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