using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AllosiusDev
{
    [Serializable]
    public class BaseFeedback
    {
        [Tooltip("Determines whether feedback is active or not")]
        public bool IsActive = true;

        public Vector3 target { get; protected set; }
        public bool hasTarget { get; protected set; }

        public SpriteRenderer spriteRenderer { get; protected set; }
        public Color spriteRendererBaseColor { get; protected set; }

        public virtual void SetTarget(Vector3 _target)
        {
            target = _target;
            hasTarget = true;
        }

        public virtual void SetSpriteRenderer(SpriteRenderer _targetRenderer)
        {
            spriteRenderer = _targetRenderer;
        }

        public virtual void SetSpriteRendererBaseColor(Color _baseColor)
        {
            spriteRendererBaseColor = _baseColor;
        }

        public virtual IEnumerator Execute(GameObject _owner)
        {
            yield break;
        }
    }
}
