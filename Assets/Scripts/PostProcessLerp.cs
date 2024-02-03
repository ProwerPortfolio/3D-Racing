// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������������ ����-�������������� �������� � ����������� �� �������� ����������.
    /// </summary>
    public class PostProcessLerp : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// ������ �� PostProcessProfile.
        /// </summary>
        [SerializeField] private PostProcessProfile postProcessProfile;

        /// <summary>
        /// ������ �� ������ �������� ���� ������.
        /// </summary>
        private ChromaticAberration blur;

        /// <summary>
        /// ������ �� ������ ��������.
        /// </summary>
        private Vignette vignette;

        /// <summary>
        /// �������� ������������ �������� ���� ������.
        /// </summary>
        [SerializeField] private float minBlur;
        /// <summary>
        /// �������� ������������� �������� ���� ������.
        /// </summary>
        [SerializeField] private float maxBlur;

        /// <summary>
        /// �������� ����������� ������������� ��������.
        /// </summary>
        [SerializeField] private float minVignette;
        /// <summary>
        /// �������� ������������ ������������� ��������.
        /// </summary>
        [SerializeField] private float maxVignette;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            postProcessProfile.TryGetSettings(out blur);
            postProcessProfile.TryGetSettings(out vignette);
        }

        private void Update()
        {
            blur.intensity.value = Mathf.Lerp(minBlur, maxBlur, car.LinearVelocity / car.MaxSpeed);

            vignette.intensity.value = Mathf.Lerp(minVignette, maxVignette, car.LinearVelocity / car.MaxSpeed);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}