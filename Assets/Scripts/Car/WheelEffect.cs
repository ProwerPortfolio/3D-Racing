// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Эффекты от шин автомобиля.
    /// </summary>
    public class WheelEffect : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Массив ссылок на колёсные коллайдеры.
        /// </summary>
        [SerializeField] private WheelCollider[] wheels;

        /// <summary>
        /// Массив ссылок на частицы дыма от колёс.
        /// </summary>
        [SerializeField] private ParticleSystem[] wheelsSmoke;

        /// <summary>
        /// Ссылка на аудиосурс со звуком скольжения колёс.
        /// </summary>
        [SerializeField] private AudioSource audioSource;

        /// <summary>
        /// Лимит переднего скольжения до создания эффектов.
        /// </summary>
        [SerializeField] private float forwardSlipLimit;
        /// <summary>
        /// Лимит бокового скольжения до создания эффектов.
        /// </summary>
        [SerializeField] private float sidewaysSlipLimit;

        /// <summary>
        /// Ссылка на префаб с эффектом полосы от шин на дороге.
        /// </summary>
        [SerializeField] private GameObject skidPrefab;

        /// <summary>
        /// Смещение следа от покрышек вниз по Y.
        /// </summary>
        [SerializeField] private float skidYOffset;

        private WheelHit wheelHit;

        private Transform[] skidTrail;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            skidTrail = new Transform[wheels.Length];
        }

        private void Update()
        {
            bool isSlip = false;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetGroundHit(out wheelHit);

                if (wheels[i].isGrounded)
                {
                    if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaysSlipLimit)
                    {
                        if (!skidTrail[i])
                            skidTrail[i] = Instantiate(skidPrefab).transform;

                        if (!audioSource.isPlaying)
                            audioSource.Play();

                        if (skidTrail[i])
                        {
                            skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius;
                            skidTrail[i].position -= new Vector3(0, skidYOffset, 0);
                            skidTrail[i].forward = -wheelHit.normal;

                            wheelsSmoke[i].transform.position = skidTrail[i].position;
                            wheelsSmoke[i].Emit(1);
                        }

                        isSlip = true;

                        continue;
                    }
                }

                skidTrail[i] = null;
                wheelsSmoke[i].Stop();
            }

            if (!isSlip)
                audioSource.Stop();
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}