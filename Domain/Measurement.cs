﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [Index(nameof(Timestamp))]
    public class Measurement
    {
        [Key]
        public int MeasurementId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public float Temperature { get; set; }

        [Range(0, 100)]
        [Required]
        public int Humidity { get; set; }

        [Required]
        public int Co2 { get; set; }
    }
}