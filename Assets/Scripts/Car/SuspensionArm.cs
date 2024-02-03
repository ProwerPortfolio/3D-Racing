// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Отвечает за рычаги подвески. Направляет их в сторону колеса.
    /// </summary>
    public class SuspensionArm : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на то, как будет смещаться колесо.
        /// </summary>
        [SerializeField] private Transform target;
        /// <summary>
        /// Фактор, на который будет домнажаться смещение.
        /// </summary>
        [SerializeField] private float factor;

        /// <summary>
        /// Смещение изначальной позиции.
        /// </summary>
        private float baseOffset;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            baseOffset = target.localPosition.y;
        }

        private void Update()
        {
            transform.localEulerAngles = new Vector3(0, 0, (target.localPosition.y - baseOffset) * factor);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}