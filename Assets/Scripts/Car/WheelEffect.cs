// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������� �� ��� ����������.
    /// </summary>
    public class WheelEffect : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ ������ �� ������� ����������.
        /// </summary>
        [SerializeField] private WheelCollider[] wheels;

        /// <summary>
        /// ������ ������ �� ������� ���� �� ����.
        /// </summary>
        [SerializeField] private ParticleSystem[] wheelsSmoke;

        /// <summary>
        /// ������ �� ��������� �� ������ ���������� ����.
        /// </summary>
        [SerializeField] private AudioSource audioSource;

        /// <summary>
        /// ����� ��������� ���������� �� �������� ��������.
        /// </summary>
        [SerializeField] private float forwardSlipLimit;
        /// <summary>
        /// ����� �������� ���������� �� �������� ��������.
        /// </summary>
        [SerializeField] private float sidewaysSlipLimit;

        /// <summary>
        /// ������ �� ������ � �������� ������ �� ��� �� ������.
        /// </summary>
        [SerializeField] private GameObject skidPrefab;

        /// <summary>
        /// �������� ����� �� �������� ���� �� Y.
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