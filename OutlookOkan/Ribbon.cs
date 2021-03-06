﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;

namespace OutlookOkan
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI _ribbon;

        public Ribbon(){}

        #region IRibbonExtensibility のメンバー

        public string GetCustomUI(string ribbonId)
        {
            return GetResourceText("OutlookOkan.Ribbon.xml");
        }

        #endregion

        #region リボンのコールバック

        public void Ribbon_Load(Office.IRibbonUI ribbonUi)
        {
            _ribbon = ribbonUi;
        }

        public void ShowSettings(Office.IRibbonControl control)
        {
            var settingWindow = new SettingWindow();
            var temp = settingWindow.ShowDialog();
            settingWindow.Dispose();
        }

        public void ShowVersion(Office.IRibbonControl control)
        {
            var aboutBox = new AboutBox();
            var temp = aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        public void ShowHelp(Office.IRibbonControl control)
        {
            Process.Start("https://github.com/t-miyake/OutlookOkan/wiki");
        }

        #endregion

        #region ヘルパー

        private static string GetResourceText(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceNames = asm.GetManifestResourceNames();
            for (var i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (var resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}