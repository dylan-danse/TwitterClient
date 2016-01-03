using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TwitterClient.Models;
using TwitterClient.Xml;

namespace TwitterClient.Auth
{
    public class RememberToken
    {

        private const string path = @"token/";
        private const string name = @"/token.tk";

        private XmlManager<Models.Token> xmlManager;

        public XmlManager<Models.Token> XmlManager
        {
            get { return xmlManager; }
            set { xmlManager = value; }
        }
        public string Path
        {
            get { return path; }
        }
        public static string Name
        {
            get
            {
                return name;
            }
        }

        public RememberToken()
        {
            XmlManager = new XmlManager<Token>();
        }

        public bool IsTokenExist
        {
            get
            {
                return File.Exists(Path+Name);
            }
        }

        public Token GetToken()
        {
            Token token = null;
            if (IsTokenExist)
            {
                using (var fm = new FileManager.FileOpen(Path+Name))
                {
                    token = XmlManager.Deserialize(fm.Stream);
                }
            }
            return token;
        }

        public void SaveRememberToken(Token token)
        {
            using (var fm = new FileManager.FileWriter(Path,Name))
            {
                XmlManager.Serialize(fm.Stream, token);
            }
        }
    }
}
