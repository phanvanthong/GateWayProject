namespace GetWay.Data.DbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using GetWay.Data.Models;

    public partial class getwayDbContext : DbContext
    {
        public getwayDbContext()
            : base("name=getwayDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public getwayDbContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static getwayDbContext Create()
        {
            return new getwayDbContext();
        }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Arrival> Arrivals { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Garage> Garages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<CarType>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.CarType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Driver>()
                .HasMany(e => e.Notifications)
                .WithOptional(e => e.Driver)
                .HasForeignKey(e => e.Driver_DriverID);

            modelBuilder.Entity<Garage>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.Garage)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Garage>()
                .HasMany(e => e.Drivers)
                .WithOptional(e => e.Garage)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Plan>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Plan)
                .HasForeignKey(e => e.Plan_PlanID);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Notifications)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Student_StudentID);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.Notifications)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.Teacher_TeacherID);
        }
    }
}
