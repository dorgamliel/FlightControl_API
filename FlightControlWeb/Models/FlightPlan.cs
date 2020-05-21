﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [Key]
        [Newtonsoft.Json.JsonIgnore]
        [JsonPropertyName("flight_id")]
        public long FlightID { get; set; }
        public int Passengers { get; set; }
        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }
        public List<Segment> Segments { get; set; }
        [JsonPropertyName("initial_location")]
        public InitialLocation InitialLocation { get; set; }
    }
   
    public class Segment
    {
        [Key]
        [Newtonsoft.Json.JsonIgnore]
        public long ID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonPropertyName("timespan_seconds")]
        public int TimespanSeconds { get; set; }
    }
    public class InitialLocation
    {
        [Key]
        [Newtonsoft.Json.JsonIgnore]
        public long ID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }
    }
}
