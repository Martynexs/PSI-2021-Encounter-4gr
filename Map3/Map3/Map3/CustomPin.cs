using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Map3
{
    public class CustomPin
    {
        public Pin pin { get; set; }
        public string Id { get; set; }
        public string PinAddress { get; set; }
        public string PinType { get; set; }
        public string PinName { get; set; }
    }
}
