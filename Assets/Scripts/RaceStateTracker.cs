// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ��������� �����.
    /// </summary>
    public enum RaceState
    {
        /// <summary>
        /// ����������.
        /// </summary>
        Preparation,
        /// <summary>
        /// �������� �����.
        /// </summary>
        CountDown,
        /// <summary>
        /// �����.
        /// </summary>
        Race,
        /// <summary>
        /// ����������� �����.
        /// </summary>
        Passed
    }

    /// <summary>
    /// ����������� ��������� �����.
    /// </summary>
    public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
    {
        #region Parameters

        /// <summary>
        /// ���������� � ������.
        /// </summary>
        public event UnityAction PeparationStarted;

        /// <summary>
        /// ������ �����.
        /// </summary>
        public event UnityAction Started;

        /// <summary>
        /// ����������� �����.
        /// </summary>
        public event UnityAction Completed;

        public event UnityAction<TrackPoint> TrackPointPassed;

        public event UnityAction<int> LapCompleted;

        /// <summary>
        /// ������ �� TrackPointCircuit.
        /// </summary>
        private TrackPointCircuit trackPointCircuit;

        /// <summary>
        /// ������ �� ������.
        /// </summary>
        [SerializeField] private Timer countDownTimer;

        /// <summary>
        /// ���������� ������ �� ����������.
        /// </summary>
        [SerializeField] private int lapsToComplete;

        /// <summary>
        /// ������� ��������� �����.
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
        /// �������� �����.
        /// </summary>
        private void StartRace()
        {
            if (state != RaceState.CountDown) return;

            StartState(RaceState.Race);

            Started?.Invoke();
        }

        /// <summary>
        /// ��������� �����.
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
        /// ����� ���������.
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
        /// ��������� �������� ����� ����� ������.
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