using FoodTrucks.Helper;
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
            StackLayout stack = CreateLayout();

            View = stack;
        }

        public StackLayout CreateLayout()
        {

            Image imgTruckLogo = new Image { Source = "foodtruck.png", WidthRequest = 50 };

            StackLayout slTruckLogo = new StackLayout
            {
                Children = { imgTruckLogo }
            };

            Label lblTitle = new Label
            {
                Text = model.TruckName,
                TextColor = Color.Gray
            };

            Label lblInFrontOf = new Label
            {
                //Text = ? "Infront of" + 
                TextColor = Color.Gray
            };

            if (!string.IsNullOrEmpty(model.InfrontOf))
            {
                lblInFrontOf.Text = "Infront of" + model.InfrontOf;
            }

            StackLayout slLabelTitleInfrontOf = new StackLayout { Children = { lblTitle, lblInFrontOf }, Orientation = StackOrientation.Vertical };

            Image imgTruckArrow = new Image { Source = Constants.ImagePath.RightArrow };

            StackLayout slDetailArrow = new StackLayout
            {
                Children = { imgTruckArrow },
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slFinalLayout = new StackLayout { Children = { slTruckLogo, slLabelTitleInfrontOf, slDetailArrow }, Padding = new Thickness(5), Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

            return slFinalLayout;
        }
    }
}

