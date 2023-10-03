﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blogger.Application.Post.Commands
{
    public class CreatePostDto
    {
        public string Title { get; set; } = default!;
        public PostCategory Category { get; set; }
        public string Content { get; set; } = default!;
    }
}
