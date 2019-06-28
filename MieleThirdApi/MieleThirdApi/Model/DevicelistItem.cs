using MieleThirdApi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{
    public class DevicelistItem
    {
        public string IconUri { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string EndeZeit { get; set; }
        public double ProgressBarValue { get; set; }

        private IDateTimeHelper _dateTimeHelper;

        public DevicelistItem()
        {
            IconUri = "icon.png";
            Name = "Dampfgarer";
            Status = "In Betrieb";
            EndeZeit = "19:34";
            ProgressBarValue = .4;
        }

        public DevicelistItem(Appliance appliance, IDateTimeHelper dateTimeHelper = null)
        {
            if (dateTimeHelper == null) _dateTimeHelper = new DateTimeHelper();
            else _dateTimeHelper = dateTimeHelper;

            IconUri = GetIconUri(appliance.Ident.type.value_raw);
            Name = GetApplianceName(appliance.Ident.deviceName, appliance.Ident.type.value_localized);
            Status = appliance.State.status.value_localized;
            EndeZeit = GetEndeZeit(appliance.State.remainingTime);
            ProgressBarValue = GetProgressBarValue(appliance.State.remainingTime, appliance.State.elapsedTime);
        }

        private string GetIconUri(int typeRaw)
        {
            return "icon.png";
        }

        private string GetApplianceName(string devicename, string devicetype)
        { 
            if (String.IsNullOrEmpty(devicename) ) return devicetype;
            else return devicename;
        }

        private string GetEndeZeit(List<int> remainingTime)
        {
            //TODO Berechen die ENdeZeit. Achtung, was ist mit 24h Überschreitungen?
            var hours = _dateTimeHelper.Now().Hour + remainingTime[0];
            var minutes = _dateTimeHelper.Now().Minute + remainingTime[1];
            if(minutes >= 60) //cannot be ore than 59+59=118
            {
                hours = hours + 1;
                minutes = minutes - 60;
            }
            if(hours >= 24)
            {
                hours = hours - 24;
            }
            return hours.ToString() + ":" + minutes.ToString();
        }

        private double GetProgressBarValue(List<int> remainingTime, List<int> elapsedTime)
        {
            //Calculate progressBarValue
            Random random = new Random();
            return random.NextDouble();             
        }
    }
}
