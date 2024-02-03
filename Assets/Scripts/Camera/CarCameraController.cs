// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Контроллер камеры автомобиля.
    /// </summary>
    public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        #region Parameters

        private Car car;
        [SerializeField] private new Camera camera;
        [SerializeField] private CarCameraFollow follower;
        [SerializeField] private CarCameraShaker shaker;
        [SerializeField] private CarCameraFovCorrector fovCorrector;
        [SerializeField] private CarCameraPathFollower pathFollower;

        private RaceStateTracker stateTracker;

        #endregion

        #region API

        private void OnPeparationStarted()
        {
            follower.enabled = true;
            pathFollower.enabled = false;
        }

        private void OnCompleted()
        {
            pathFollower.enabled = true;
            pathFollower.StartMoveToNearestPoint();
            pathFollower.SetLookTarget(car.transform);

            follower.enabled = false;
        }

        #region Unity API

        private void Awake()
        {
            follower.SetProperties(car, camera);
            shaker.SetProperties(car, camera);
            fovCorrector.SetProperties(car, camera);
        }

        private void Start()
        {
            stateTracker.PeparationStarted += OnPeparationStarted;
            stateTracker.Completed += OnCompleted;

            follower.enabled = false;
            pathFollower.enabled = true;
        }

        private void OnDestroy()
        {
            stateTracker.PeparationStarted -= OnPeparationStarted;
            stateTracker.Completed -= OnCompleted;
        }

        #endregion

        #region Public API

        public void Construct(Car t)
        {
            car = t;
        }

        public void Construct(RaceStateTracker t)
        {
            stateTracker = t;
        }

        #endregion

        #endregion
    }
}