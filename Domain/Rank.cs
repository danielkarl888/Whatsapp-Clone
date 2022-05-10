using System.ComponentModel.DataAnnotations;

namespace WAppBIU_Server.Models
{
    public class Rank
    {
        public int Id { get; set; }
        [Required]
        [Range(1,5)]
        public int Number { get; set; }
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

    }
}
