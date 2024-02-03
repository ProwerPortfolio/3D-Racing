// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{

    /// <summary>
    /// ќбеспечивает зависимости дл€ глобальных скриптов.
    /// </summary>
    public class SceneDependenciesContainer : Dependency
    {
        #region Parameters

        /// <summary>
        /// —сылка на RaceStateTracker.
        /// </summary>
        [SerializeField] private RaceStateTracker raceStateTracker;

        /// <summary>
        /// —сылка на RaceTimeTracker.
        /// </summary>
        [SerializeField] private RaceTimeTracker raceTimeTracker;

        /// <summary>
        /// —сылка на RaceResultTime.
        /// </summary>
        [SerializeField] private RaceResultTime raceResultTime;

        /// <summary>
        /// —сылка на CarInputControl.
        /// </summary>
        [SerializeField] private CarInputControl carInputControl;

        /// <summary>
        /// —сылка на TrackPointCircuit.
        /// </summary>
        [SerializeField] private TrackPointCircuit trackPointCircuit;

        /// <summary>
        /// —сылка на автомобиль.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// —сылка на CarCameraController.
        /// </summary>
        [SerializeField] private CarCameraController carCameraController;

        #endregion

        #region API

        protected override void BindAll(MonoBehaviour monoBehaviour)
        {
            Bind<RaceStateTracker>(raceStateTracker, monoBehaviour);

            Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviour);

            Bind<RaceResultTime>(raceResultTime, monoBehaviour);

            Bind<CarInputControl>(carInputControl, monoBehaviour);

            Bind<TrackPointCircuit>(trackPointCircuit, monoBehaviour);

            Bind<Car>(car, monoBehaviour);

            Bind<CarCameraController>(carCameraController, monoBehaviour);
        }

        #region Unity API

        private void Awake()
        {
            FindAllObjectToBind();
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}