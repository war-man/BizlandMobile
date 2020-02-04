﻿using Bizland.Core.Events;
using Prism.Ioc;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core.Helpers
{
    public static class ThemeHelper
    {
        public static string CurrentTheme;

        public static void ChangeTheme(string theme)
        {
            // don't change to the same theme
            if (theme == CurrentTheme) return;

            //// clear all the resources
            //Application.Current.Resources.MergedDictionaries.Clear();
            //Application.Current.Resources.Clear();
            ResourceDictionary applicationResourceDictionary = Application.Current.Resources;
            ResourceDictionary newTheme = null;


            switch (theme.ToLowerInvariant())
            {
                case "light":
                    //newTheme = new LightTheme();
                    break;
                case "dark":
                    //newTheme = new DarkTheme();
                    break;
            }

            foreach (var merged in newTheme.MergedDictionaries)
            {
                applicationResourceDictionary.MergedDictionaries.Add(merged);
            }

            ManuallyCopyThemes(newTheme, applicationResourceDictionary);

            CurrentTheme = theme;

            var _eventAggregator = Prism.PrismApplicationBase.Current.Container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<ThemeChangedEvent>().Publish(true);

        }

        private static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        {
            foreach (var item in fromResource.Keys)
            {
                toResource[item] = fromResource[item];
            }
        }
    }
}
