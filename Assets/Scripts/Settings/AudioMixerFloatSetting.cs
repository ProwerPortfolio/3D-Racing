// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Audio;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Настройка уровня громкости.
    /// </summary>
    [CreateAssetMenu]
    public class AudioMixerFloatSetting : Setting
    {
        #region Parameters

        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string nameParameter;

        [SerializeField] private float minRealValue;
        [SerializeField] private float maxRealValue;

        [SerializeField] private float virtualStep;
        [SerializeField] private float minVirtualValue;
        [SerializeField] private float maxVirtualValue;

        private float currentValue = 0;

        #endregion

        #region API

        private void AddValue(float value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        private void RemoveValue(float value)
        {
            currentValue -= value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        #region Unity API



        #endregion

        #region Public API

        public override bool isMinValue => currentValue == minRealValue;

        public override bool isMaxValue => currentValue == maxRealValue;

        public override void SetNextValue()
        {
            AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
            Apply();
        }

        public override void SetPreviousValue()
        {
            RemoveValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
            Apply();
        }

        public override string GetStringValue()
        {
            return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
        }

        public override object GetValue()
        {
            return currentValue;
        }

        public override void Apply()
        {
            audioMixer.SetFloat(nameParameter, currentValue);
            Save();
        }

        public override void Load()
        {
            currentValue = PlayerPrefs.GetFloat("AudioMixerFloatSetting", 0);
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("AudioMixerFloatSetting", currentValue);
        }

        #endregion

        #endregion
    }
}