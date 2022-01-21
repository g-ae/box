using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace boxapp
{
    class Settings
    {
        /* INSIDE SAVE FILE TXT :
         * 1st line : game version, vérifie que ce soit la même // VERSION:1.0.0
         * sinon, prend tous les paramètres existants et crée un nouveau fichier actualisé
         * 2nd line : game language in fr, en, pt, etc.         // LANGUAGE:ENGLISH
         */
        #region VARIABLES / PARAMÈTRES
        private string lang = Status.Language.EN;
        #endregion
        #region VARIABLES POUR CODE
        private string SAVE_PATH = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Box/save.txt";
        private FileInfo saveFile;
        #endregion
        #region METHODES
        public Settings()
        {
            // recherche fichier paramètres dans le pc : Documents/Games/Box/save.txt
            // plus tard : encrypter avec une extension du genre ".bx"

            CheckSaveFolderExists();
            saveFile = new FileInfo(SAVE_PATH);
            // VERIFICATION FICHIER EXISTANT, SINON CRÉATION
            if (!saveFile.Exists)
            {
                File.Create(SAVE_PATH);
                WriteSettings(saveFile);
            }
            if (!CheckVersion(saveFile))
            {
                // update settings file
                // récupérer tous les paramètres:
                GetAllSettingsFromFile(saveFile);
                WriteSettings(saveFile);
            }
            if (!CheckSettings(saveFile))
            {
                WriteSettings(saveFile);
            }
        }
        private bool CheckSettings(FileInfo file)
        {
            StreamReader sr = new StreamReader(file.FullName);
            bool right = true;

            // LIGNE 0 - VERSION
            string line = sr.ReadLine();
            if (!line.Contains("VERSION:"))
            {
                right = false;
            }
            string version = line.Split(':')[1];
            if (version == "")
            {
                right = false;
            }

            // LIGNE 1 - LANGUE
            line = sr.ReadLine();
            if (!line.Contains("LANGUAGE:"))
            {
                right = false;
            }
            CheckLanguage(file);

            return right;
        }
        private void WriteSettings(FileInfo file)
        {
            StreamWriter sw = new StreamWriter(file.FullName);
            sw.Flush();

            // LIGNE 0
            sw.Write($"GAME VERSION:{Status.Game.Version}\n");

            // LIGNE 1
            sw.Write($"LANGUAGE:{lang}\n");

            sw.Close();
        }
        private bool CheckVersion(FileInfo file)
        {
            StreamReader sr = new StreamReader(file.FullName);
            if (sr.ReadToEnd().Contains(Status.Game.Version))
            {
                sr.Close();
                return true;
            }
            else
            {
                sr.Close();
                return false;
            }
        }
        private void GetAllSettingsFromFile(FileInfo file)
        {
            lang = CheckLanguage(file);
        }
        /// <summary>
        /// Vérifie la langue dans le fichier sélectionné (saveFile). Si la langue n'est pas reconnue, retourne la langue de base (English)
        /// </summary>
        /// <param name="file">Save file à vérifier</param>
        /// <returns>Langue dans fichier sous Status.Language</returns>
        private string CheckLanguage(FileInfo file)
        {
            StreamReader sr = new StreamReader(file.FullName);
            string content = sr.ReadToEnd();
            sr.Close();
            foreach(string langInList in Status.Language.GetLanguageList())
            {
                if (content.Contains(langInList))
                {
                    return langInList;
                }
            }
            return Status.Language.EN;
        }
        private void CheckSaveFolderExists()
        {
            string path = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Box";
            // check that the folder "Documents/Box" exists, if it doesn't, create one
            if (!new DirectoryInfo(path).Exists)
            {
                Directory.CreateDirectory(path);
            }
        }
        public void SetLanguageOnLabels()
        {
            // set all labels' texts to the right language (use lang variable)
            //JsonTextReader jtr = new JsonTextReader(new StringReader());
        }
        public string GetSetting(string setting)
        {
            switch (setting)
            {
                case Status.Game.Language:
                    return lang;
            }
            return null;
        }
        #endregion
        
    }
}
