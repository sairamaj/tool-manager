namespace StorageSample.Models
{
    class ConnectionInfo 
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }  
        public string ClientSecret { get; set; }
        public string SubscriptionId { get; set; }
        public string ToolStorageKey {get; set;}
    }
}