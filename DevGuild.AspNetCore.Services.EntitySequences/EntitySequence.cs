using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevGuild.AspNetCore.Services.EntitySequences
{
    public class EntitySequence
    {
        [Required]
        [MaxLength(50)]
        public String Id { get; set; }

        public Int64 NextSequenceId { get; set; }
    }
}
