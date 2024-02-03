// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Ёффекты ветра на скорости.
    /// </summary>
    public class WindEffects : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// —сылка на автомобиль.
        /// </summary>
        [SerializeField] private Car car;

        [SerializeField] private AudioSource windSound;

        [SerializeField] private ParticleSystem windParticles;

        /// <summary>
        /// «начение минимальной громкости звука ветра.
        /// </summary>
        [SerializeField] private float minVolume;
        /// <summary>
        /// «начение максимальной громкости звука ветра.
        /// </summary>
        [SerializeField] private float maxVolume;

        /// <summary>
        /// «начение минимальной прозрачности частиц ветра.
        /// </summary>
        [SerializeField] private float minAlpha;
        /// <summary>
        /// «начение максимальной прозрачности частиц ветра.
        /// </summary>
        [SerializeField] private float maxAlpha;

        /// <summary>
        /// «начение минимальной скорости частиц ветра.
        /// </summary>
        [SerializeField] private float minSpeed;
        /// <summary>
        /// «начение максимальной скорости частиц ветра.
        /// </summary>
        [SerializeField] private float maxSpeed;

        #endregion

        #region API



        #region Unity API

        private void Update()
        {
            windSound.volume = Mathf.Lerp(minVolume, maxVolume, car.LinearVelocity / car.MaxSpeed);

            float alpha = Mathf.Lerp(minAlpha, maxAlpha, car.LinearVelocity / car.MaxSpeed);

            windParticles.startColor = new Color(1, 1, 1, alpha);

            windParticles.startSpeed = Mathf.Lerp(minSpeed, maxSpeed, car.LinearVelocity / car.MaxSpeed);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}