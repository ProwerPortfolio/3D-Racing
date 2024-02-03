// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ���������� �����������.
    /// </summary>
    public class CarInputControl : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ����������.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// ������ �������� ����������.
        /// </summary>
        [SerializeField] private AnimationCurve brakeCurve;

        /// <summary>
        /// ������ �������� ���� � ����������� �� ��������.
        /// </summary>
        [SerializeField] private AnimationCurve steerCurve;

        /// <summary>
        /// ����������� ���� ���������� ��� ���������� ������ ����.
        /// </summary>
        [SerializeField] [Range(0.0f, 1.0f)] private float autoBrakeStrength;

        /// <summary>
        /// �������� �� �������������� ���������� ��� ���������� ������ ����?
        /// </summary>
        [SerializeField] private bool autoBrake;

        #endregion

        #region API

        /// <summary>
        /// �������������� ����������, ��� �������, ��� ����� �� �������� ���.
        /// </summary>
        private void UpdateAutoBrake()
        {
            if (Input.GetAxis("Vertical") == 0 && autoBrake)
            {
                car.brakeControl = brakeCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * autoBrakeStrength;
            }
        }

        #region Unity API

        private void Update()
        {
            float verticalAxis = Input.GetAxis("Vertical");

            if (Input.GetAxis("Jump") == 0)
            {
                if (Mathf.Sign(verticalAxis) == Mathf.Sign(car.WheelSpeed) || Mathf.Abs(car.WheelSpeed) < 0.5f)
                {
                    car.throttleControl = Mathf.Abs(verticalAxis);
                    car.brakeControl = 0;
                }
                else
                {
                    car.throttleControl = 0;
                    car.brakeControl = brakeCurve.Evaluate(car.WheelSpeed / car.MaxSpeed);
                }
            }
            else
            {
                car.brakeControl = Input.GetAxis("Jump");
            }
            
            if (verticalAxis < 0 && car.WheelSpeed > -0.5f && car.WheelSpeed < 0.5f)
            {
                car.ShiftToReverseGear();
            }

            if (verticalAxis > 0 && car.WheelSpeed > -0.5f && car.WheelSpeed < 0.5f)
            {
                car.ShiftToFirstGear();
            }

            car.steerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * Input.GetAxis("Horizontal");

            UpdateAutoBrake();
        }

        public void Reset()
        {
            car.throttleControl = 0;
            car.steerControl = 0;
            car.brakeControl = 0;
        }

        #endregion

        #region Public API

        /// <summary>
        /// ��������� ������������� ����������.
        /// </summary>
        public void Stop()
        {
            car.throttleControl = 0;
            car.steerControl = 0;
            car.brakeControl = 1;
        }

        #endregion

        #endregion
    }
}