// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Кнопка гонки.
    /// </summary>
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
    {
        #region Parameters

        [SerializeField] private RaceInfo raceInfo;

        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI title;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            ApplyProperty(raceInfo);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Применяет настройки.
        /// </summary>
        /// <param name="property">Настройки.</param>
        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is RaceInfo == false) return;

            raceInfo = property as RaceInfo;

            icon.sprite = raceInfo.Sprite;
            title.text = raceInfo.Title;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (raceInfo == null) return;

            SceneManager.LoadScene(raceInfo.SceneName);
        }

        #endregion

        #endregion
    }
}