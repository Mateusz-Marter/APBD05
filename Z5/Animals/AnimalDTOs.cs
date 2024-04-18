using System.ComponentModel.DataAnnotations;

namespace Z5.Animals
{
    public class AnimalDTOs
    {
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "The Area field is required.")]
        public string Area { get; set; }
        
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

    }
}
