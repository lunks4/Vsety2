﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vsety.DataAccess.Entities
{
    public class PersonEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } 
        [DataType(DataType.Text)]
        public string Surname { get; set; } 
        public string Gender { get; set;} 
        public string City { get; set; } 
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set;}
        [DataType(DataType.Text)]
        public string Nickname { get; set; }

        public string? Description { get; set; }


        public Guid UserId { get; set; }
        public UserEntity User { get; set; }




        public ImgEntity? Img { get; set; }
    }
}
