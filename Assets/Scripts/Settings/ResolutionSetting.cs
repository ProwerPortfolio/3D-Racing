// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Настройка разрешения.
    /// </summary>
    [CreateAssetMenu]
    public class ResolutionSetting : Setting
    {
        #region Parameters

        [SerializeField]
        private Vector2Int[] avalibaleResolutions = new Vector2Int[]
        {
            new Vector2Int(800, 600),
            new Vector2Int(1280, 720),
            new Vector2Int(1600, 900),
            new Vector2Int(1920, 1080)
        };

        private int currentResolutionIndex = 0;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public override bool isMinValue => currentResolutionIndex == 0;

        public override bool isMaxValue => currentResolutionIndex == avalibaleResolutions.Length - 1;

        public override void SetNextValue()
        {
            if (!isMaxValue)
            {
                currentResolutionIndex++;
                Apply();
            }
        }

        public override void SetPreviousValue()
        {
            if (!isMinValue)
            {
                currentResolutionIndex--;
                Apply();
            }
        }

        public override object GetValue()
        {
            return avalibaleResolutions[currentResolutionIndex];
        }

        public override string GetStringValue()
        {
            return avalibaleResolutions[currentResolutionIndex].x + "x" + avalibaleResolutions[currentResolutionIndex].y;
        }

        public override void Apply()
        {
            Screen.SetResolution(avalibaleResolutions[currentResolutionIndex].x, avalibaleResolutions[currentResolutionIndex].y, true);
            Save();
        }

        public override void Load()
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionSetting", avalibaleResolutions.Length - 1);
        }

        public void Save()
        {
            PlayerPrefs.SetInt("ResolutionSetting", currentResolutionIndex);
        }

        #endregion

        #endregion
    }
}