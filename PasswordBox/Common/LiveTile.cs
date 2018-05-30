using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using PasswordBox.Model;
using PasswordBox.Common;
using PasswordBox.Services;

namespace PasswordBox.Common
{
    class LiveTile
    {
        public static void LoadTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            XmlDocument document = new XmlDocument();
            document.LoadXml(File.ReadAllText("./Common/AdaptiveTile.xml"));
            XmlNodeList textElements = document.GetElementsByTagName("text");
            var textElement = textElements[0] as XmlElement;
            for (int i = 0; i < textElements.Count; i++)
            {
                textElement = textElements[i] as XmlElement;
                if (UserInfo.CheckIfExist("UserName"))
                {
                    textElement.SetAttribute("content", UserInfo.GetInfo("UserName"));
                }
            }

            XmlNodeList imgElements = document.GetElementsByTagName("image");
            var imgElement = imgElements[0] as XmlElement;
            for (int i = 0; i < imgElements.Count; i++)
            {
                imgElement = imgElements[i] as XmlElement;
                if (UserInfo.GetImage("Avator.jpg") != null)
                {
                    imgElement.SetAttribute("src", Path.Combine(ApplicationData.Current.LocalFolder.Path, "Avator.jpg"));
                }
            }

            var tileNotification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
        }
    }
}
