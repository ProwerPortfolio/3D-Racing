// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Настройка качества графики.
    /// </summary>
    [CreateAssetMenu]
    public class GraphicsQualitySetting : Setting
    {
        #region Parameters

        private int currentLevelIndex = 0;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public override bool isMinValue => currentLevelIndex == 0;

        public override bool isMaxValue => currentLevelIndex == QualitySettings.names.Length - 1;

        public override void SetNextValue()
        {
            if (!isMaxValue)
            {
                currentLevelIndex++;
                Apply();
            }
        }

        public override void SetPreviousValue()
        {
            if (!isMinValue)
            {
                currentLevelIndex--;
                Apply();
            }
        }

        public override object GetValue()
        {
            return QualitySettings.names[currentLevelIndex];
        }

        public override string GetStringValue()
        {
            return QualitySettings.names[currentLevelIndex];
        }

        public override void Apply()
        {
            QualitySettings.SetQualityLevel(currentLevelIndex);
            Save();
        }

        public override void Load()
        {
            currentLevelIndex = PlayerPrefs.GetInt("GraphicsQualitySetting", QualitySettings.names.Length - 1);
        }

        public void Save()
        {
            PlayerPrefs.SetInt("GraphicsQualitySetting", currentLevelIndex);
        }

        #endregion

        #endregion
    }
}