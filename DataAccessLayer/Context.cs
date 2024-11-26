using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DilKursumDB;integrated security=True;TrustServerCertificate=True; Pooling=False");
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Dil> Dil { get; set; }
        public DbSet<Egitmen> Egitmen { get; set; }
        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<KursDetail> KursDetail { get; set; }
        public DbSet<KursFile> KursFile { get; set; }
        public DbSet<Kursiyer> Kursiyer { get; set; }
        public DbSet<KursiyerKursDetail> KursiyerKursDetail { get; set; }
        public DbSet<Anasayfa> Anasayfa { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Kurumsal> Kurumsal { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Footer> Footer { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<Header> Header { get; set; }

    }
}
