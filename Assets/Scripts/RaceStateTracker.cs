// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Состояния гонки.
    /// </summary>
    public enum RaceState
    {
        /// <summary>
        /// Подготовка.
        /// </summary>
        Preparation,
        /// <summary>
        /// Обратный отчёт.
        /// </summary>
        CountDown,
        /// <summary>
        /// Гонка.
        /// </summary>
        Race,
        /// <summary>
        /// Прохождение гонки.
        /// </summary>
        Passed
    }

    /// <summary>
    /// Отслеживает состояние гонки.
    /// </summary>
    public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
    {
        #region Parameters

        /// <summary>
        /// Подготовка к старту.
        /// </summary>
        public event UnityAction PeparationStarted;

        /// <summary>
        /// Начало гонки.
        /// </summary>
        public event UnityAction Started;

        /// <summary>
        /// Прохождение гонки.
        /// </summary>
        public event UnityAction Completed;

        public event UnityAction<TrackPoint> TrackPointPassed;

        public event UnityAction<int> LapCompleted;

        /// <summary>
        /// Ссылка на TrackPointCircuit.
        /// </summary>
        private TrackPointCircuit trackPointCircuit;

        /// <summary>
        /// Ссылка на таймер.
        /// </summary>
        [SerializeField] private Timer countDownTimer;

        /// <summary>
        /// Количество кругов до завершения.
        /// </summary>
        [SerializeField] private int lapsToComplete;

        /// <summary>
        /// Текущее состояние гонки.
        /// </summary>
        private RaceState state;

        #endregion

        #region API

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            TrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (trackPointCircuit.TrackType == TrackType.Sprint)
            {
                CompleteRace();
            }

            if (trackPointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == lapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }

        private void OnCountDownTimerFinished()
        {
            StartRace();
        }

        /// <summary>
        /// Начинает гонку.
        /// </summary>
        private void StartRace()
        {
            if (state != RaceState.CountDown) return;

            StartState(RaceState.Race);

            Started?.Invoke();
        }

        /// <summary>
        /// Завершает гонку.
        /// </summary>
        private void CompleteRace()
        {
            if (state != RaceState.Race) return;

            StartState(RaceState.Passed);

            Completed?.Invoke();
        }

        private void CompleteLap(int lapAmount)
        {
            LapCompleted?.Invoke(lapAmount);
        }

        /// <summary>
        /// Задаёт состояние.
        /// </summary>
        /// <param name="state"></param>
        private void StartState(RaceState state)
        {
            this.state = state;
        }

        #region Unity API

        private void Start()
        {
            StartState(RaceState.Preparation);

            countDownTimer.enabled = false;

            countDownTimer.Finished += OnCountDownTimerFinished;

            trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
            trackPointCircuit.LapCompleted += OnLapCompleted;
        }

        private void OnDestroy()
        {
            countDownTimer.Finished -= OnCountDownTimerFinished;

            trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
            trackPointCircuit.LapCompleted -= OnLapCompleted;
        }

        #endregion

        #region Public API

        public RaceState State => state;

        public Timer CountDownTimer => countDownTimer;

        /// <summary>
        /// Запускает обратный отчёт перед гонкой.
        /// </summary>
        public void LaunchPeparationStart()
        {
            if (state != RaceState.Preparation) return;

            StartState(RaceState.CountDown);

            countDownTimer.enabled = true;

            PeparationStarted?.Invoke();
        }

        public void Construct(TrackPointCircuit t)
        {
            this.trackPointCircuit = t;
        }

        #endregion

        #endregion
    }
}