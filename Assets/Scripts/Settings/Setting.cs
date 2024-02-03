// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Настройка.
    /// </summary>
    public abstract class Setting : ScriptableObject
    {
        #region Parameters

        [SerializeField] protected string title;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public virtual bool isMinValue { get; }

        public virtual bool isMaxValue { get; }

        public string Title => title;

        public virtual void SetNextValue() { }

        public virtual void SetPreviousValue() { }

        public virtual object GetValue()
        {
            return default(object);
        }

        public virtual string GetStringValue()
        {
            return string.Empty;
        }

        public virtual void Apply() { }

        public virtual void Load() { }

        #endregion

        #endregion
    }
}