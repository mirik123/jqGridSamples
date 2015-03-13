namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientStatus")]
    public partial class ClientStatus
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(50)]
        public string descr { get; set; }

        [Required]
        [StringLength(50)]
        public string color { get; set; }
    }
}
