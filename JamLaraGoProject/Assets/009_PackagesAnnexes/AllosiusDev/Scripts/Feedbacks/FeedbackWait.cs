using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AllosiusDev
{
    [Serializable]
    public class FeedbackWait : BaseFeedback
    {
        public float waitTime;

        public override IEnumerator Execute(GameObject _owner)
        {
            if (IsActive)
            {
                Debug.Log("Wait");
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
