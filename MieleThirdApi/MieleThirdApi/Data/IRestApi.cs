﻿using MieleThirdApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public interface IRestApi
    {
        Task<List<Appliance>> GetDevicesListAsync();
        Task<Appliance> GetDeviceAsync(string fabNr);
    }
}
