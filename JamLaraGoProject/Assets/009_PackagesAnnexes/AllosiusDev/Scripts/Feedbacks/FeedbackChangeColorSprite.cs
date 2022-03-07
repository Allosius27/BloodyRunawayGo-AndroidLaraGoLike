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
    public class FeedbackChangeColorSprite : BaseFeedback
    {
        public Color newSpriteColor;

        public override IEnumerator Execute(GameObject _owner)
        {
            if (IsActive)
            {
                if(spriteRenderer != null)
                {
                    Debug.Log("ChangeColor");
                    spriteRenderer.color = newSpriteColor;
                }
                else
                {
                    Debug.LogWarning("SpriteRenderer is null");
                }
            }
            return base.Execute(_owner);
        }
    }
}
