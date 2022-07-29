namespace MoeBinAPI.Data
{
    public class CreateResponse
    {
        public string Location { get; set; }
        
        public CreateResponse(string s)
        {
            this.Location = s;
        }
    }
    public class RetriveResponse
    {
        public string Content { get; set; }
        public RetriveResponse(string content)
        {
            this.Content = content;
        }
    }
}