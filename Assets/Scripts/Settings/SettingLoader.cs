// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Сохранение игровых настроек.
    /// </summary>
    public class SettingLoader : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private Setting[] allSettings;

        #endregion

        #region API



        #region Unity API

        private void Awake()
        {
            for (int i = 0; i < allSettings.Length; i++)
            {
                allSettings[i].Load();
                allSettings[i].Apply();
            }
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}