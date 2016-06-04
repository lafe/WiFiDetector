using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lafe.WiFiDetector.Models
{
    public class Measurement
    {
        /// <summary>
        /// Gets or sets the identifier of this measurement
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of this measurement
        /// </summary>
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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