using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Translate
    {
        public GameObject TextObj => textObj;
        [SerializeField] private GameObject textObj;

        public string TranslateText => translateText;
        [SerializeField] private string translateText;
    }
