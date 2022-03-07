#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace AllosiusDev
{

    [CreateAssetMenu(fileName = "New FeedbacksData", menuName = "AllosiusDev/FeedbacksData")]
    public class FeedbacksData : SerializedScriptableObject
    {
        [TabGroup("Time")]
        [GUIColor(1, 0.92f, 0.016f)]
        [Button(ButtonSizes.Medium)]
        public void AddFeedbackWait()
        {
            Feedback feedback = new Feedback();
            feedback.Type = Feedback.FeedbackType.Wait;
            feedback.IsFeedbackWait = true;
            feedback.Initialized = true;
            feedbacks.Add(feedback);
        }

        [TabGroup("Sound")]
        [GUIColor(0, 1, 0)]
        [Button(ButtonSizes.Medium)]
        public void AddFeedbackPlaySound()
        {
            Feedback feedback = new Feedback();
            feedback.Type = Feedback.FeedbackType.PlaySound;
            feedback.IsFeedbackPlaySound = true;
            feedback.Initialized = true;
            feedbacks.Add(feedback);
        }


        [TabGroup("GameObject")]
        [GUIColor(1, 0, 0)]
        [Button(ButtonSizes.Medium)]
        public void AddFeedbackInstantiateObject()
        {
            Feedback feedback = new Feedback();
            feedback.Type = Feedback.FeedbackType.InstantiateObject;
            feedback.IsFeedbackInstantiateObject = true;
            feedback.Initialized = true;
            feedbacks.Add(feedback);
        }

        [TabGroup("GameObject")]
        [GUIColor(1, 0, 0)]
        [Button(ButtonSizes.Medium)]
        [ShowInInspector, PropertySpace]
        public void AddFeedbackChangeColorSprite()
        {
            Feedback feedback = new Feedback();
            feedback.Type = Feedback.FeedbackType.ChangeColorSprite;
            feedback.IsFeedbackChangeColorSprite = true;
            feedback.Initialized = true;
            feedbacks.Add(feedback);
        }

        [TabGroup("GameObject")]
        [GUIColor(1, 0, 0)]
        [Button(ButtonSizes.Medium)]
        public void AddFeedbackReturnInitialColorSprite()
        {
            Feedback feedback = new Feedback();
            feedback.Type = Feedback.FeedbackType.ReturnInitialColorSprite;
            feedback.IsFeedbackReturnInitialColorSprite = true;
            feedback.Initialized = true;
            feedbacks.Add(feedback);
        }


        [ShowInInspector, PropertySpace]
        [PropertyOrder(1)]
        [ListDrawerSettings(HideAddButton = true)]
        public List<Feedback> feedbacks = new List<Feedback>();

        private BaseFeedback GetTypeFeedback(Feedback item)
        {
            if (item.Type == Feedback.FeedbackType.Wait)
            {
                if (item.Initialized == false)
                {
                    Debug.Log("OnBeforeSerialize");

                    item.IsFeedbackWait = true;
                    item.Initialized = true;
                }

                return item.feedbackWait;
            }
            else if (item.Type == Feedback.FeedbackType.PlaySound)
            {
                if (item.Initialized == false)
                {
                    Debug.Log("OnBeforeSerialize");

                    item.IsFeedbackPlaySound = true;
                    item.Initialized = true;
                }

                return item.feedbackPlaySound;
            }
            else if (item.Type == Feedback.FeedbackType.InstantiateObject)
            {
                if (item.Initialized == false)
                {
                    Debug.Log("OnBeforeSerialize");

                    item.IsFeedbackInstantiateObject = true;
                    item.Initialized = true;
                }

                return item.feedbackInstantiateObject;
            }
            else if (item.Type == Feedback.FeedbackType.ChangeColorSprite)
            {
                if (item.Initialized == false)
                {
                    Debug.Log("OnBeforeSerialize");

                    item.IsFeedbackChangeColorSprite = true;
                    item.Initialized = true;
                }

                return item.feedbackChangeColorSprite;
            }
            else if (item.Type == Feedback.FeedbackType.ReturnInitialColorSprite)
            {
                if (item.Initialized == false)
                {
                    Debug.Log("OnBeforeSerialize");

                    item.IsFeedbackReturnInitialColorSprite = true;
                    item.Initialized = true;
                }

                return item.feedbackReturnInitialColorSprite;
            }

            return null;
        }

        public IEnumerator Execute(GameObject _owner)
        {
            Debug.Log("Execute");
            foreach (var item in feedbacks)
            {
                Debug.Log("Execute " + item);
                yield return GetTypeFeedback(item).Execute(_owner);
            }
        }



        public void SetTarget(Transform _target)
        {
            Debug.Log("Set Target");
            foreach (var item in feedbacks)
            {
                GetTypeFeedback(item).SetTarget(_target);
            }
        }

        public void SetSpriteRenderer(SpriteRenderer _targetRenderer)
        {
            Debug.Log("Set Sprite Renderer");
            foreach (var item in feedbacks)
            {
                GetTypeFeedback(item).SetSpriteRenderer(_targetRenderer);
            }
        }

        public void SetSpriteRendererBaseColor(Color _baseColor)
        {
            Debug.Log("Set Sprite Renderer Base Color");
            foreach (var item in feedbacks)
            {
                GetTypeFeedback(item).SetSpriteRendererBaseColor(_baseColor);
            }
        }



        protected override void OnBeforeSerialize()
        {
            base.OnBeforeSerialize();
            foreach (var item in feedbacks)
            {
                GetTypeFeedback(item);
            }
        }
    }


    [Serializable]
    public class Feedback
    {

        [HideInInspector]
        public enum FeedbackType
        {
            Default,
            Wait,
            PlaySound,
            InstantiateObject,
            ChangeColorSprite,
            ReturnInitialColorSprite,
        }
        public FeedbackType Type;

        public bool Initialized { get; set; }

        public bool IsFeedbackWait { get; set; }
        [ShowIfGroup("IsFeedbackWait")]
        [BoxGroup("IsFeedbackWait/Feedback Wait")]
        [GUIColor(1, 0.92f, 0.016f)]
        public FeedbackWait feedbackWait;

        public bool IsFeedbackPlaySound { get; set; }
        [ShowIfGroup("IsFeedbackPlaySound")]
        [BoxGroup("IsFeedbackPlaySound/Feedback Play Sound")]
        [GUIColor(0, 1, 0)]
        public FeedbackPlaySound feedbackPlaySound;


        public bool IsFeedbackInstantiateObject { get; set; }
        [ShowIfGroup("IsFeedbackInstantiateObject")]
        [BoxGroup("IsFeedbackInstantiateObject/Feedback Instantiate Object")]
        [GUIColor(1, 0, 0)]
        public FeedbackInstantiateObject feedbackInstantiateObject;

        public bool IsFeedbackChangeColorSprite { get; set; }
        [ShowIfGroup("IsFeedbackChangeColorSprite")]
        [BoxGroup("IsFeedbackChangeColorSprite/Feedback Change Color Sprite")]
        [GUIColor(1, 0, 0)]
        public FeedbackChangeColorSprite feedbackChangeColorSprite;

        public bool IsFeedbackReturnInitialColorSprite { get; set; }
        [ShowIfGroup("IsFeedbackReturnInitialColorSprite")]
        [BoxGroup("IsFeedbackReturnInitialColorSprite/Feedback Return Initial Color Sprite")]
        [GUIColor(1, 0, 0)]
        public FeedbackReturnInitialColorSprite feedbackReturnInitialColorSprite;
    }
}
#endif
