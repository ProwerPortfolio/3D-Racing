// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// �������� �� ������ ��������. ���������� �� � ������� ������.
    /// </summary>
    public class SuspensionArm : MonoBehaviour
    {
        #region Parameters

        /// <summary>
        /// ������ �� ��, ��� ����� ��������� ������.
        /// </summary>
        [SerializeField] private Transform target;
        /// <summary>
        /// ������, �� ������� ����� ����������� ��������.
        /// </summary>
        [SerializeField] private float factor;

        /// <summary>
        /// �������� ����������� �������.
        /// </summary>
        private float baseOffset;

        #endregion

        #region API



        #region Unity API

        private void Start()
        {
            baseOffset = target.localPosition.y;
        }

        private void Update()
        {
            transform.localEulerAngles = new Vector3(0, 0, (target.localPosition.y - baseOffset) * factor);
        }

        #endregion

        #region Public API



        #endregion

        #endregion
    }
}