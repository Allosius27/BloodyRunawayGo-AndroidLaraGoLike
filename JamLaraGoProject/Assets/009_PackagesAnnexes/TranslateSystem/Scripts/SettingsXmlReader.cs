using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    public class SettingsXmlReader : MonoBehaviour
    {
        #region Fields

        private List<string> listTranslates = new List<string>();

        List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
        Dictionary<string, string> obj;

        #endregion

        #region UnityInspector

        [SerializeField] private TextAsset dictionary;

        public string languageName;
        public int currentLanguage;

        [SerializeField] private List<Translate> translates = new List<Translate>();

        #endregion

        // Start is called before the first frame update
        void Awake()
        {

        }

        private void Start()
        {
            for (int i = 0; i < translates.Count; i++)
            {
                if (listTranslates.Contains(translates[i].TranslateText) == false)
                {
                    listTranslates.Add(translates[i].TranslateText);
                }
            }

            Reader();
        }


        // Update is called once per frame
        void Update()
        {
            languages[LangueManager.Instance.currentLanguage].TryGetValue("Name", out languageName);

            for (int i = 0; i < translates.Count; i++)
            {
                LanguageTryGetValue(translates[i].TranslateText, translates[i].TextObj);
            }
        }

        void LanguageTryGetValue(string textToTranslate, GameObject uiTextToTranslate)
        {
            languages[LangueManager.Instance.currentLanguage].TryGetValue(textToTranslate, out textToTranslate);

            if (uiTextToTranslate != null)
            {
                Text _text = uiTextToTranslate.GetComponent<Text>();
                if (_text != null)
                {
                    _text.text = textToTranslate;
                }

                TextMeshProUGUI _textMeshPro = uiTextToTranslate.GetComponent<TextMeshProUGUI>();
                if (_textMeshPro != null)
                {
                    _textMeshPro.text = textToTranslate;
                }
            }
        }

        void TranslateText(XmlNode value, string textToTranslate)
        {
            if (value.Name == textToTranslate)
            {
                obj.Add(value.Name, value.InnerText);
            }
        }

        void Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(dictionary.text);
            XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

            foreach (XmlNode languageValue in languageList)
            {
                XmlNodeList languageContent = languageValue.ChildNodes;
                obj = new Dictionary<string, string>();

                foreach (XmlNode value in languageContent)
                {
                    if (value.Name == "Name")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    for (int i = 0; i < listTranslates.Count; i++)
                    {
                        TranslateText(value, listTranslates[i]);
                    }
                }

                languages.Add(obj);
            }
        }
    }
