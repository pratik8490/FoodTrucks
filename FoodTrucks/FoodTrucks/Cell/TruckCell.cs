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
            StackLayout stack = CreateMyEscrowLayout();

            View = stack;
        }

        public StackLayout CreateMyEscrowLayout()
        {

            Image imgTruckLogo = new Image { Source = "foodtruck.png" };

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

            Image imgTruckArrow = new Image { Source = Constants.ImagePath.RightArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
            StackLayout slEsrowLayout = new StackLayout { Children = { imgTruckLogo, slLabelTitleInfrontOf, imgTruckArrow }, Padding = new Thickness(30, 5, 0, 5), Orientation = StackOrientation.Horizontal };

            return slEsrowLayout;
        }
    }
}

