﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.Core.Models;

namespace Vsety.DataAccess.Entities
{
    public class CommentEntity
    {
        public Guid Id { get; set; }


        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }


        public DateTime? Time { get; set; }
        public string Comment { get; set; } = string.Empty;


        public Guid PostId { get; set; }
        public PostEntity Post { get; set; } = null!;
    }
}
