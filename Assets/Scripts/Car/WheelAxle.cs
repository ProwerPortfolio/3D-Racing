// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityStandardAssets.Utility;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Физика колёсной оси.
    /// </summary>
    [Serializable]
    public class WheelAxle
    {
        #region Parameters

        /// <summary>
        /// Ссылка на WheelCollider левого колеса.
        /// </summary>
        [SerializeField] private WheelCollider leftWheelCollider;
        /// <summary>
        /// Ссылка на WheelCollider правого колеса.
        /// </summary>
        [SerializeField] private WheelCollider rightWheelCollider;

        /// <summary>
        /// Ссылка на Transform левого колеса.
        /// </summary>
        [SerializeField] private Transform leftWheelMesh;
        /// <summary>
        /// Ссылка на Transform правого колеса.
        /// </summary>
        [SerializeField] private Transform rightWheelMesh;

        /// <summary>
        /// Является ли колесо крутящимся?
        /// </summary>
        [SerializeField] private bool isMotor;

        /// <summary>
        /// Является ли колесо поворачивающимся?
        /// </summary>
        [SerializeField] private bool isSteer;

        /// <summary>
        /// Является ли колесо тормозящим?
        /// </summary>
        [SerializeField] private bool isBrake;

        /// <summary>
        /// Ширина колеса автомобиля.
        /// </summary>
        [SerializeField] private float wheelWidth;

        /// <summary>
        /// Сила стабилизации поперечной устойчивости.
        /// </summary>
        [SerializeField] private float antiRollForce;

        /// <summary>
        /// Дополнительная сила вниз для колёс.
        /// </summary>
        [SerializeField] private float additionalWheelDownForce;

        /// <summary>
        /// Базовая прямолинейная сила трения.
        /// </summary>
        [SerializeField] private float baseForwardStiffness;

        /// <summary>
        /// Коэффициент прямолинейной стабильности.
        /// </summary>
        [SerializeField] private float stabilityForwardFactor;

        /// <summary>
        /// Базовая боковая сила трения.
        /// </summary>
        [SerializeField] private float baseSidewaysStiffness;

        /// <summary>
        /// Коэффициент боковой стабильности.
        /// </summary>
        [SerializeField] private float stabilitySidewaysFactor;

        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;

        #endregion

        #region API

        /// <summary>
        /// Синхронизирует коллайдеры и трансформы.
        /// </summary>
        private void SyncMeshTransform()
        {
            UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
            UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
        }

        /// <summary>
        /// Синхронизирует коллайдер и трансформу какого-либо колеса.
        /// </summary>
        private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;

            wheelCollider.GetWorldPose(out position, out rotation);

            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }

        /// <summary>
        /// Обновляет рейкасты колёс.
        /// </summary>
        private void UpdateWheelHits()
        {
            leftWheelCollider.GetGroundHit(out leftWheelHit);
            rightWheelCollider.GetGroundHit(out rightWheelHit);
        }

        /// <summary>
        /// Стабилизатор поперечной устойчивости автомобиля.
        /// </summary>
        private void ApplyAntiRoll()
        {
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (leftWheelCollider.isGrounded)
                travelL = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;

            if (rightWheelCollider.isGrounded)
                travelR = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;

            float forceDir = (travelL - travelR);

            if (leftWheelCollider.isGrounded)
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * -forceDir * antiRollForce, leftWheelCollider.transform.position);

            if (rightWheelCollider.isGrounded)
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * forceDir * antiRollForce, rightWheelCollider.transform.position);
        }

        /// <summary>
        /// Прижимная сила автомобиля.
        /// </summary>
        private void ApplyDownForce()
        {
            if (leftWheelCollider.isGrounded)
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce * leftWheelCollider.attachedRigidbody.velocity.magnitude, leftWheelCollider.transform.position);

            if (rightWheelCollider.isGrounded)
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce * rightWheelCollider.attachedRigidbody.velocity.magnitude, rightWheelCollider.transform.position);
        }

        /// <summary>
        /// Коррекция трения колёс автомобиля.
        /// </summary>
        private void CorrectStiffness()
        {
            WheelFrictionCurve leftForward = leftWheelCollider.forwardFriction;
            WheelFrictionCurve rightForward = rightWheelCollider.forwardFriction;

            WheelFrictionCurve leftSideways = leftWheelCollider.sidewaysFriction;
            WheelFrictionCurve rightSideways = rightWheelCollider.sidewaysFriction;

            leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
            leftForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

            leftSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewaysFactor;
            rightSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewaysFactor;

            leftWheelCollider.forwardFriction = leftForward;
            rightWheelCollider.forwardFriction = rightForward;

            leftWheelCollider.sidewaysFriction = leftSideways;
            rightWheelCollider.sidewaysFriction = rightSideways;
        }

        #region Public API

        public bool IsMotor => isMotor;

        public bool IsSteer => isSteer;

        public bool IsBrake => isBrake;


        /// <summary>
        /// Обновляет параметры.
        /// </summary>
        public void Update()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            ApplyDownForce();
            CorrectStiffness();

            SyncMeshTransform();
        }

        public void ConfigureVechicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepsAboveThreshold)
        {
            leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
            rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
        }

        /// <summary>
        /// Применяет данные поворота колеса непосредственно к колесу.
        /// Использует угол Аккермана.
        /// </summary>
        /// <param name="steerAngle">Угол поворота колеса.</param>
        /// <param name="wheelBaseLength">Длина колёсной базы.</param>
        public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
        {
            if (!isSteer) return;

            float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
            float angleSign = Mathf.Sign(steerAngle);

            if (steerAngle > 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth + 0.5f))) * angleSign;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth + 0.5f))) * angleSign;
            }
            else if (steerAngle < 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth + 0.5f))) * angleSign;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth + 0.5f))) * angleSign;
            }
            else
            {
                leftWheelCollider.steerAngle = 0;
                rightWheelCollider.steerAngle = 0;
            }
        }

        /// <summary>
        /// Применяет данные крутящего момента колеса непосредственно к колесу.
        /// </summary>
        /// <param name="motorTorque">Сила крутящего момента колеса.</param>
        public void ApplyMotorTorque(float motorTorque)
        {
            if (!isMotor) return;

            leftWheelCollider.motorTorque = motorTorque;
            rightWheelCollider.motorTorque = motorTorque;
        }

        /// <summary>
        /// Применяет данные тормозного момента колеса непосредственно к колесу.
        /// </summary>
        /// <param name="brakeTorque">Сила тормозного момента колеса.</param>
        public void ApplyMotorBrake(float brakeTorque)
        {
            if (!isBrake) return;

            leftWheelCollider.brakeTorque = brakeTorque;
            rightWheelCollider.brakeTorque = brakeTorque;
        }

        /// <summary>
        /// Получает количество оборотов колеса.
        /// </summary>
        /// <returns>Количество оборотов колеса.</returns>
        public float GetAvarageRpm()
        {
            return (leftWheelCollider.rpm + rightWheelCollider.rpm) * 0.5f;
        }

        /// <summary>
        /// Получает радиус левого колеса.
        /// </summary>
        /// <returns>Радиус левого колеса.</returns>
        public float GetRadius()
        {
            return leftWheelCollider.radius;
        }

        #endregion

        #endregion
    }
}