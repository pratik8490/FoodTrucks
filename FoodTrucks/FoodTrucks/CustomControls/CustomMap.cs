using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodTrucks.CustomControls
{
    public class CustomMap : Map
    {
        public CustomMap(MapSpan mapSpan)
            : base(mapSpan)
        {
        }

        public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin>(x => x.SelectedPin, new CustomPin { Label = "test123" });

        public CustomPin SelectedPin
        {
            get { return (CustomPin)base.GetValue(SelectedPinProperty); }
            set { base.SetValue(SelectedPinProperty, value); }
        }

        public static readonly BindableProperty CustomPinsProperty = BindableProperty.Create<CustomMap, List<CustomPin>>(x => x.CustomPins, new List<CustomPin>() { new CustomPin() { Label = "test123" } });

        public List<CustomPin> CustomPins
        {
            get { return (List<CustomPin>)base.GetValue(CustomPinsProperty); }
            set { base.SetValue(CustomPinsProperty, value); }
        }

    }

}
