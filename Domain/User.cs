using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI;

namespace Domain
{
    public class User
    {

        [Key]
        public string UserName { get; set; }
        public string? DisplayName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [RegularExpression("([A-Za-z]+[0-9]|[0-9]+[A-Za-z])[A-Za-z0-9]*",
         ErrorMessage = "you have to have at least 1 letter and 1 number!.")]

        public string Password { get; set; }
        [JsonIgnore]
        public List<Contact>? Conversations { get; set; }



    }
}
