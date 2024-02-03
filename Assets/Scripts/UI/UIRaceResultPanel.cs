// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Интерфейс меню завершения гонки.
    /// </summary>
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        #region Parameters

        [SerializeField] private GameObject resultPanel;

        [SerializeField] private TextMeshProUGUI recordTime;

        [SerializeField] private TextMeshProUGUI currentTime;

        private RaceResultTime raceResultTime;

        #endregion

        #region API

        private void OnUpdateResults()
        {
            resultPanel.SetActive(true);

            recordTime.text = StringTime.SecondToTimeString(raceResultTime.GetAbsoleteRecord());
            recordTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
        }

        #region Unity API

        private void Start()
        {
            resultPanel.SetActive(false);
            raceResultTime.ResultUpdated += OnUpdateResults;
        }

        private void OnDestroy()
        {
            raceResultTime.ResultUpdated -= OnUpdateResults;
        }

        #endregion

        #region Public API

        public void Construct(RaceResultTime t) => raceResultTime = t;

        #endregion

        #endregion
    }
}