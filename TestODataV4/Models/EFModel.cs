namespace WebApplication2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DevEntities : DbContext
    {
        public DevEntities()
            : base("name=DevEntities")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientStatus> ClientStatus { get; set; }
        public virtual DbSet<ClientType> ClientType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientStatus>()
                .Property(e => e.color)
                .IsUnicode(false);
        }
    }
}
