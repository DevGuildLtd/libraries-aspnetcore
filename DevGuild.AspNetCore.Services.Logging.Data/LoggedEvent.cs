using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevGuild.AspNetCore.Services.Logging.Data
{
    public class LoggedEvent
    {
        public Int64 Id { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(300)]
        public String RequestHost { get; set; }

        [Required]
        [MaxLength(10)]
        public String RequestMethod { get; set; }

        [Required]
        [MaxLength(1024)]
        public String RequestAddress { get; set; }

        [MaxLength(256)]
        public String UserName { get; set; }

        [Required]
        [MaxLength(40)]
        public String UserAddress { get; set; }

        [MaxLength(512)]
        public String UserAgent { get; set; }

        [Required]
        [MaxLength(1024)]
        public String RequestId { get; set; }

        [MaxLength(512)]
        public String Category { get; set; }

        [MaxLength(5)]
        public String LogLevel { get; set; }

        public Int32 EventId { get; set; }

        [MaxLength(256)]
        public String EventName { get; set; }

        public String EventScope { get; set; }

        public String EventMessage { get; set; }

        public String ExceptionClass { get; set; }

        public String ExceptionMessage { get; set; }

        public String ExceptionDetails { get; set; }
    }
}
