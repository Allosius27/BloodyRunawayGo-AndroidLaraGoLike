using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

    public class MenuXmlReader : MonoBehaviour
    {
        public TextAsset dictionary;

        public string languageName;
        public int currentLanguage;

        string nouvellepartie;
        string continuer;
        string options;
        string quitter;


        public Text textNouvellepartie;
        public Text textContinuer;
        public Text textOptions;
        public Text textQuitter;


        List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
        Dictionary<string, string> obj;

        // Start is called before the first frame update
        void Awake()
        {
            Reader();
        }

        // Update is called once per frame
        void Update()
        {
            languages[currentLanguage].TryGetValue("Name", out languageName);

            languages[currentLanguage].TryGetValue("nouvellepartie", out nouvellepartie);

            languages[currentLanguage].TryGetValue("continuer", out continuer);

            languages[currentLanguage].TryGetValue("options", out options);

            languages[currentLanguage].TryGetValue("quitter", out quitter);


            textNouvellepartie.text = nouvellepartie;
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

                    if (value.Name == "nouvellepartie")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }
                }

                languages.Add(obj);
            }
        }

        public void ValueChangeCheck()
        {
            //currentLanguage = selectDropdown.value;
        }
    }