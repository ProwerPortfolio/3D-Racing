// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������, ������� ��������� ��������� �� ����������.
    /// </summary>
    public class UICarUpdater : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����� ������� �������� ����������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI speedText;

        /// <summary>
        /// ������ �� ����� ������� �������� ����������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI gearText;

        /// <summary>
        /// ������ �� ����������� �������� ���� ������� �������� ��������� ����������.
        /// </summary>
        [SerializeField] private Image gearProgressBar;

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        /// <summary>
        /// ��������� ����� ������� �������� ����������.
        /// </summary>
        /// <param name="currentSpeed">������� �������� ����������.</param>
        public void SpeedTextUpdate(float currentSpeed)
        {
            int speed = (int) currentSpeed;

            speedText.text = speed.ToString();
        }

        /// <summary>
        /// ��������� ����� ������� �������� ����������.
        /// </summary>
        /// <param name="currentGear">��������� ������� �������� ����������.</param>
        /// <param name="currentGearIndex">������ ������� �������� ����������.</param>
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
        /// ��������� �������� ��� ������� �������� ���������.
        /// </summary>
        /// <param name="currentEngineRpm">������� ������� ���������.</param>
        /// <param name="maxEngineRpm">������������ ������� ���������.</param>
        public void GearProgressBarUpdate(float currentEngineRpm, float maxEngineRpm)
        {
            gearProgressBar.fillAmount = currentEngineRpm / maxEngineRpm;
        }

        #endregion

        #endregion
    }
}