namespace STARK_Project.CryptoApiModel
{
    /// <summary>
    /// General info about cryptocurrency
    /// </summary>
    public class CryptoInfo
    {
        public string Market { get; set; }
        public string FromSymbol { get; set; }
        public string ToSymbol { get; set; }
        public float Price { get; set; }
        public float LastUpdate { get; set; }
        public float Volume24Hour { get; set; }
        public float Change24Hour { get; set; }
        public float ChangePCT24Hour { get; set; }
        public float ChangeDay { get; set; }
        public float ChangePCTDay { get; set; }
        public float ChangeHour { get; set; }
        public float ChangePCTHour { get; set; }
        public float LowDay { get; set; }
        public float HighDay { get; set; }
    }
}