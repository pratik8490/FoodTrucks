﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Cell
{
    public class HorizontalCell : ViewCell
    {

        public HorizontalCell()
        {
            Grid grid = new Grid
            {
                Padding = new Thickness(5, 0, 5, 0),
                ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (0.3, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (0.7, GridUnitType.Star) },
				},
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
            };

            var leftLabel = new Label
            {
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Start,
            };

            var rightLabel = new Label
            {
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.End,
            };

            leftLabel.SetBinding<HorizontalCell>(Label.TextProperty, vm => vm.Text);
            leftLabel.SetBinding<HorizontalCell>(Label.TextColorProperty, vm => vm.TextColor);

            rightLabel.SetBinding<HorizontalCell>(Label.TextProperty, vm => vm.Detail);
            rightLabel.SetBinding<HorizontalCell>(Label.TextColorProperty, vm => vm.DetailColor);

            grid.Children.Add(leftLabel, 0, 0);
            grid.Children.Add(rightLabel, 1, 0);

            grid.BindingContext = this;
            View = grid;
        }

        public static readonly BindableProperty DetailProperty =
            BindableProperty.Create<HorizontalCell, string>(
                p => p.Detail, string.Empty);

        public static readonly BindableProperty DetailColorProperty =
            BindableProperty.Create<HorizontalCell, Color>(
                p => p.DetailColor, Color.Gray);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<HorizontalCell, string>(
                p => p.Text, string.Empty);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<HorizontalCell, Color>(
                p => p.TextColor, Color.Black);

        public string Detail
        {
            get
            {
                return ((string)base.GetValue(DetailProperty)).TruncateString(25);
            }
            set
            {
                base.SetValue(DetailProperty, value);
            }
        }

        public Color DetailColor
        {
            get
            {
                return (Color)base.GetValue(DetailColorProperty);
            }
            set
            {
                base.SetValue(DetailColorProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        public Color TextColor
        {
            get
            {
                return (Color)base.GetValue(TextColorProperty);
            }
            set
            {
                base.SetValue(TextColorProperty, value);
            }
        }
    }
    public static class StringExtensions
    {
        public static string TruncateString(this String originalString, int length)
        {
            if (string.IsNullOrEmpty(originalString))
            {
                return originalString;
            }
            if (originalString.Length > length)
            {
                return originalString.Substring(0, length) + "...";
            }
            else
            {
                return originalString;
            }
        }
    }
}
