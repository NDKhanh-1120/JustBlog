namespace FA.JustBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Desciption { get; set; }

        [StringLength(1000)]
        public string PostContent { get; set; }

        [StringLength(50)]
        public string UrlSlug { get; set; }

        [StringLength(50)]
        public string Published { get; set; }

        [StringLength(50)]
        public string PostedOn { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
