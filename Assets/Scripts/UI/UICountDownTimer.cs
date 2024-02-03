// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Вывод таймера отсчёта до гонки в UI.
    /// </summary>
    public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        #region Parameters

        [SerializeField] private GameObject toolTipCanvas;

        /// <summary>
        /// Ссылка на текст таймера отсчёта до гонки.
        /// </summary>
        [SerializeField] private TextMeshProUGUI text;

        /// <summary>
        /// Ссылка на трекер состояния гонки.
        /// </summary>
        private RaceStateTracker tracker;

        #endregion

        #region API

        private void OnRaceStarted()
        {
            text.enabled = false;
            enabled = false;
        }

        private void OnPeparationStarted()
        {
            text.enabled = true;
            enabled = true;

            toolTipCanvas.SetActive(false);
        }

        #region Unity API

        private void Start()
        {
            tracker.PeparationStarted += OnPeparationStarted;
            tracker.Started += OnRaceStarted;

            text.enabled = false;

            toolTipCanvas.SetActive(true);
        }

        private void OnDestroy()
        {
            tracker.PeparationStarted -= OnPeparationStarted;
            tracker.Started -= OnRaceStarted;
        }

        private void Update()
        {
            text.text = tracker.CountDownTimer.Value.ToString("F0");

            if (text.text == "0")
                text.text = "GO!";
        }

        #endregion

        #region Public API

        public void Construct(RaceStateTracker t)
        {
            tracker = t;
        }

        #endregion

        #endregion
    }
}