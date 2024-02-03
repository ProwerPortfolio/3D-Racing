// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace Racing3D
{
    /// <summary>
    /// � ������ ��������� ������ �������� �������� ������, � ����� ������ ����� � ���������.
    /// </summary>
    public class UISelectableButton : UIButton
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������� ������ ������.
        /// </summary>
        [SerializeField] private Image selectImage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnSelect;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public override void SetFocuse()
        {
            base.SetFocuse();

            selectImage.gameObject.SetActive(true);

            OnSelect?.Invoke();
        }

        public override void SetUnFocuse()
        {
            base.SetUnFocuse();

            selectImage.gameObject.SetActive(false);

            OnUnSelect?.Invoke();
        }

        #endregion

        #endregion
    }
}