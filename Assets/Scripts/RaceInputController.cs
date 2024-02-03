// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������������ ����������� ���������� ����������� � ����������� �� ��������� �����.
    /// </summary>
    public class RaceInputController : MonoBehaviour, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        #region Parameters

        /// <summary>
        /// ������ �� CarInputControl ��� �������� ���������� �����������.
        /// </summary>
        private CarInputControl carControl;

        /// <summary>
        /// ������ �� RaceStateTracker ��� ������������ �������� ��������� �����.
        /// </summary>
        private RaceStateTracker stateTracker;

        #endregion

        #region API

        private void OnRaceStarted()
        {
            carControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            carControl.Stop();

            carControl.enabled = false;
        }

        #region Unity API

        private void Start()
        {
            stateTracker.Started += OnRaceStarted;
            stateTracker.Completed += OnRaceFinished;

            carControl.enabled = false;
        }

        private void OnDestroy()
        {
            stateTracker.Started -= OnRaceStarted;

            stateTracker.Completed -= OnRaceFinished;
        }

        #endregion

        #region Public API

        public void Construct(CarInputControl t)
        {
            carControl = t;
        }

        public void Construct(RaceStateTracker t)
        {
            stateTracker = t;
        }

        #endregion

        #endregion
    }
}