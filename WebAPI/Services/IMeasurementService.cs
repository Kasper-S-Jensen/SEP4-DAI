﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace WebAPI.Services
{
    public interface IMeasurementService
    {
        /// <summary>
        /// Registers that new measurements have been generated by a device.  
        /// </summary>
        /// <param name="deviceId">The id of the device that generated the new measurement</param>
        /// <param name="measurements">A list of the measurements that the device has generated</param>
        /// <exception cref="ArgumentException">If deviceId is invalid i.e, deviceId is empty or whitespace only</exception>
        /// <exception cref="ArgumentException">If deviceId is null</exception>
        /// <exception cref="ArgumentException">If list of measurements is null i.e, not instantiated</exception>
        Task AddMeasurements(string deviceId, IEnumerable<Measurement> measurements);
    }
}