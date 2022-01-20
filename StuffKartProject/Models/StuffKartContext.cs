using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StuffKartProject.Models
{
  public partial class StuffKartContext : DbContext
  {
    public StuffKartContext()
    {

    }
    public virtual DbSet<CartDetail> CartDetails { get; set; }
    public virtual DbSet<UserDetails> UserDetails { get; set; }
    public virtual DbSet<UploadProducts> Products { get; set; }
    public virtual DbSet<ImageUpload> ImageUploads { get; set; }
    public virtual DbSet<OrderDetails> Orders { get; set; }

    public StuffKartContext(DbContextOptions<StuffKartContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<CartDetail>(entity =>
      {
        entity.HasKey(e => new { e.Id })
                 .HasName("PK__UserDeta__6F9E65E3D0D59B88");

        entity.Property(e => e.productName)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

        entity.Property(e => e.productDescription)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

        entity.Property(e => e.price)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.size)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.UserId)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.Quantity)
                 .IsRequired()
                 .HasMaxLength(50)
                 .IsUnicode(false);

        entity.Property(e => e.Total)
                 .IsRequired()
                 .HasMaxLength(50)
                 .IsUnicode(false);
      });

      modelBuilder.Entity<ImageUpload>(entity =>
      {
        entity.HasKey(e => new { e.ProductId })
            .HasName("PK__UserDeta__6F9E65E3D0D59B88");

        entity.ToTable("ImageUpload");

        entity.Property(e => e.Image)
              .HasMaxLength(50)
              .IsUnicode(false);
      });

      modelBuilder.Entity<UploadProducts>(entity =>
            {
              entity.HasKey(e => new { e.ProductId })
                  .HasName("PK__UserDeta__6F9E65E3D0D59B77");

              entity.ToTable("UploadProducts");

              entity.Property(e => e.Category)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

              entity.Property(e => e.Total)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

              entity.Property(e => e.Quantity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

              entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

              entity.Property(e => e.Price)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

              entity.Property(e => e.Size)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

              entity.Property(e => e.ProductName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

              entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

              entity.Property(e => e.Image1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

              entity.Property(e => e.Image2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

      modelBuilder.Entity<UserDetails>(entity =>
      {
        entity.HasKey(e => new { e.UserId })
                  .HasName("PK__UserDeta__6F9E65E3D0D59B87");

        entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.MobileNumber)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.Password)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.SecurityAnswer)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.SecurityQuestion)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.isAdmin)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

      });

      modelBuilder.Entity<ResetPassword>(entity =>
      {
        entity.HasKey(e => new { e.MobileNumber })
            .HasName("PK__UserDeta__6F9E65E3D0D59B88");

        entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

        entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

        entity.Property(e => e.ConfirmPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

        entity.Property(e => e.SecurityAnswer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

        entity.Property(e => e.SecurityQuestion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);


      });

      modelBuilder.Entity<OrderDetails>(entity =>
      {
        entity.HasKey(e => new { e.ProductName })
            .HasName("PK__UserDeta__6F9E65E3D0D59B88");

        entity.ToTable("OrderDetails");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Total)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.Quantity)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.ProductDescription)
              .IsRequired()
              .HasMaxLength(100)
              .IsUnicode(false);

        entity.Property(e => e.Price)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.Size)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        entity.Property(e => e.MobileNumber)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.Country)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.ZipCode)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);

        entity.Property(e => e.State)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode(false);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
