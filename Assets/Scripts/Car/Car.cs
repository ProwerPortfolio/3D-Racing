// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Автомобиль.
    /// </summary>
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// Ссылка на шасси автомобиля.
        /// </summary>
        private CarChassis chassis;
        
        /// <summary>
        /// Значение максимального угла поворота колеса.
        /// </summary>
        [SerializeField] private float maxSteerAngle;
        
        /// <summary>
        /// Значение максимального тормозного момента.
        /// </summary>
        [SerializeField] private float maxBreakTorque;

        /// <summary>
        /// Кривая крутящего момента в зависимости от оборотов двигателя.
        /// </summary>
        [Header("Engine")]
        [SerializeField] private AnimationCurve engineTorqueCurve;
        /// <summary>
        /// Максимальный крутящий момент двигателя.
        /// </summary>
        [SerializeField] private float engineMaxTorque;
        /// <summary>
        /// Текущий крутящий момент двигателя.
        /// </summary>
        private float engineTorque;
        /// <summary>
        /// Количество текущих оборотов двигателя.
        /// </summary>
        private float engineRpm;
        /// <summary>
        /// Минимальное количество оборотов двигателя.
        /// </summary>
        [SerializeField] private float engineMinRpm;
        /// <summary>
        /// Максимальное количество оборотов двигателя.
        /// </summary>
        [SerializeField] private float engineMaxRpm;

        /// <summary>
        /// Массив множителей передач, на которые умножается крутящий момент.
        /// </summary>
        [Header("Gearbox")]
        [SerializeField] private float[] gears;
        /// <summary>
        /// Множитель финальной передачи - дифференциала.
        /// </summary>
        [SerializeField] private float finalDriveRatio;

        /// <summary>
        /// Текущий множитель передачи коробки передач.
        /// </summary>
        private float selectedGear;

        /// <summary>
        /// Номер текущей передачи коробки передач.
        /// </summary>
        private int selectedGearIndex;

        /// <summary>
        /// Множитель передачи заднего хода коробки передач.
        /// </summary>
        [SerializeField] private float rearGear;

        /// <summary>
        /// Количество оборотов двигателя, при котором коробка передач должна переключиться выше.
        /// </summary>
        [SerializeField] private float upShiftEngineRpm;

        /// <summary>
        /// Количество оборотов двигателя, при котором коробка передач должна переключиться ниже.
        /// </summary>
        [SerializeField] private float downShiftEngineRpm;

        /// <summary>
        /// Максимальная скорость автомобиля.
        /// </summary>
        [SerializeField] private float maxSpeed;

        /// <summary>
        /// Сила нажатия на педаль газа автомобиля.
        /// </summary>
        public float throttleControl;

        /// <summary>
        /// Сила поворота руля автомобиля.
        /// </summary>
        public float steerControl;

        /// <summary>
        /// Сила нажатия на педаль тормоза автомобиля.
        /// </summary>
        public float brakeControl;

        [SerializeField] private float linearVelocity;

        /// <summary>
        /// Ссылка на апдейтер интерфейса на автомобиле.
        /// </summary>
        [SerializeField] private UICarUpdater uIupdater;

        /// <summary>
        /// Ссылка на AudioSource со звуком переключения передачи.
        /// </summary>
        [SerializeField] private AudioSource shiftGearBoxSound;

        #endregion

        #region API

        /// <summary>
        /// Обновление крутящего момента двигателя.
        /// </summary>
        private void UpdateEngineTorque()
        {
            engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);

            engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

            engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
        }

        /// <summary>
        /// Переключает передачу.
        /// </summary>
        /// <param name="gearIndex">Индекс передачи, на которую следует переключиться.</param>
        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);

            if (selectedGear != gears[gearIndex])
                shiftGearBoxSound.PlayOneShot(shiftGearBoxSound.clip);

            selectedGear = gears[gearIndex];

            selectedGearIndex = gearIndex; 
        }

        /// <summary>
        /// Автоматическое переключение коробки передач.
        /// </summary>
        private void AutoGearShip()
        {
            if (selectedGear < 0) return;

            if (engineRpm >= upShiftEngineRpm)
                UpGear();

            if (engineRpm < downShiftEngineRpm)
                DownGear();
        }

        #region Unity API

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            linearVelocity = LinearVelocity;

            UpdateEngineTorque();

            AutoGearShip();

            if (LinearVelocity >= maxSpeed)
                engineTorque = 0;

            chassis.motorTorque = engineTorque * throttleControl;
            chassis.steerAngle = maxSteerAngle * steerControl;
            chassis.brakeTorque = maxBreakTorque * brakeControl;

            uIupdater.SpeedTextUpdate(linearVelocity);
            uIupdater.GearTextUpdate(selectedGear, selectedGearIndex);
            uIupdater.GearProgressBarUpdate(engineRpm, engineMaxRpm);
        }

        public void Reset()
        {
            chassis.Reset();

            chassis.motorTorque = 0;
            chassis.brakeTorque = 0;
            chassis.steerAngle = 0;

            throttleControl = 0;
            brakeControl = 0;
            steerControl = 0;
        }

        #endregion

        #region Public API

        public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody : chassis.Rigidbody;

        public float LinearVelocity => chassis.LinearVelocity;

        public float WheelSpeed => chassis.GetWheelSpeed();

        public float MaxSpeed => maxSpeed;

        public float EngineRpm => engineRpm;

        public float EngineMaxRpm => engineMaxRpm;

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        #region Gearbox

        /// <summary>
        /// Поднять передачу.
        /// </summary>
        public void UpGear()
        {
            ShiftGear(selectedGearIndex + 1);
        }

        /// <summary>
        /// Опустить передачу.
        /// </summary>
        public void DownGear()
        {
            ShiftGear(selectedGearIndex - 1);
        }

        /// <summary>
        /// Включить передачу заднего хода.
        /// </summary>
        public void ShiftToReverseGear()
        {
            selectedGear = rearGear;
        }

        /// <summary>
        /// Переключиться на первую передачу.
        /// </summary>
        public void ShiftToFirstGear()
        {
            ShiftGear(0);
        }

        /// <summary>
        /// Переключить коробку передач в нейтральное состояние.
        /// </summary>
        public void ShiftToNetral()
        {
            selectedGear = 0;
        }

        #endregion

        #endregion

        #endregion
    }
}