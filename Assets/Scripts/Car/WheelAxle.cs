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
    /// ������ ������� ���.
    /// </summary>
    [Serializable]
    public class WheelAxle
    {
        #region Parameters

        /// <summary>
        /// ������ �� WheelCollider ������ ������.
        /// </summary>
        [SerializeField] private WheelCollider leftWheelCollider;
        /// <summary>
        /// ������ �� WheelCollider ������� ������.
        /// </summary>
        [SerializeField] private WheelCollider rightWheelCollider;

        /// <summary>
        /// ������ �� Transform ������ ������.
        /// </summary>
        [SerializeField] private Transform leftWheelMesh;
        /// <summary>
        /// ������ �� Transform ������� ������.
        /// </summary>
        [SerializeField] private Transform rightWheelMesh;

        /// <summary>
        /// �������� �� ������ ����������?
        /// </summary>
        [SerializeField] private bool isMotor;

        /// <summary>
        /// �������� �� ������ ����������������?
        /// </summary>
        [SerializeField] private bool isSteer;

        /// <summary>
        /// �������� �� ������ ����������?
        /// </summary>
        [SerializeField] private bool isBrake;

        /// <summary>
        /// ������ ������ ����������.
        /// </summary>
        [SerializeField] private float wheelWidth;

        /// <summary>
        /// ���� ������������ ���������� ������������.
        /// </summary>
        [SerializeField] private float antiRollForce;

        /// <summary>
        /// �������������� ���� ���� ��� ����.
        /// </summary>
        [SerializeField] private float additionalWheelDownForce;

        /// <summary>
        /// ������� ������������� ���� ������.
        /// </summary>
        [SerializeField] private float baseForwardStiffness;

        /// <summary>
        /// ����������� ������������� ������������.
        /// </summary>
        [SerializeField] private float stabilityForwardFactor;

        /// <summary>
        /// ������� ������� ���� ������.
        /// </summary>
        [SerializeField] private float baseSidewaysStiffness;

        /// <summary>
        /// ����������� ������� ������������.
        /// </summary>
        [SerializeField] private float stabilitySidewaysFactor;

        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;

        #endregion

        #region API

        /// <summary>
        /// �������������� ���������� � ����������.
        /// </summary>
        private void SyncMeshTransform()
        {
            UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
            UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
        }

        /// <summary>
        /// �������������� ��������� � ���������� ������-���� ������.
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
        /// ��������� �������� ����.
        /// </summary>
        private void UpdateWheelHits()
        {
            leftWheelCollider.GetGroundHit(out leftWheelHit);
            rightWheelCollider.GetGroundHit(out rightWheelHit);
        }

        /// <summary>
        /// ������������ ���������� ������������ ����������.
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
        /// ��������� ���� ����������.
        /// </summary>
        private void ApplyDownForce()
        {
            if (leftWheelCollider.isGrounded)
                leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce * leftWheelCollider.attachedRigidbody.velocity.magnitude, leftWheelCollider.transform.position);

            if (rightWheelCollider.isGrounded)
                rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce * rightWheelCollider.attachedRigidbody.velocity.magnitude, rightWheelCollider.transform.position);
        }

        /// <summary>
        /// ��������� ������ ���� ����������.
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
        /// ��������� ���������.
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
        /// ��������� ������ �������� ������ ��������������� � ������.
        /// ���������� ���� ���������.
        /// </summary>
        /// <param name="steerAngle">���� �������� ������.</param>
        /// <param name="wheelBaseLength">����� ������� ����.</param>
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
        /// ��������� ������ ��������� ������� ������ ��������������� � ������.
        /// </summary>
        /// <param name="motorTorque">���� ��������� ������� ������.</param>
        public void ApplyMotorTorque(float motorTorque)
        {
            if (!isMotor) return;

            leftWheelCollider.motorTorque = motorTorque;
            rightWheelCollider.motorTorque = motorTorque;
        }

        /// <summary>
        /// ��������� ������ ���������� ������� ������ ��������������� � ������.
        /// </summary>
        /// <param name="brakeTorque">���� ���������� ������� ������.</param>
        public void ApplyMotorBrake(float brakeTorque)
        {
            if (!isBrake) return;

            leftWheelCollider.brakeTorque = brakeTorque;
            rightWheelCollider.brakeTorque = brakeTorque;
        }

        /// <summary>
        /// �������� ���������� �������� ������.
        /// </summary>
        /// <returns>���������� �������� ������.</returns>
        public float GetAvarageRpm()
        {
            return (leftWheelCollider.rpm + rightWheelCollider.rpm) * 0.5f;
        }

        /// <summary>
        /// �������� ������ ������ ������.
        /// </summary>
        /// <returns>������ ������ ������.</returns>
        public float GetRadius()
        {
            return leftWheelCollider.radius;
        }

        #endregion

        #endregion
    }
}