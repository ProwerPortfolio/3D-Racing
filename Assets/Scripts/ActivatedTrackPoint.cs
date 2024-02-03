// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// �������������� ������� ����� �� �����.
    /// </summary>
    public class ActivatedTrackPoint : TrackPoint
    {
        #region Parameters

        /// <summary>
        /// ������ �� ������, ������� ����� ����������, ����� ����� ������ �������.
        /// </summary>
        [SerializeField] private GameObject hint;

        #endregion

        #region API

        protected override void OnPassed()
        {
            hint.SetActive(false);
        }

        protected override void OnAssignAsTarget()
        {
            hint.SetActive(true);
        }

        #region Unity API

        private void Start()
        {
            hint.SetActive(isTarget);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}