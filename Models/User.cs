using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace Olives.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }
        [JsonIgnore] public Image Image { get; set; }
        [JsonIgnore] public List<Interest> Interests { get; set; }
    }
}