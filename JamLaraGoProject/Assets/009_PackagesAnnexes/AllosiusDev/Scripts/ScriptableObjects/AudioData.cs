//
// Updated by Allosius(Yanis Q.) on 20/10/2021.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev
{
    [CreateAssetMenu(fileName = "New AudioData", menuName = "AllosiusDev/AudioData")]
    public class AudioData : ScriptableObject
    {
        #region UnityInspector

        public Sound sound;

        #endregion
    }
}
