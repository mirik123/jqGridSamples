namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        public int Id { get; set; }
        public byte status { get; set; }
        public byte cltype { get; set; }

        [Required]
        [StringLength(100)]
        public string firstname { get; set; }

        [Required]
        [StringLength(100)]
        public string lastname { get; set; }

        [Required]
        [StringLength(100)]
        public string ssn { get; set; }

        [StringLength(100)]
        public string addr_city { get; set; }

        [StringLength(100)]
        public string addr_street { get; set; }

        [StringLength(100)]
        public string addr_home { get; set; }

        [StringLength(100)]
        public string phone_cell { get; set; }
    }
}
