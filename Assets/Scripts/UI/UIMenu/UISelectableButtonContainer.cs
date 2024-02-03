// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ������ ������ ������ � ��� ������� ���������� �� ������ ������� ����� � ��������� ������ � ������ ����� ������.
    /// </summary>
    public class UISelectableButtonContainer : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Transform, � �������� ����� �������� ������.
        /// </summary>
        [SerializeField] private Transform buttonsContainer;

        public bool interactable = true;

        private UISelectableButton[] buttons;

        private int selectButtonIndex = 0;

        #endregion

        #region API

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        private void SelectButton(UIButton button)
        {
            if (!interactable) return;

            buttons[selectButtonIndex].SetUnFocuse();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (button == buttons[i])
                {
                    selectButtonIndex = i;
                    button.SetFocuse();
                    break;
                }
            }
        }

        #region Unity API

        private void Start()
        {
            buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();

            if (buttons == null)
                Debug.LogError("������ ���. �����.");

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter += OnPointerEnter;
            }

            if (!interactable) return;

            buttons[selectButtonIndex].SetFocuse();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter -= OnPointerEnter;
            }
        }

        #endregion

        #region Public API

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
        }

        public void SelectNext()
        {

        }

        public void SelectPrevious()
        {

        }

        #endregion

        #endregion
    }
}