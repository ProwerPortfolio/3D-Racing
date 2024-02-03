// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Кнопка настройки в интерфейсе.
    /// </summary>
    public class UISettingButton : UISelectableButton, IScriptableObjectProperty
    {
        #region Parameters

        [SerializeField] private Setting setting;

        [SerializeField] private TextMeshProUGUI titleText;

        [SerializeField] private TextMeshProUGUI valueText;

        [SerializeField] private Image previousImage;

        [SerializeField] private Image nextImage;

        #endregion

        #region API

        private void UpdateInfo()
        {
            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            previousImage.enabled = !setting.isMinValue;
            nextImage.enabled = !setting.isMaxValue;
        }

        #region Unity API

        private void Start()
        {
            ApplyProperty(setting);
        }

        #endregion

        #region Public API

        public void SetNextValueSetting()
        {
            setting?.SetNextValue();
            setting?.Apply();
            UpdateInfo();
        }

        public void SetPreviousValueSetting()
        {
            setting?.SetPreviousValue();
            setting?.Apply();
            UpdateInfo();
        }

        public void ApplyProperty(ScriptableObject setting)
        {
            if (setting == null) return;

            if (setting is Setting == false) return;

            this.setting = setting as Setting;

            UpdateInfo();
        }

        #endregion

        #endregion
    }
}