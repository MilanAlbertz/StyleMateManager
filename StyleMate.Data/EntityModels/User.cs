using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleMate.Data.EntityModels
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public ICollection<UserLikedGarment> LikedGarments { get; set; }

        public ICollection<UserLikedTags> LikedTags { get; set; }
    }

    public class UserLikedGarment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GarmentId { get; set; }
    }

    public class UserLikedTags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TagId { get; set; }
    }
}
