// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Информация о заезде.
    /// </summary>
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        #region Parameters

        [SerializeField] private string sceneName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string title;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        public string SceneName => sceneName;
        public Sprite Sprite => sprite;
        public string Title => title;

        #endregion

        #endregion
    }
}