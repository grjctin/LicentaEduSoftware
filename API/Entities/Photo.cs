using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //conventie pentru a defini corect o relatie, sa nu fie foreign-key-ul nullable, stergere cascadata, etc
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}