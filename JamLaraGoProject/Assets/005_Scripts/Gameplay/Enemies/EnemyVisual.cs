using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private Enemy enemy;

    #endregion

    #region Behaviour

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(enemy.BulletPrefab);
        bullet.transform.SetParent(enemy.BulletPoint);
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;

        bullet.GetComponent<BulletEnemy>().Graphics.transform.localEulerAngles = transform.localEulerAngles;

        Vector3 direction = enemy.RangePoint.TransformDirection(Vector3.forward);
        bullet.GetComponent<BulletEnemy>().direction = direction;
    }

    #endregion
}
