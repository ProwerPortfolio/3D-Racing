// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Racing3D
{
    /// <summary>
    /// —крипт, который обновл€ет интерфейс на автомобиле.
    /// </summary>
    public class UICarUpdater : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// —сылка на текст текущей скорости автомобил€.
        /// </summary>
        [SerializeField] private TextMeshProUGUI speedText;

        /// <summary>
        /// —сылка на текст текущей передачи автомобил€.
        /// </summary>
        [SerializeField] private TextMeshProUGUI gearText;

        /// <summary>
        /// —сылка на изображение прогресс бара текущих оборотов двигател€ автомобил€.
        /// </summary>
        [SerializeField] private Image gearProgressBar;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// ќбновл€ет текст текущей скорости автомобил€.
        /// </summary>
        /// <param name="currentSpeed">“екуща€ скорость автомобил€.</param>
        public void SpeedTextUpdate(float currentSpeed)
        {
            int speed = (int) currentSpeed;

            speedText.text = speed.ToString();
        }

        /// <summary>
        /// ќбновл€ет текст текущей передачи автомобил€.
        /// </summary>
        /// <param name="currentGear">ћножитель текущей передачи автомобил€.</param>
        /// <param name="currentGearIndex">»ндекс текущей передачи автомобил€.</param>
        public void GearTextUpdate(float currentGear, int currentGearIndex)
        {
            if (currentGear < 0)
            {
                gearText.text = "R";
            }

            if (currentGear == 0)
            {
                gearText.text = "N";
            }

            if (currentGear > 0)
            {
                gearText.text = (currentGearIndex + 1).ToString();
            }
        }

        /// <summary>
        /// ќбновл€ет прогресс бар текущих оборотов двигател€.
        /// </summary>
        /// <param name="currentEngineRpm">“екущие обороты двигател€.</param>
        /// <param name="maxEngineRpm">ћаксимальные обороты двигател€.</param>
        public void GearProgressBarUpdate(float currentEngineRpm, float maxEngineRpm)
        {
            gearProgressBar.fillAmount = currentEngineRpm / maxEngineRpm;
        }

        #endregion

        #endregion
    }
}