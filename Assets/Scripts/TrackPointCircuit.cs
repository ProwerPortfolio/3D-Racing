// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Racing3D
{
    /// <summary>
    /// ��� ������.
    /// </summary>
    public enum TrackType
    {
        /// <summary>
        /// ���������.
        /// </summary>
        Circular,
        /// <summary>
        /// ������.
        /// </summary>
        Sprint
    }

    /// <summary>
    /// ���� ������� ����� �����.
    /// </summary>
    public class TrackPointCircuit : MonoBehaviour
    {
        #region Parameters

        public event UnityAction<TrackPoint> TrackPointTriggered;

        /// <summary>
        /// �������, ������� ���������� ��� ����������� �����.
        /// </summary>
        public event UnityAction<int> LapCompleted;

        /// <summary>
        /// ������ �� ��� ������.
        /// </summary>
        [SerializeField] private TrackType trackType;

        /// <summary>
        /// ������ ������ ������� ����� ������.
        /// </summary>
        private TrackPoint[] points;

        /// <summary>
        /// ���������� ����������� ������.
        /// </summary>
        private int lapsCompleted = -1;

        #endregion

        #region API

        /// <summary>
        /// ������������� �������� ������� ������� ����� �� �������.
        /// </summary>
        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            points = new TrackPoint[transform.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i).GetComponent<TrackPoint>();

                if (!points[i])
                {
                    Debug.LogError("���-�� �� ���. � ������� � ����.");
                    return;
                }

                points[i].Reset();
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].next = points[i + 1];
            }

            if (trackType == TrackType.Circular)
            {
                points[points.Length - 1].next = points[0];
            }

            points[0].isFirst = true;

            if (trackType == TrackType.Sprint)
            {
                points[points.Length - 1].isLast = true;
            }

            if (trackType == TrackType.Circular)
            {
                points[0].isLast = true;
            }
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (!trackPoint.IsTarget) return;

            trackPoint.Passed();
            trackPoint.next?.AssignAsTarget();

            TrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.isLast)
            {
                lapsCompleted++;

                if (trackType == TrackType.Sprint)
                {
                    LapCompleted?.Invoke(lapsCompleted);
                }

                if (trackType == TrackType.Circular)
                {
                    if (lapsCompleted > 0)
                    {
                        LapCompleted?.Invoke(lapsCompleted);
                    }
                }
            }
        }

        #region Unity API

        private void Awake()
        {
            BuildCircuit();
        }

        private void Start()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered += OnTrackPointTriggered;
            }

            points[0].AssignAsTarget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].Triggered -= OnTrackPointTriggered;
            }
        }

        #endregion

        #region Public API

        public TrackType TrackType => trackType;

        #endregion

        #endregion
    }
}