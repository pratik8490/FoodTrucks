using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

﻿using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FoodTruck;
using FoodTruck.Droid;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace FoodTruck.Droid
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.LightGray);
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                Control.SetCursorVisible(true);

                Control.SetHintTextColor(global::Android.Graphics.Color.White);
                //Control.SetShadowLayer(2, 2, 2, global::Android.Graphics.Color.DarkGray);
                //Control.SetHighlightColor(global::Android.Graphics.Color.DarkGray);
            }
        }
    }
}