﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MvcApp.Data
{
    public class UserCard
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
