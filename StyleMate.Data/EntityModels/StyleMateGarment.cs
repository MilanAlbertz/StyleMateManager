using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StyleMate.Data.EntityModels
{
    public class StyleMateGarment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public float Price { get; set; }
        public string SiteUrl { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ImageUrl> ImageUrls { get; set; }

        public StyleMateGarment()
        { 
            Name = string.Empty;
            SiteUrl = string.Empty;
            Tags = new List<Tag>();
            ImageUrls = new List<ImageUrl>();
        }
    }

    public class ImageUrl
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string GarmentTag { get; set; }
    }

}