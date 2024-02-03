// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// –еспавнер автомобил€.
    /// </summary>
    public class CarRespawner : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
    {
        #region Parameters

        [SerializeField] private float respawnHeight;

        private TrackPoint respawnTrackPoint;

        /// <summary>
        /// —сылка на RaceStateTracker.
        /// </summary>
        private RaceStateTracker raceStateTracker;

        /// <summary>
        /// —сылка на CarInputControl.
        /// </summary>
        private CarInputControl carInputControl;

        /// <summary>
        /// —сылка на автомобиль.
        /// </summary>
        private Car car;

        #endregion

        #region API

        private void OnTrackPointPassed(TrackPoint point)
        {
            respawnTrackPoint = point;
        }

        #region Unity API

        private void Start()
        {
            raceStateTracker.TrackPointPassed += OnTrackPointPassed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }

        private void OnDestroy()
        {
            raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
        }

        #endregion

        #region Public API

        public void Construct(RaceStateTracker t)
        {
            raceStateTracker = t;
        }

        public void Construct(Car t)
        {
            car = t;
        }

        public void Construct(CarInputControl t)
        {
            carInputControl = t;
        }

        public void Respawn()
        {
            Quaternion normalRotation = new Quaternion(respawnTrackPoint.transform.rotation.x, respawnTrackPoint.transform.rotation.y - 180, respawnTrackPoint.transform.rotation.z, respawnTrackPoint.transform.rotation.w);

            if (!respawnTrackPoint) return;

            if (raceStateTracker.State != RaceState.Race) return;

            car.Respawn(respawnTrackPoint.transform.position + respawnTrackPoint.transform.up * respawnHeight, normalRotation);

            carInputControl.Reset();
        }

        #endregion

        #endregion
    }
}