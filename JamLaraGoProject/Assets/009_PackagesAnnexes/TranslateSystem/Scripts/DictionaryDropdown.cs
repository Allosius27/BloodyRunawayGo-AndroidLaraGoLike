using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class DictionaryDropdown : MonoBehaviour
    {
        public Dropdown selectDropdown;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ValueChangeCheck()
        {
            Debug.Log("Change Langue");
            LangueManager.Instance.currentLanguage = selectDropdown.value;
        }
    }
