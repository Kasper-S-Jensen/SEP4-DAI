﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebAPI.Repositories;

namespace WebAPI.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMeasurementRepository _measurementRepository;

        public RoomService(IRoomRepository roomRepository, IMeasurementRepository measurementRepository)
        {
            _roomRepository = roomRepository;
            _measurementRepository = measurementRepository;
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null)
            {
                throw new ArgumentException($"No room with id: {id} exists");
            }

            return room;
        }

        public async Task<IEnumerable<Measurement>> GetMeasurementsByRoomIdAsync(int id)
        {
            if (!(await RoomExists(id)))
            {
                throw new ArgumentException($"No room with id: {id} exists");
            }

            return await _measurementRepository.GetByRoomIdAsync(id);
        }

        public async Task<IEnumerable<Measurement>> GetMeasurementsByRoomNameAsync(string roomName)
        {
            if (!(await RoomExists(roomName)))
            {
                throw new ArgumentException($"No room with id: {roomName} exists");
            }

            return await _measurementRepository.GetByRoomNameAsync(roomName);
        }

        public async Task AddMeasurements(string deviceId, string roomName, IEnumerable<Measurement> measurements)
        {
            var existingRoom = await _roomRepository.GetRoomByName(roomName);

            if (!(await RoomExists(roomName)))
            {
                throw new ArgumentException($"No room with id: {roomName} exists");
            }

            if (!ClimateDeviceExists(existingRoom, deviceId))
            {
                throw new ArgumentException($"No device with id: {deviceId} exists");
            }

            await _measurementRepository.AddMeasurements(deviceId, measurements);
        }

        private bool ClimateDeviceExists(Room room, string deviceId)
        {
            return room.ClimateDevices.Any(roomClimateDevice => roomClimateDevice.ClimateDeviceId == deviceId);
        }

        private async Task<bool> RoomExists(int roomId)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(roomId);
            if (existingRoom == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> RoomExists(string roomName)
        {
            var existingRoom = await _roomRepository.GetRoomByName(roomName);
            if (existingRoom == null)
            {
                return false;
            }

            return true;
        }
    }
}