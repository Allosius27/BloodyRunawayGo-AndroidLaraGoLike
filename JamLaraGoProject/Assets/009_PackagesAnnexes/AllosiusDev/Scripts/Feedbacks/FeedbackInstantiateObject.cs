using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev
{
    [Serializable]
    public class FeedbackInstantiateObject : BaseFeedback
    {
        public GameObject objectToInstantiate;
        public Vector3 objectPositionOffset;

        public override IEnumerator Execute(GameObject _owner)
        {
            if(target != null)
            {
                GameObject _feedbackInstantiate = GameObject.Instantiate(objectToInstantiate, target.position + objectPositionOffset, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Target is null");
                GameObject _feedbackInstantiate = GameObject.Instantiate(objectToInstantiate, _owner.transform.position + objectPositionOffset, Quaternion.identity);
            }

            return base.Execute(_owner);
        }
    }
}
