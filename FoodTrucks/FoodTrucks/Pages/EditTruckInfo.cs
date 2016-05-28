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
    public class EditTruckInfo : BasePage
    {
        private List<FoodTypeModel> _FoodTypeList = new List<FoodTypeModel>();
        private List<BarModel> _BarList = new List<BarModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();
        private IFoodType _FoodTypeProvider = new FoodTypeProvider();
        private IBar _BarProvider = new BarProvider();
        private int _SelectedFoodTypeID = 0, _SelectedBarID = 0;
        private bool _SelectedActivate = false, _SelectedLocation = false;
        private byte[] _UploadImage = null;
        private TruckInfoModel _TruckInfo = new TruckInfoModel();
        private Stream ms;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Edit Truck Info Page"/> class.
        /// </summary>
        public EditTruckInfo()
        {
            Title = "Edit TruckInfo";
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
               {
                   try
                   {
                       //Call for food type and Bar
                       _FoodTypeList = await _FoodTypeProvider.GetFoodType();
                       _BarList = await _BarProvider.GetBar();
                       _TruckInfo = await _TruckInfoProvider.GetTruckDetail(FoodTruckContext.UserID);

                       EditTruckInfoLayout();
                   }
                   catch (Exception ex)
                   {

                   }
               });
        }
        #endregion

        public void EditTruckInfoLayout()
        {
            try
            {
                ExtendedEntry txtTruckName = new ExtendedEntry();
                txtTruckName.Text = _TruckInfo.TruckName;
                txtTruckName.TextColor = Color.Black;

                StackLayout slTruckName = new StackLayout
                {
                    Children = { txtTruckName },
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                ExtendedEntry txtDescrition = new ExtendedEntry();
                txtDescrition.TextColor = Color.Black;
                txtDescrition.Text = _TruckInfo.Description;

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

                if (_TruckInfo.FoodTypeId != 0)
                {
                    lblFoodType.Text = _FoodTypeList.Where(x => x.Id == _TruckInfo.FoodTypeId).FirstOrDefault().Type.ToString();
                    //pcrFoodType.SelectedIndex = 
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
                    if (pcrFoodType.SelectedIndex > -1)
                    {
                        string foodType = lblFoodType.Text = pcrFoodType.Items[pcrFoodType.SelectedIndex];
                        _SelectedFoodTypeID = _FoodTypeList.Where(x => x.Type == foodType).FirstOrDefault().Id;
                    }
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

                if (_TruckInfo.BarId != 0)
                {
                    lblBar.Text = _BarList.Where(x => x.Id == _TruckInfo.BarId).FirstOrDefault().Name;
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
                        if (pcrBar.SelectedIndex > -1)
                        {
                            string bar = lblBar.Text = pcrBar.Items[pcrBar.SelectedIndex];
                            _SelectedBarID = _BarList.Where(x => x.Name == bar).FirstOrDefault().Id;
                        }
                    };

                #endregion

                ExtendedEntry txtWebsite = new ExtendedEntry
                {
                    TextColor = Color.Black,
                    Text = _TruckInfo.Link
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

                Image imgMenu = new Image { WidthRequest = 30 };
                if (!string.IsNullOrEmpty(_TruckInfo.Menu))
                    imgMenu.Source = FileImageSource.FromUri(new Uri("http://foodlifttrucks.com/" + _TruckInfo.Menu));

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

                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    file.Dispose();
                                    _UploadImage = memoryStream.ToArray();
                                }

                                imgMenu.Source = file.Path;
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

                                using (var memoryStream = new MemoryStream())
                                {
                                    file.GetStream().CopyTo(memoryStream);
                                    file.Dispose();
                                    _UploadImage = memoryStream.ToArray();
                                }

                                imgMenu.Source = file.Path;
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
                    Children = { lblMenu },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(0, 10, 0, 10)
                };

                Label lblLocation = new Label
                {
                    Text = "Use my location?",
                    TextColor = Color.Black
                };

                Switch swcLocation = new Switch { IsToggled = true };

                //if (!string.IsNullOrEmpty(_TruckInfo.Longitude.ToString()) && !string.IsNullOrEmpty(_TruckInfo.Longitude.ToString()))
                //{

                //}

                swcLocation.Toggled += (sender, e) =>
                    {
                        _SelectedLocation = e.Value;
                    };

                StackLayout slLocation = new StackLayout
                {
                    Children = { lblLocation, swcLocation },
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

                Switch swcActivate = new Switch { IsToggled = true, HorizontalOptions = LayoutOptions.EndAndExpand };

                swcActivate.Toggled += (sender, e) =>
                {
                    _SelectedActivate = e.Value;
                };

                if (Convert.ToBoolean(_TruckInfo.IsActive))
                {
                    swcActivate.IsToggled = Convert.ToBoolean(_TruckInfo.IsActive);
                }

                StackLayout slActivate = new StackLayout
                {
                    Children = { lblActivate, swcActivate },
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

                btnSubmit.Clicked += async (sender, e) =>
                   {
                       //Device.BeginInvokeOnMainThread(async () =>
                       //{
                       btnSubmit.IsVisible = false;
                       //UserDialogs.Instance.ShowLoading();

                       try
                       {
                           using (UserDialogs.Instance.Loading("Loading..."))
                           {

                               TruckInfoModel truckInfo = new TruckInfoModel();

                               truckInfo.BarId = _SelectedBarID;
                               truckInfo.FoodTypeId = _SelectedFoodTypeID;
                               truckInfo.IsActive = Convert.ToByte(_SelectedActivate);
                               ///truckInfo.Menu = _UploadImage;
                               truckInfo.TruckName = txtTruckName.Text;
                               truckInfo.Link = txtWebsite.Text;
                               truckInfo.Description = txtDescrition.Text;

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
                                   _TruckInfoProvider.UploadBitmapAsync(_UploadImage, newID);
                                   Navigation.PushAsync(App.TruckListPage());
                                   //UserDialogs.Instance.ShowSuccess("Successfully saved truckinfo.");
                               }
                               else
                                   UserDialogs.Instance.ShowError("Some error ocurred.");

                               //UserDialogs.Instance.HideLoading();
                           }
                       }
                       catch (Exception ex)
                       {
                           btnSubmit.IsVisible = true;
                           UserDialogs.Instance.ShowError(ex.Message.ToString(), 1);
                       }
                       //});
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
    }
}
