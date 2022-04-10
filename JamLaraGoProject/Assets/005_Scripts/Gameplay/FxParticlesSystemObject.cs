using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxParticlesSystemObject : MonoBehaviour
{
    #region UnityInspector

    [SerializeField] private bool destroyAfterLifeTime = true;

    [SerializeField] private ParticleSystem particles;

    #endregion

    #region Behaviour

    private void Start()
    {
        if(destroyAfterLifeTime)
        {
            Destroy(gameObject, particles.main.duration);
        }
    }

    #endregion
}
