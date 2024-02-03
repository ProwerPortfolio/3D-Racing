// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ќбновл€ет интерфейс прошедшего времени.
    /// </summary>
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        #region Parameters

        [SerializeField] private TextMeshProUGUI timeText;

        /// <summary>
        /// —сылка на текст дл€ вывода оставшегос€ времени.
        /// </summary>
        [SerializeField] private TextMeshProUGUI text;

        /// <summary>
        /// —сылка на RaceStateTracker.
        /// </summary>
        private RaceStateTracker raceStateTracker;

        /// <summary>
        /// —сылка на RaceTimeTracker.
        /// </summary>
        private RaceTimeTracker raceTimeTracker;

        #endregion

        #region API

        private void OnRaceStarted()
        {
            text.enabled = true;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            text.enabled = false;
            enabled = false;

            float lastCurrentTime = raceTimeTracker.CurrentTime;

            timeText.text = StringTime.SecondToTimeString(lastCurrentTime);
        }

        #region Unity API

        private void Start()
        {
            raceStateTracker.Started += OnRaceStarted;
            raceStateTracker.Completed += OnRaceCompleted;

            text.enabled = false;
        }

        private void Update()
        {
            text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStarted;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        #endregion

        #region Public API

        public void Construct(RaceStateTracker t)
        {
            raceStateTracker = t;
        }

        public void Construct(RaceTimeTracker t)
        {
            raceTimeTracker = t;
        }

        #endregion

        #endregion
    }
}