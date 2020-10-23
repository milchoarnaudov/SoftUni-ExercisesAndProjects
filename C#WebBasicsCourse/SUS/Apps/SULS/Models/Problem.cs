﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
    public class Problem
    {
        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
        }

        public string Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
