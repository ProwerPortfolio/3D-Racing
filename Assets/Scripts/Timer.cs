// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// �������, ������� ����������, ����� ������ �����������.
        /// </summary>
        public event UnityAction Finished;

        /// <summary>
        /// �����, ������� ����� �������� ������ �� ������������.
        /// </summary>
        [SerializeField] private float time;

        /// <summary>
        /// ������� ���������� ����� �� ���������� ������ �������.
        /// </summary>
        private float value;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            value = time;
        }

        private void Update()
        {
            value -= Time.deltaTime;

            if (value <= 0)
            {
                enabled = false;

                Finished?.Invoke();
            }
        }

        #endregion

        #region Public API

        public float Value => value;

        #endregion

        #endregion
    }
}