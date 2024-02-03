// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ���������, ������� ������ ������ �� ���������� � ������.
    /// </summary>
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������.
        /// </summary>
        protected Car car;

        /// <summary>
        /// ������ �� ������.
        /// </summary>
        protected new Camera camera;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// ����� ��������� � �������.
        /// </summary>
        /// <param name="car">������ �� ����������.</param>
        /// <param name="camera">������ �� ������.</param>
        public virtual void SetProperties(Car car, Camera camera)
        {
            this.car = car;
            this.camera = camera;
        }

        #endregion

        #endregion
    }
}