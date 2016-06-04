using System;
using lafe.WiFiDetector.Models;

namespace lafe.WiFiDetector.DTOs
{
    /// <summary>
    /// The Data Transfer Object for the measurements
    /// </summary>
    public class MeasurementDto
    {
        /// <summary>
        /// Gets or sets the identifier of this measurement
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of this measurement
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the device identifier of the device that sended this measurement.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the SSID that was found in this measurement
        /// </summary>
        public string SSID { get; set; }

        /// <summary>
        /// Gets or sets the Mac address of the router that was detected
        /// </summary>
        public string BSSID { get; set; }

        /// <summary>
        /// Gets or sets the signal strength of the measurement in dB
        /// </summary>
        public double SignalStrength { get; set; }

        /// <summary>
        /// Gets or sets the encryption that was detected
        /// </summary>
        public EncryptionType Encryption { get; set; }

        /// <summary>
        /// Gets or sets the channel the detected WiFi uses
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the WiFi was hidden
        /// </summary>
        public bool Hidden { get; set; }
    }
}