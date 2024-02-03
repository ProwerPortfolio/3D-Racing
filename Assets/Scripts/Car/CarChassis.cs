// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using Unity.VisualScripting;
using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Шасси автомобиля.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на колёсные оси автомобиля.
        /// </summary>
        [SerializeField] private WheelAxle[] wheelAxles;

        /// <summary>
        /// Длина колёсной базы.
        /// </summary>
        [SerializeField] private float wheelBaseLength;

        /// <summary>
        /// Центр массы автомобиля.
        /// </summary>
        [SerializeField] private Transform centerOfMass;

        /// <summary>
        /// Минимальное значение параметра сопротивления воздуха автомобиля.
        /// </summary>
        [Header("AngularDrag")]
        [SerializeField] private float angularDragMin;
        /// <summary>
        /// Максимальное значение параметра сопротивления воздуха автомобиля.
        /// </summary>
        [SerializeField] private float angularDragMax;
        /// <summary>
        /// Коэффициент значения параметра сопротивления воздуха автомобиля.
        /// </summary>
        [SerializeField] private float angularDragFactor;

        /// <summary>
        /// Минимальное значение параметра прижимной силы автомобиля.
        /// </summary>
        [Header("DownForce")]
        [SerializeField] private float downForceMin;
        /// <summary>
        /// Максимальное значение параметра прижимной силы автомобиля.
        /// </summary>
        [SerializeField] private float downForceMax;
        /// <summary>
        /// Коэффициент значения параметра прижимной силы автомобиля.
        /// </summary>
        [SerializeField] private float downForceFactor;

        /// <summary>
        /// Значение максимального крутящего момента.
        /// </summary>
        public float motorTorque;

        /// <summary>
        /// Значение максимального угла поворота колеса.
        /// </summary>
        public float steerAngle;

        /// <summary>
        /// Значение максимального тормозного момента.
        /// </summary>
        public float brakeTorque;

        private new Rigidbody rigidbody;

        #endregion

        #region API

        /// <summary>
        /// Обновление колесных осей автомобиля.
        /// </summary>
        private void UpdateWheelAxles()
        {
            int motorWheelAmount = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                if (wheelAxles[i].IsMotor)      
                    motorWheelAmount += 2;
            }

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].ApplyMotorTorque(motorTorque / motorWheelAmount);
                wheelAxles[i].ApplySteerAngle(steerAngle, wheelBaseLength);
                wheelAxles[i].ApplyMotorBrake(brakeTorque);

                wheelAxles[i].Update();
            }
        }

        /// <summary>
        /// Сопротивление вращению автомобиля.
        /// </summary>
        private void UpdateAngularDrag()
        {
            rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
        }

        /// <summary>
        /// Сопротивление воздуха - прижимная сила автомобиля.
        /// </summary>
        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
            rigidbody.AddForce(-transform.up * downForce);
        }

        #region Unity API

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (centerOfMass != null)
                rigidbody.centerOfMass = centerOfMass.localPosition;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                wheelAxles[i].ConfigureVechicleSubsteps(50, 50, 50);
            }
        }

        private void FixedUpdate()
        {
            UpdateWheelAxles();
            UpdateAngularDrag();
            UpdateDownForce();
        }

        public void Reset()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        #endregion

        #region Public API

        public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

        public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

        /// <summary>
        /// Получает среднее вращение всех колёс.
        /// </summary>
        /// <returns>Среднее вращение всех колёс.</returns>
        public float GetAverageRpm()
        {
            float sum = 0;

            for (int i = 0; i < wheelAxles.Length; i++)
            {
                sum += wheelAxles[i].GetAvarageRpm();
            }

            return sum / wheelAxles.Length;
        }

        /// <summary>
        /// Получает скорость по колёсам.
        /// </summary>
        /// <returns>Скорость по колёсам.</returns>
        public float GetWheelSpeed()
        {
            return GetAverageRpm() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
        }

        #endregion

        #endregion
    }
}