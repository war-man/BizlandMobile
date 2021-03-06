﻿using Bizland.Core.Helpers;
using Bizland.Utilities.Constant;
using Bizland.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Bizland.Core.Resource
{
    public partial class MobileResource
    {
        private static MobileResource instance = null;

        protected static MobileResource Instance
        {
            get
            {
                // Lazy load => design Pattern
                if (instance == null)
                {
                    instance = new MobileResource();
                }
                return instance;
            }
        }

        public static string Get(string key)
        {
            try
            {
                return Instance.GetType().GetProperty(key).GetValue(Instance)?.ToString() ?? key;
            }
            catch
            {
                return key;
            }
        }

        public static T Get<T>(MobileResourceNames key, T defaultValue, T defaultValueEng) where T : IConvertible
        {
            var cultureInfo = Settings.CurrentLanguage;
            var val = cultureInfo == CultureCountry.Vietnamese ? defaultValue : defaultValueEng;
            try
            {
                var configDict = DicMobileResource;

                // Neu dictionary chua key nay thi moi lay ra, neu ko tra ve gia tri mac dinh
                if (configDict != null && configDict.Count > 0 && configDict.ContainsKey(key.ToString()))
                {
                    var setting = configDict[key.ToString()].ToString();

                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        // this will throw an exception when conversion is not possible
                        val = (T)converter.ConvertFromString(setting);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);

            }

            return val;
        }

        public static IDictionary<string, string> _DicMobileResource = null;

        public static IDictionary<string, string> DicMobileResource
        {
            get
            {
                if (_DicMobileResource == null)
                {
                    try
                    {
                        //var service = Prism.PrismApplicationBase.Current.Container.Resolve<IResourceService>();

                        //_DicMobileResource = service.Find(x => x.CodeName == Settings.CurrentLanguage).ToDictionary(k => k.Name, v => v.Value);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.WriteError(MethodBase.GetCurrentMethod().Name, ex);
                    }
                }
                return _DicMobileResource;
            }
        }
    }
}