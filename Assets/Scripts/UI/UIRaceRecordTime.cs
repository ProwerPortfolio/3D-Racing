// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ќбновл€ет интерфейс рекорда времени игрока.
    /// </summary>
    public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
    {
        #region Parameters

        [SerializeField] private GameObject resultsCanvas;
        [SerializeField] private TextMeshProUGUI recordText;

        [SerializeField] private GameObject goldRecordObject;
        [SerializeField] private GameObject playerRecordObject;
        [SerializeField] private TextMeshProUGUI goldRecordTimeText;
        [SerializeField] private TextMeshProUGUI playerRecordTimeText;

        /// <summary>
        /// —сылка на RaceStateTracker.
        /// </summary>
        private RaceStateTracker raceStateTracker;

        /// <summary>
        /// —сылка на RaceResultTime.
        /// </summary>
        private RaceResultTime raceResultTime;

        #endregion

        #region API

        private void OnRaceStart()
        {
            if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.PlayerRecordTime == 0)
            {
                goldRecordObject.SetActive(true);
                goldRecordTimeText.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
            }

            if (raceResultTime.PlayerRecordTime != 0)
            {
                playerRecordObject.SetActive(true);
                playerRecordTimeText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            }
        }

        private void OnRaceCompleted()
        {
            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);

            recordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            resultsCanvas.SetActive(true);
        }

        #region Unity API

        private void Start()
        {
            raceStateTracker.Started += OnRaceStart;
            raceStateTracker.Completed += OnRaceCompleted;

            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);
            resultsCanvas.SetActive(false);
        }

        private void OnDestroy()
        {
            raceStateTracker.Started -= OnRaceStart;
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        #endregion

        #region Public API

        public void Construct(RaceStateTracker t)
        {
            raceStateTracker = t;
        }

        public void Construct(RaceResultTime t)
        {
            raceResultTime = t;
        }

        #endregion

        #endregion
    }
}