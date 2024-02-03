// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Контейнер, который хранит ссылки на автомобиль и камеру.
    /// </summary>
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на автомобиль.
        /// </summary>
        protected Car car;

        /// <summary>
        /// Ссылка на камеру.
        /// </summary>
        protected new Camera camera;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// Задаёт параметры в скрипте.
        /// </summary>
        /// <param name="car">Ссылка на автомобиль.</param>
        /// <param name="camera">Ссылка на камеру.</param>
        public virtual void SetProperties(Car car, Camera camera)
        {
            this.car = car;
            this.camera = camera;
        }

        #endregion

        #endregion
    }
}