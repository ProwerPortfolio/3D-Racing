// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Таймер.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Событие, которое вызывается, когда таймер завершается.
        /// </summary>
        public event UnityAction Finished;

        /// <summary>
        /// Время, которое будет работать таймер до срабатывания.
        /// </summary>
        [SerializeField] private float time;

        /// <summary>
        /// Текущее оставшееся время до завершения работы таймера.
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