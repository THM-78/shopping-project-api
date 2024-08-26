namespace shopping_project_api.Utilities
{
    public class SmsData
    {
        public int messageId { get; set; }
        public long mobile { get; set; }
        public string messageText { get; set; }
        public long sendDateTime { get; set; }
        public int lineNumber { get; set; }
        public double cost { get; set; }
        public int deliveryState { get; set; }
        public long deliveryDateTime { get; set; }
    }
}
