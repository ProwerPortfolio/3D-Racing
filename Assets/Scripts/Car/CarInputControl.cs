// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Управление автомобилем.
    /// </summary>
    public class CarInputControl : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на автомобиль.
        /// </summary>
        [SerializeField] private Car car;

        /// <summary>
        /// Кривая плавного торможения.
        /// </summary>
        [SerializeField] private AnimationCurve brakeCurve;

        /// <summary>
        /// Кривая поворота руля в зависимости от скорости.
        /// </summary>
        [SerializeField] private AnimationCurve steerCurve;

        /// <summary>
        /// Коэффициент авто торможения при отпущенной педали газа.
        /// </summary>
        [SerializeField] [Range(0.0f, 1.0f)] private float autoBrakeStrength;

        /// <summary>
        /// Включено ли автоматическое торможение при отпущенной педали газа?
        /// </summary>
        [SerializeField] private bool autoBrake;

        #endregion

        #region API

        /// <summary>
        /// Автоматическое торможение, при условии, что игрок не нажимает газ.
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
        /// Полностью останавливает автомобиль.
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