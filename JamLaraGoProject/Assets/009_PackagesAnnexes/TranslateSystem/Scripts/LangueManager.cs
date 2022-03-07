using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class LangueManager : AllosiusDev.Singleton<LangueManager>
    {

        public int currentLanguage;
        //public Dropdown selectDropdown;
        //public DictionaryDropdown dictionaryDropdown;

        protected override void Awake()
        {
            base.Awake();

            //dictionaryDropdown = GameObject.FindGameObjectWithTag("LangueDropdown").GetComponent<DictionaryDropdown>();
            //selectDropdown = dictionaryDropdown.selectDropdown;
        }
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }