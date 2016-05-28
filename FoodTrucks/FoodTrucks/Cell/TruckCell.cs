﻿using FoodTrucks.Helper;
using FoodTrucks.Provider.Models;
using System;
using Xamarin.Forms;

namespace FoodTrucks
{
    public class TruckCell : ViewCell
    {
        private TruckInfoModel model;
        protected override void OnBindingContextChanged()
        {
            model = (TruckInfoModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateMyEscrowLayout();

            View = stack;
        }

        public StackLayout CreateMyEscrowLayout()
        {

            Image imgTruckLogo = new Image { Source = "foodtruck.png" };

            StackLayout slTruckLogo = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                Children = { imgTruckLogo }
            };

            Label lblTitle = new Label
            {
                Text = model.TruckName,
                TextColor = Color.Gray
            };

            Label lblInFrontOf = new Label
            {
                Text = model.Description,
                TextColor = Color.Gray
            };

            StackLayout slLabelTitleInfrontOf = new StackLayout { Children = { lblTitle, lblInFrontOf }, Orientation = StackOrientation.Vertical };

            Image imgTruckArrow = new Image { Source = Constants.ImagePath.RightArrow };

            StackLayout slDetailArrow = new StackLayout
            {
                Children = { imgTruckArrow },
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slFinalLayout = new StackLayout { Children = { slTruckLogo, slLabelTitleInfrontOf, slDetailArrow }, Padding = new Thickness(5), Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

            return slFinalLayout;
        }
    }
}

