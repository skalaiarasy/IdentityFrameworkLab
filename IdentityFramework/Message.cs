﻿using System;
using System.Collections.Generic;

#nullable disable

namespace IdentityFramework
{
    public partial class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? PostedTime { get; set; }
        public bool? Updated { get; set; }
        public string Message1 { get; set; }
    }
}
