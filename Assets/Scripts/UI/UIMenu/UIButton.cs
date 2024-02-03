// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Отслеживает события указателя (наведение, клика, отведения) и может иметь фокус. Но фокусом сам не управляет.
    /// </summary>
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Parameters

        [SerializeField] protected bool interactible;

        private bool focuse = false;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public bool Focuse => focuse;

        /// <summary>
        /// Фокусируется на кнопке.
        /// </summary>
        public virtual void SetFocuse()
        {
            if (!interactible) return;

            focuse = true;
        }

        /// <summary>
        /// Расфокусируется на кнопке.
        /// </summary>
        public virtual void SetUnFocuse()
        {
            if (!interactible) return;

            focuse = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactible) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!interactible) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactible) return;

            PointerClick?.Invoke(this);

            OnClick?.Invoke();
        }

        #endregion

        #endregion
    }
}