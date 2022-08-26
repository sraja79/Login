namespace Login.Models
{
    public class EmailOptionModel
    {
        public List<string> ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
