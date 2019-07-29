using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{    public class DeviceListEventArgs : EventArgs
    {
        public List<DevicelistItem> DevicelistItemList { get; set; }
    }
}
