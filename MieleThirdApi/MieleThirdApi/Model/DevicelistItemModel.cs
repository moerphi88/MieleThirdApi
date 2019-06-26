using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{
    public class DevicelistItemModel
    {
        public DevicelistItemModel()
        {
            IconUri = "icon.png";
            Name = "Dampfgarer";
            Status = "In Betrieb";
            EndeZeit = "19:34";
            ProgressBarValue = .4;
        }

        public DevicelistItemModel(Appliance appliance)
        {
            IconUri = "icon.png";
            Name = "Dampfgarer";
            Status = "In Betrieb";
            EndeZeit = "19:34";
            ProgressBarValue = .4;
        }

        public string IconUri { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string EndeZeit { get; set; }
        public double ProgressBarValue { get; set; }
    }
}
