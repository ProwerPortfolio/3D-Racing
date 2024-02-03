// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ѕреобразует секунды в нормализованное врем€.
    /// </summary>
    public static class StringTime
    {
        #region Parameters

        

        #endregion

        #region API

        

        #region Unity API

        

        #endregion

        #region Public API

        public static string SecondToTimeString(float seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss\.ff");
        }

        #endregion

        #endregion
    }
}