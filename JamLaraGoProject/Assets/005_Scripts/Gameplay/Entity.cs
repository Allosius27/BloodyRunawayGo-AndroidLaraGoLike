using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Properties

    public float distanceEnemyChecked { get; set; }

    public isObstacle IsObstacle;

    #endregion

    #region UnityInspector

    public enum isObstacle
    {
        NotObstacle,
        IsObstacle,
    }

    #endregion

    #region Behaviour

    public void ChangeIsObstacleState(isObstacle value)
    {
        IsObstacle = value;
        GameCore.Instance.UpdateEnemiesBehaviour();
    }

    #endregion
}
