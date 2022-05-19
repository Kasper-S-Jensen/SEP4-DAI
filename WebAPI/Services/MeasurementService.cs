﻿using Domain;
using Domain.Exceptions;
using WebAPI.Repositories;

namespace WebAPI.Services;

public class MeasurementService: IMeasurementService
{
    private IMeasurementRepository _measurementRepository;
    private IDeviceService _deviceService;

    public MeasurementService(IMeasurementRepository measurementRepository, IDeviceService deviceService)
    {
        _measurementRepository = measurementRepository;
        _deviceService = deviceService;
    }

    public async Task AddMeasurements(string deviceId, IEnumerable<Measurement> measurements)
    {
        try
        {
            await _deviceService.GetDeviceById(deviceId);
        }
        catch (ArgumentException e)
        {
            try
            {
                Console.WriteLine("miaw");
                await _deviceService.AddNewDevice(new ClimateDevice() {ClimateDeviceId = deviceId});
                await _measurementRepository.AddMeasurements(deviceId, measurements);
                return;
            }
            catch (DeviceAlreadyExistsException e1)
            {
                Console.WriteLine(e1);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        await _measurementRepository.AddMeasurements(deviceId, measurements);
    }
}