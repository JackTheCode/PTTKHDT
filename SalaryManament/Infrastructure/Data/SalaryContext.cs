using Core.DTO;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.Data
{
    public class SalaryContext : DbContext
    {
        public SalaryContext() : base("SalaryContext") {

        }
        public DbSet<nhanvien> nhanvien { get; set; }
        public DbSet<chucvu> chucvu { get; set; }
        public DbSet<ngach> ngach { get; set; }
        public DbSet<luong> luong { get; set; }
        public DbSet<nhanvien_chucvu> nhanvien_chucvu { get; set; }
        public DbSet<nhanvien_ngach> nhanvien_ngach { get; set; }
        public DbSet<nhanvien2> nhanvien2 { get; set; }
        public DbSet<SubLuong> SubLuong { get; set; }
        //public DbSet<lichsuchucvu> lichsuchucvu { get; set; }
        //public DbSet<lichsungach> lichsungach { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
