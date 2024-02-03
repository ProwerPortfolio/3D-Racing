// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Точка на треке для отслеживания автомобиля и его состояния.
    /// </summary>
    public class TrackPoint : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Событие, которое вызывается, если автомобиль проехал через точку на треке.
        /// </summary>
        public event UnityAction<TrackPoint> Triggered;

        /// <summary>
        /// Ссылка на следующую точку на треке.
        /// </summary>
        public TrackPoint next;

        /// <summary>
        /// Является ли эта точка на треке первой?
        /// </summary>
        public bool isFirst;

        /// <summary>
        /// Является ли эта точка на треке последней?
        /// </summary>
        public bool isLast;

        /// <summary>
        /// Является ли эта точка на треке целью?
        /// </summary>
        protected bool isTarget;

        #endregion

        #region API

        protected virtual void OnPassed() { }

        protected virtual void OnAssignAsTarget() { }

        #region Unity API

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.GetComponent<Car>()) return;

            Triggered?.Invoke(this);
        }

        public void Reset()
        {
            next = null;
            isFirst = false;
            isLast = false;
        }

        #endregion

        #region Public API

        public bool IsTarget => isTarget;

        public void Passed()
        {
            isTarget = false;
            OnPassed();
        }

        /// <summary>
        /// Назаначает эту точку на треке как цель.
        /// </summary>
        public void AssignAsTarget()
        {
            isTarget = true;
            OnAssignAsTarget();
        }

        #endregion

        #endregion
    }
}