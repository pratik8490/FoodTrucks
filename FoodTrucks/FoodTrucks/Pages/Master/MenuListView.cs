using System;
using MenuItem = FoodTrucks.Models.MenuModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace FoodTrucks.Pages.Master
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;
            SeparatorVisibility = SeparatorVisibility.None;

            var cell = new DataTemplate(typeof(MenuCell));
            cell.SetBinding(MenuCell.TextProperty, "Title");
            //cell.SetBinding(MenuCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }
    public class MenuCell : ImageCell
    {
        public MenuCell()
            : base()
        {
            this.TextColor = Color.FromHex("AAAAAA");
        }
    }
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = "Home",
                //IconSource = "contacts.png",
                //TargetType = typeof(ContractsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Edit Profile",
                //IconSource = "leads.png",
                //TargetType = typeof(LeadsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Logout",
                IconSource = "opportunities.png",
                //TargetType = typeof(OpportunitiesPage)
            });
        }
    }
}
