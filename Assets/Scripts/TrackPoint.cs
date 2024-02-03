// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ����� �� ����� ��� ������������ ���������� � ��� ���������.
    /// </summary>
    public class TrackPoint : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// �������, ������� ����������, ���� ���������� ������� ����� ����� �� �����.
        /// </summary>
        public event UnityAction<TrackPoint> Triggered;

        /// <summary>
        /// ������ �� ��������� ����� �� �����.
        /// </summary>
        public TrackPoint next;

        /// <summary>
        /// �������� �� ��� ����� �� ����� ������?
        /// </summary>
        public bool isFirst;

        /// <summary>
        /// �������� �� ��� ����� �� ����� ���������?
        /// </summary>
        public bool isLast;

        /// <summary>
        /// �������� �� ��� ����� �� ����� �����?
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
        /// ���������� ��� ����� �� ����� ��� ����.
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