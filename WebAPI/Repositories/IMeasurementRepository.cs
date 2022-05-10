﻿using Domain;

namespace WebAPI.Repositories;

public interface IMeasurementRepository
{
    Task<IEnumerable<Measurement>> GetAllAsync();
    Task<IEnumerable<Measurement>> GetByRoomIdAsync(int roomId);
    Task<IEnumerable<Measurement>> GetByRoomNameAsync(string roomName);
}