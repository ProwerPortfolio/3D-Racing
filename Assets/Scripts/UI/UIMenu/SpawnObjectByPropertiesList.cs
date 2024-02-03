// Created and owned by Sankoh_Tew. Hi, dataminers! ;)

#region Usings

using UnityEngine;

#endregion

namespace Racing3D
{
    /// <summary>
    /// Скрипт, который спавнит объекты по настройкам.
    /// </summary>
    public class SpawnObjectByPropertiesList : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;
        [SerializeField] private ScriptableObject[] properties;

        #endregion

        #region API



        #region Unity API



        #endregion

        #region Public API

        [ContextMenu(nameof(SpawnInEditMode))]
        public void SpawnInEditMode()
        {
            if (Application.isPlaying) return;

            GameObject[] allObject = new GameObject[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                allObject[i] = parent.GetChild(i).gameObject;
            }

            for (int i = 0; i < allObject.Length; i++)
            {
                DestroyImmediate(allObject[i]);
            }


            for (int i = 0; i < properties.Length; i++)
            {
                GameObject gameObject = Instantiate(prefab, parent);

                IScriptableObjectProperty scriptableObjectProperty = gameObject.GetComponent<IScriptableObjectProperty>();

                scriptableObjectProperty.ApplyProperty(properties[i]);
            }
        }

        #endregion

        #endregion
    }
}