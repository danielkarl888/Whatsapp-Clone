using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI
{
    public class Contact
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Server { get; set; }

        public string? Last { get; set; }

        public string? LastDate { get; set; }

       // [JsonIgnore]
        public List<Message>? Messages { get; set; }

    }
}
