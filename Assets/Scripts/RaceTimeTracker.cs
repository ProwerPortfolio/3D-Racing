// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Отслеживает время гонки.
    /// </summary>
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        #region Parameters

        /// <summary>
        /// Ссылка на RaceStateTracker.
        /// </summary>
        private RaceStateTracker stateTracker;

        /// <summary>
        /// Текущее время.
        /// </summary>
        private float currentTime;

        #endregion

        #region API

        private void OnRaceCompleted()
        {
            enabled = false;
        }

        private void OnRaceStarted()
        {
            enabled = true;

            currentTime = 0;
        }

        #region Unity API

        private void Start()
        {
            stateTracker.Started += OnRaceStarted;
            stateTracker.Completed += OnRaceCompleted;

            enabled = false;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            stateTracker.Started -= OnRaceStarted;
            stateTracker.Completed -= OnRaceCompleted;
        }

        #endregion

        #region Public API

        public float CurrentTime => currentTime;

        public void Construct(RaceStateTracker t)
        {
            stateTracker = t;
        }

        #endregion

        #endregion
    }
}