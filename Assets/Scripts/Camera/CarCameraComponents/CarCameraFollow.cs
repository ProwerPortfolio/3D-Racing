// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Скрипт слежения камеры.
    /// </summary>
    public class CarCameraFollow : CarCameraComponent
    {
        #region Parameters

        /// <summary>
        /// Угол наклона камеры.
        /// </summary>
        [Header("Offset")]
        [SerializeField] private float viewHeight;
        /// <summary>
        /// Высота камеры.
        /// </summary>
        [SerializeField] private float height;
        /// <summary>
        /// Дистанция от камеры до объекта слежения.
        /// </summary>
        [SerializeField] private float distance;

        /// <summary>
        /// Цель, за которой будет выполняться слежение.
        /// </summary>
        private Transform target;
        /// <summary>
        /// Ссылка на Rigidbody, чтобы брать скорость.
        /// </summary>
        private new Rigidbody rigidbody;

        [Header("Damping")]
        [SerializeField] private float rotationDamping;
        [SerializeField] private float heightDamping;
        [SerializeField] private float speedTreshold;

        #endregion

        #region API



        #region Unity API

        private void FixedUpdate()
        {
            Vector3 velocity = rigidbody.velocity;

            Vector3 targetRotation = target.eulerAngles;

            if (velocity.magnitude > speedTreshold)
            {
                targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
            }

            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
            float currentHeight = Mathf.Lerp(transform.position.y, target.position.y + height, heightDamping * Time.fixedDeltaTime);

            Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            transform.position = target.position - positionOffset;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            transform.LookAt(target.position + new Vector3(0, viewHeight, 0));
        }

        #endregion

        #region Public API

        public override void SetProperties(Car car, Camera camera)
        {
            base.SetProperties(car, camera);

            target = car.transform;
            rigidbody = car.Rigidbody; 
        }

        #endregion

        #endregion
    }
}