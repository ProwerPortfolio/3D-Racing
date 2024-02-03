// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Стартует гонку с клавиатуры.
    /// </summary>
    public class RaceKeybordStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
        #region Parameters

        /// <summary>
        /// Ссылка на RaceStateTracker.
        /// </summary>
        private RaceStateTracker stateTracker;

        #endregion

        #region API



        #region Unity API

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stateTracker.LaunchPeparationStart();
            }
        }

        #endregion

        #region Public API

        public void Construct(RaceStateTracker t)
        {
            stateTracker = t;
        }

        #endregion

        #endregion
    }
}