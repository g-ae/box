using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boxapp
{
    public static class Status
    {
        public static class Game
        {
            public const string Version = "1.1.0";
            public const string Language = "SETTING_LANGUAGE";
        }
        public static class MovementType
        {
            public const string Left = "MOVEMENT_LEFT";
            public const string Right = "MOVEMENT_RIGHT";
            public const string Up = "MOVEMENT_UP";
            public const string Down = "MOVEMENT_DOWN";
        }
        public static class CaseType
        {
            public const string Frontier = "CASETYPE_FRONTIER";
            public const string Player = "CASETYPE_PLAYER";
            public const string Box = "CASETYPE_BOX";
            public const string Background = "CASETYPE_BACKGROUND";
            public const string End = "CASETYPE_END";
        }
        public static class Language
        {
            public const string EN = "ENGLISH";
            public const string FR = "FRANCAIS";
            public static List<string> GetLanguageList()
            {
                List<string> lst = new List<string>();
                lst.Add(EN);
                lst.Add(FR);
                return lst;
            }
        }
    }
}
