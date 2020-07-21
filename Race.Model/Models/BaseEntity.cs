using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Race.Model.Models
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public BaseEntity(int id)
        {
            this.Id = id;
        }
    }
}
