using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleMate.Data.EntityModels
{
    public class StyleMateGarment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public float Price { get; set; }
        public string SiteUrl { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageUrl> ImageUrls { get; set; }

        public StyleMateGarment()
        { 
            Tags = new List<Tag>();
            ImageUrls = new List<ImageUrl>();
        }
    }

    public class ImageUrl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GarmentTag { get; set; }
    }

}