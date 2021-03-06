﻿using System;
using System.Globalization;
using System.Threading;
using Vueling.Common.Logic.Properties;

namespace Vueling.Common.Logic.Helpers
{
    public static class Language
    {
        public static string AppLanguage { get; set; }

        public static void InitializeLanguage()
        {            
            string idioma = Environment.GetEnvironmentVariable(ConfigStrings.Language, EnvironmentVariableTarget.User);
            
            if (String.IsNullOrEmpty(idioma))
            {
                AppLanguage = ConfigStrings.Spanish;
                Environment.SetEnvironmentVariable(ConfigStrings.Language, ConfigStrings.Spanish, EnvironmentVariableTarget.User);
                ChangeLanguage(ConfigStrings.Spanish);
            }
            else
            {
                AppLanguage = idioma;
                ChangeLanguage(idioma);                
            }            
        }
        public static void ChangeLanguage(string idioma)
        {
            Environment.SetEnvironmentVariable(ConfigStrings.Language, idioma, EnvironmentVariableTarget.User);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(idioma);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(idioma);
        }
    }
}
