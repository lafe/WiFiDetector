namespace lafe.WiFiDetector.Models
{
    public enum EncryptionType
    {
        /// <summary>
        /// WEP Encryption
        /// </summary>
        WEP = 5,
        /// <summary>
        /// WPA Encryption
        /// </summary>
        WPA = 2,
        /// <summary>
        /// WPA2 Encryption
        /// </summary>
        WPA2 = 4,
        /// <summary>
        /// No Encrpytion used
        /// </summary>
        None = 7,
        /// <summary>
        /// Automatic choice of Encryption (e.g. WPA or WPA2)
        /// </summary>
        Auto = 8
    }
}