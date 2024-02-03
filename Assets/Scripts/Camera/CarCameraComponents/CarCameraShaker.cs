// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Тряска камеры.
    /// </summary>
    public class CarCameraShaker : CarCameraComponent
    {
        #region Parameters

        /// <summary>
        /// Нормализованная скорость автомобиля, при которой камера начинает трястись.
        /// </summary>
        [SerializeField][Range(0f, 1f)] private float normalizeSpeedShake;

        /// <summary>
        /// Сила тряски камеры.
        /// </summary>
        [SerializeField] private float shakeAmount;

        #endregion

        #region API



        #region Unity API

        private void Update()
        {
            if (car.LinearVelocity / car.MaxSpeed >= normalizeSpeedShake)
                transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}