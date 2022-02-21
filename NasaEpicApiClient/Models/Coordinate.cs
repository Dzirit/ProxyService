using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NasaEpicApiClient.Models
{
    public class Coordinate
    {
        /// <summary>
        /// Geographical coordinates that the satellite is looking at
        /// </summary>
        [JsonProperty(PropertyName = "centroid_coordinates")]
        public GeoCoordinate CentroidCoordinate { get; set; }

        /// <summary>
        /// Position of the satellite in space
        /// </summary>
        [JsonProperty(PropertyName = "dscovr_j2000_position")]
        public Position SatellitePosition { get; set; }

        /// <summary>
        /// Position of the moon in space
        /// </summary>
        [JsonProperty(PropertyName = "lunar_j2000_position")]
        public Position MooPosition { get; set; }

        /// <summary>
        /// Position of the sun in space
        /// </summary>
        [JsonProperty(PropertyName = "sun_j2000_position")]
        public Position SunPosition { get; set; }

        /// <summary>
        /// Satellite attitude
        /// </summary>
        [JsonProperty(PropertyName = "attitude_quaternions")]
        public AttitudeQuaternion SatelliteAttitude { get; set; }
    }

    public class GeoCoordinate
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class Position
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }

    public class AttitudeQuaternion
    {
        public double Q0 { get; set; }

        public double Q1 { get; set; }

        public double Q2 { get; set; }

        public double Q3 { get; set; }
    }
}
