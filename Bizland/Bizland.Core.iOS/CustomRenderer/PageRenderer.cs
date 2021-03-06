﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core.iOS.CustomRenderer;
using Bizland.Core.Styles;
using Bizland.Utilities.Enums;
using Foundation;
using UIKit;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(Bizland.Core.iOS.CustomRenderer.PageRenderer))]
namespace Bizland.Core.iOS.CustomRenderer
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetAppTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);
            if (this.TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
            {
                SetAppTheme();
            }


        }

        void SetAppTheme()
        {
            var themeService = Prism.PrismApplicationBase.Current.Container.Resolve<IThemeService>();
            if (themeService != null)
            {
                if (TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark)
                {
                    themeService.UpdateTheme(ThemeMode.Dark);
                }
                else
                {
                    themeService.UpdateTheme(ThemeMode.Light);
                }
            }
        }
    }
}