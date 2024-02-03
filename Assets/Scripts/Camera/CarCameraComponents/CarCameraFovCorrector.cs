// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������������ ���� ������ ������ � ����������� �� �������� ����������.
    /// </summary>
    public class CarCameraFovCorrector : CarCameraComponent
    {
        #region Parameters

        /// <summary>
        /// �������� ������������ ���� ������ ������.
        /// </summary>
        [SerializeField] private float minFov;
        /// <summary>
        /// �������� ������������� ���� ������ ������.
        /// </summary>
        [SerializeField] private float maxFov;

        /// <summary>
        /// �������� ���� ������ ������ �� ���������.
        /// </summary>
        private float defaultFov;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            defaultFov = camera.fieldOfView;
        }

        private void Update()
        {
            camera.fieldOfView = Mathf.Lerp(minFov, maxFov, car.LinearVelocity / car.MaxSpeed);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}