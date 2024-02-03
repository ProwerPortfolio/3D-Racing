// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

#endregion

namespace Racing3D
{
    /// <summary>
    /// »нтерпол€ци€ пост-процессинговых эффектов в зависимости от скорости автомобил€.
    /// </summary>
    public class PostProcessLerp : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// —сылка на автомобиль.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// —сылка на PostProcessProfile.
        /// </summary>
        [SerializeField] private PostProcessProfile postProcessProfile;

        /// <summary>
        /// —сылка на эффект размыти€ краЄв экрана.
        /// </summary>
        private ChromaticAberration blur;

        /// <summary>
        /// —сылка на эффект виньетки.
        /// </summary>
        private Vignette vignette;

        /// <summary>
        /// «начение минимального размыти€ краЄв экрана.
        /// </summary>
        [SerializeField] private float minBlur;
        /// <summary>
        /// «начение максимального размыти€ краЄв экрана.
        /// </summary>
        [SerializeField] private float maxBlur;

        /// <summary>
        /// «начение минимальной интенсивности виньетки.
        /// </summary>
        [SerializeField] private float minVignette;
        /// <summary>
        /// «начение максимальной интенсивности виньетки.
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