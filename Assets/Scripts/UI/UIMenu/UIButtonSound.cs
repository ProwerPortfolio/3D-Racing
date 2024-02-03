// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Менеджер звуков интерфейса.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class UIButtonSound : MonoBehaviour
    {
        #region Parameters

        private new AudioSource audio;

        [SerializeField] private AudioClip hover;

        [SerializeField] private AudioClip click;

        private UIButton[] uIButtons;

        #endregion

        #region API

        private void OnPointerEnter(UIButton button)
        {
            audio.PlayOneShot(hover);
        }

        private void OnPointerClicked(UIButton button)
        {
            audio.PlayOneShot(click);
        }

        #region Unity API

        private void Start()
        {
            audio = GetComponent<AudioSource>();

            uIButtons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < uIButtons.Length; i++)
            {
                uIButtons[i].PointerEnter += OnPointerEnter;
                uIButtons[i].PointerClick += OnPointerClicked;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < uIButtons.Length; i++)
            {
                uIButtons[i].PointerEnter -= OnPointerEnter;
                uIButtons[i].PointerClick -= OnPointerClicked;
            }
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}