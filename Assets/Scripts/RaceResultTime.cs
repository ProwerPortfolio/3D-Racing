// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ��������� ���������� ����������� ����� �� �������.
    /// </summary>
    public class RaceResultTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        #region Parameters

        public const string SaveMark = "_player_best_time";

        /// <summary>
        /// �������, ������� ���������� ��� ���������� ���������� ������� ������.
        /// </summary>
        public event UnityAction ResultUpdated;

        /// <summary>
        /// ����� ������.
        /// </summary>
        [SerializeField] private float goldTime;

        /// <summary>
        /// ��������� ����� ������.
        /// </summary>
        private float playerRecordTime;

        /// <summary>
        /// ������� �����.
        /// </summary>
        private float currentTime;

        /// <summary>
        /// ������ �� RaceStateTracker.
        /// </summary>
        private RaceStateTracker raceStateTracker;

        /// <summary>
        /// ������ �� RaceTimeTracker.
        /// </summary>
        private RaceTimeTracker raceTimeTracker;

        #endregion

        #region API

        private void OnRaceCompleted()
        {
            float absoleteRecord = GetAbsoleteRecord();

            if (raceTimeTracker.CurrentTime < absoleteRecord || playerRecordTime == 0)
            {
                playerRecordTime = raceTimeTracker.CurrentTime;

                Save();
            }

            currentTime = raceTimeTracker.CurrentTime;

            ResultUpdated?.Invoke();
        }

        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }

        #region Unity API

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            raceStateTracker.Completed += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnRaceCompleted;
        }

        #endregion

        #region Public API

        public float GoldTime => goldTime;

        public float PlayerRecordTime => playerRecordTime;

        public float CurrentTime => currentTime;

        public void Construct(RaceStateTracker t)
        {
            raceStateTracker = t;
        }

        public void Construct(RaceTimeTracker t)
        {
            raceTimeTracker = t;
        }

        /// <summary>
        /// �������� ���������� ������ ������.
        /// </summary>
        /// <returns>���������� ������ ������.</returns>
        public float GetAbsoleteRecord()
        {
            if (playerRecordTime < goldTime && playerRecordTime != 0)
            {
                return playerRecordTime;
            }
            else
                return goldTime;
        }

        #endregion

        #endregion
    }
}