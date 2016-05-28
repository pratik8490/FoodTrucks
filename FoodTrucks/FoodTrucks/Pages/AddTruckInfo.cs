using Acr.UserDialogs;
using FoodTruck;
using FoodTrucks.Context;
using FoodTrucks.Helper;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using Media.Plugin;
using Media.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Pages
{
    public class AddTruckInfo : BasePage
    {
        private List<FoodTypeModel> _FoodTypeList = new List<FoodTypeModel>();
        private List<BarModel> _BarList = new List<BarModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();
        private IFoodType _FoodTypeProvider = new FoodTypeProvider();
        private IBar _BarProvider = new BarProvider();
        private int _SelectedFoodTypeID = 0, _SelectedBarID = 0;
        private bool _SelectedActivate = false, _SelectedLocation = false;
        private byte[] _UploadImage = null;
        private Stream ms;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AddTruck Info Page"/> class.
        /// </summary>
        public AddTruckInfo()
        {
            Title = "Add TruckInfo";
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
               {
                   try
                   {
                       //Call for food type and Bar
                       _FoodTypeList = await _FoodTypeProvider.GetFoodType();
                       _BarList = await _BarProvider.GetBar();

                       AddTruckInfoLayout();
                   }
                   catch (Exception ex)
                   {

                   }
               });
        }
        #endregion

        #region AddTruckInfoLayout
        /// <summary>
        /// Add Truck Info Layout.
        /// </summary>
        public void AddTruckInfoLayout()
        {
            try
            {
                ExtendedEntry txtTruckName = new ExtendedEntry();
                txtTruckName.Placeholder = "Truck Name";
                txtTruckName.TextColor = Color.Black;

                StackLayout slTruckName = new StackLayout
                {
                    Children = { txtTruckName },
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                ExtendedEntry txtDescrition = new ExtendedEntry();
                txtDescrition.TextColor = Color.Black;
                txtDescrition.Placeholder = "Description";

                StackLayout slDescription = new StackLayout
                {
                    Children = { txtDescrition },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                #region FoodType Dropdown

                Image imgFoodTypeDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblFoodType = new Label { TextColor = Color.Black, Text = "Food Type" };
                Picker pcrFoodType = new Picker { IsVisible = false, Title = "Food Type" };

                foreach (FoodTypeModel item in _FoodTypeList)
                {
                    pcrFoodType.Items.Add(item.Type);
                }

                StackLayout slFoodTypeDisplay = new StackLayout { Children = { lblFoodType, pcrFoodType, imgFoodTypeDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmFoodType = new Frame
                {
                    Content = slFoodTypeDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var foodTypeTap = new TapGestureRecognizer();

                foodTypeTap.NumberOfTapsRequired = 1; // single-tap
                foodTypeTap.Tapped += (s, e) =>
                {
                    pcrFoodType.Focus();
                };
                frmFoodType.GestureRecognizers.Add(foodTypeTap);
                slFoodTypeDisplay.GestureRecognizers.Add(foodTypeTap);

                StackLayout slFoodTypeFrameLayout = new StackLayout
                {
                    Children = { frmFoodType }
                };

                StackLayout slFoodTypeLayout = new StackLayout
                {
                    Children = { slFoodTypeFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                pcrFoodType.SelectedIndexChanged += (sender, e) =>
                {
                    string foodType = lblFoodType.Text = pcrFoodType.Items[pcrFoodType.SelectedIndex];
                    _SelectedFoodTypeID = _FoodTypeList.Where(x => x.Type == foodType).FirstOrDefault().Id;
                };

                #endregion

                #region Bar DropDown

                Image imgBarDropDown = new Image { Source = Constants.ImagePath.DropDownArrow, HorizontalOptions = LayoutOptions.EndAndExpand };
                Label lblBar = new Label { TextColor = Color.Black, Text = "Bar" };
                Picker pcrBar = new Picker { IsVisible = false, Title = "Bar" };

                foreach (BarModel item in _BarList)
                {
                    pcrBar.Items.Add(item.Name);
                }

                StackLayout slBarDisplay = new StackLayout { Children = { lblBar, pcrBar, imgBarDropDown }, Orientation = StackOrientation.Horizontal, Padding = new Thickness(Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 5, 0), Device.OnPlatform(0, 10, 0), Device.OnPlatform(0, 5, 0)) };

                Frame frmBar = new Frame
                {
                    Content = slBarDisplay,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    OutlineColor = Color.Black,
                    Padding = new Thickness(10)
                };

                var barTap = new TapGestureRecognizer();

                barTap.NumberOfTapsRequired = 1; // single-tap
                barTap.Tapped += (s, e) =>
                {
                    pcrBar.Focus();
                };
                frmBar.GestureRecognizers.Add(barTap);
                slBarDisplay.GestureRecognizers.Add(barTap);

                StackLayout slBarFrameLayout = new StackLayout
                {
                    Children = { frmBar }
                };

                StackLayout slBarLayout = new StackLayout
                {
                    Children = { slBarFrameLayout },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                pcrBar.SelectedIndexChanged += (sender, e) =>
                    {
                        string bar = lblBar.Text = pcrBar.Items[pcrBar.SelectedIndex];
                        _SelectedBarID = _BarList.Where(x => x.Name == bar).FirstOrDefault().Id;
                    };

                #endregion

                ExtendedEntry txtWebsite = new ExtendedEntry
                {
                    TextColor = Color.Black,
                    Placeholder = "Website"
                };

                StackLayout slWebsite = new StackLayout
                {
                    Children = { txtWebsite },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                Label lblMenu = new Label
                {
                    TextColor = Color.Black,
                    Text = "Select Menu"
                };

                Image imgMenu = new Image { WidthRequest = 30, IsVisible = false };

                var tapGestureRecognizer = new TapGestureRecognizer();

                tapGestureRecognizer.NumberOfTapsRequired = 1; // single-tap
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    var action = await DisplayActionSheet("Choose any one?", "Cancel", null, "Camera", "Gallery");

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {

                            if (action == "Camera")
                            {
                                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                                {
                                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                                    return;
                                }

                                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                                {
                                    Directory = "Sample",
                                    Name = "user.jpg"// "test.jpg"
                                });

                                if (file == null)
                                    return;

                                imgMenu.IsVisible = true;
                                imgMenu.Source = file.Path;

                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    file.Dispose();
                                    _UploadImage = memoryStream.ToArray();
                                }
                            }
                            else if (action == "Gallery")
                            {
                                if (!CrossMedia.Current.IsPickPhotoSupported)
                                {
                                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                                    return;
                                }
                                var file = await CrossMedia.Current.PickPhotoAsync();

                                if (file == null)
                                    return;

                                imgMenu.IsVisible = true;
                                imgMenu.Source = file.Path;

                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    file.Dispose();
                                    _UploadImage = memoryStream.ToArray();
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                };
                lblMenu.GestureRecognizers.Add(tapGestureRecognizer);

                StackLayout slMenu = new StackLayout
                {
                    Children = { lblMenu, imgMenu },
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                Label lblLocation = new Label
                {
                    Text = "Use my location?",
                    TextColor = Color.Black
                };

                Switch swcLocation = new Switch { IsToggled = true, HorizontalOptions = LayoutOptions.EndAndExpand, WidthRequest = 100 };

                swcLocation.Toggled += (sender, e) =>
                    {
                        _SelectedLocation = e.Value;
                    };

                StackLayout slSwcLocation = new StackLayout
                {
                    Children = { swcLocation },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout slLocation = new StackLayout
                {
                    Children = { lblLocation, slSwcLocation },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };


                Label lblActivate = new Label
                {
                    Text = "Activate",
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Switch swcActivate = new Switch { IsToggled = true, HorizontalOptions = LayoutOptions.EndAndExpand, WidthRequest = 100 };

                swcActivate.Toggled += (sender, e) =>
                {
                    _SelectedActivate = e.Value;
                };

                StackLayout slSwcActivate = new StackLayout
                {
                    Children = { swcActivate },
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout slActivate = new StackLayout
                {
                    Children = { lblActivate, slSwcActivate },
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                Button btnSubmit = new Button
                {
                    Text = "SUBMIT",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("f23e3e")
                };

                btnSubmit.Clicked += (sender, e) =>
                   {
                       Device.BeginInvokeOnMainThread(async () =>
                       {
                           using (UserDialogs.Instance.Loading("Loading..."))
                           {

                               btnSubmit.IsVisible = false;

                               try
                               {
                                   TruckInfoModel truckInfo = new TruckInfoModel();

                                   truckInfo.BarId = _SelectedBarID;
                                   truckInfo.FoodTypeId = _SelectedFoodTypeID;
                                   if (swcActivate.IsToggled)
                                       truckInfo.IsActive = 1;
                                   else
                                       truckInfo.IsActive = 0;
                                   //truckInfo.Menu = "testingsave";
                                   truckInfo.TruckName = txtTruckName.Text;
                                   truckInfo.Link = txtWebsite.Text;
                                   truckInfo.Description = txtDescrition.Text;
                                   truckInfo.MenuImage = _UploadImage;
                                   truckInfo.UserID = FoodTruckContext.UserID;

                                   if (_SelectedLocation)
                                   {
                                       truckInfo.Lattitude = FoodTruckContext.Position.Latitude;
                                       truckInfo.Longitude = FoodTruckContext.Position.Longitude;
                                   }
                                   else
                                   {
                                       //text box value and find location cordinations
                                   }

                                   //service call for save information
                                   int newID = await _TruckInfoProvider.Add(truckInfo);

                                   if (newID != 0)
                                   {
                                       FoodTruckContext.IsLoggedIn = true;
                                       Navigation.PushAsync(App.TruckListPage());
                                       //_TruckInfoProvider.UploadBitmapAsync(_UploadImage, newID);
                                       //UserDialogs.Instance.ShowSuccess("Successfully saved truckinfo.");
                                   }
                                   //else
                                   //    UserDialogs.Instance.ShowError("Some error ocurred.");
                                   btnSubmit.IsVisible = true;
                               }
                               catch (Exception ex)
                               {
                                   btnSubmit.IsVisible = true;
                                   UserDialogs.Instance.ShowError(ex.Message.ToString(), 1);
                               }
                           }
                       });
                   };

                StackLayout slBtnSubmit = new StackLayout
                {
                    Children = { btnSubmit },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };

                StackLayout slFinalLayout = new StackLayout
                {
                    Children = { slTruckName, slDescription, slWebsite, slMenu, slFoodTypeLayout, slBarLayout, slActivate, slLocation, slBtnSubmit },
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(10, Device.OnPlatform(40, 10, 0), 10, 10),
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slFinalLayout,
                };
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
