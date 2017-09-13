using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class BookForUpdateDto //: BookForManipulationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
