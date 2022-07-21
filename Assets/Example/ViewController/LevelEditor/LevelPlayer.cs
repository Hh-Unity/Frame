using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace ShootingEditor2D
{
    public class LevelPlayer : MonoBehaviour
    {
        public TextAsset levelFile;
        void Start()
        {
            string xml = levelFile.text;
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlNode levelNode = document.SelectSingleNode("Level");
            foreach (XmlElement levelItemNode in levelNode.ChildNodes)
            {
                string levelItemName = levelItemNode.Attributes["name"].Value;
                int levelItemX = int.Parse(levelItemNode.Attributes["x"].Value);
                int levelItemY = int.Parse(levelItemNode.Attributes["y"].Value);
                GameObject levelItemPrefab = Resources.Load<GameObject>(levelItemName);
                GameObject levelItemGameObj = Instantiate(levelItemPrefab, transform);
                levelItemGameObj.transform.position = new Vector3(levelItemX, levelItemY, 0);
            }
        }
        
    }
}

