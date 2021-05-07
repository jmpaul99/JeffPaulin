using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class jpContext : DbContext
    {

        public jpContext()
        {
        }

        public IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        public jpContext(DbContextOptions<jpContext> options)
            : base(options)
        {
            var connection = (SqlConnection)Database.GetDbConnection();
            connection.AccessToken = (new Microsoft.Azure.Services.AppAuthentication.AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogPostRec> BlogPostRecs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Category1> Categories1 { get; set; }
        public virtual DbSet<CategoryForSession> CategoryForSessions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerForSession> PlayerForSessions { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategoryRec> PostCategoryRecs { get; set; }
        public virtual DbSet<PostEditRec> PostEditRecs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoleRec> UserRoleRecs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog", "Blogs");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.BlogDescription).HasColumnName("blogDescription");

                entity.Property(e => e.BlogHeader)
                    .HasMaxLength(500)
                    .HasColumnName("blogHeader");

                entity.Property(e => e.BlogName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("blogName");

                entity.Property(e => e.BlogSubHeader)
                    .HasMaxLength(1000)
                    .HasColumnName("blogSubHeader");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.LastPostDate).HasColumnName("lastPostDate");
            });

            modelBuilder.Entity<BlogPostRec>(entity =>
            {
                entity.ToTable("BlogPostRec", "Blogs");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.BlogPostRecs)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BlogPostR__blogI__05D8E0BE");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.BlogPostRecs)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BlogPostR__postI__06CD04F7");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "Blogs");

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Category1>(entity =>
            {
                entity.ToTable("Category", "Training");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<CategoryForSession>(entity =>
            {
                entity.ToTable("CategoryForSession", "Training");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.SessionId).HasColumnName("sessionId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryForSessions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryF__categ__10566F31");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.CategoryForSessions)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CategoryF__sessi__114A936A");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender", "Training");

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("genderName");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Training");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.GroupDescription).HasColumnName("groupDescription");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("groupName");

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Group__organizat__0C85DE4D");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization", "Training");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.OrganizationDescription).HasColumnName("organizationDescription");

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("organizationName");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player", "Training");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.GenderId).HasColumnName("genderId");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Player__genderId__0B91BA14");
            });

            modelBuilder.Entity<PlayerForSession>(entity =>
            {
                entity.ToTable("PlayerForSession", "Training");

                entity.Property(e => e.Attended).HasColumnName("attended");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.PlayerId).HasColumnName("playerId");

                entity.Property(e => e.SessionId).HasColumnName("sessionId");

                entity.Property(e => e.TechnicalImprovementDuringSession).HasColumnName("technicalImprovementDuringSession");

                entity.Property(e => e.TechnicalImprovementFromPreviousSession).HasColumnName("technicalImprovementFromPreviousSession");

                entity.Property(e => e.WorkEthic).HasColumnName("workEthic");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerForSessions)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlayerFor__playe__123EB7A3");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.PlayerForSessions)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlayerFor__sessi__1332DBDC");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Blogs");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsDraft).HasColumnName("isDraft");

                entity.Property(e => e.PostBody)
                    .IsRequired()
                    .HasColumnName("postBody");

                entity.Property(e => e.PostHeader)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("postHeader");

                entity.Property(e => e.PostSubheader)
                    .HasMaxLength(1000)
                    .HasColumnName("postSubheader");

                entity.Property(e => e.PostedBy)
                    .HasMaxLength(100)
                    .HasColumnName("postedBy");
            });

            modelBuilder.Entity<PostCategoryRec>(entity =>
            {
                entity.ToTable("PostCategoryRec", "Blogs");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PostCategoryRecs)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostCateg__categ__07C12930");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostCategoryRecs)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostCateg__postI__08B54D69");
            });

            modelBuilder.Entity<PostEditRec>(entity =>
            {
                entity.ToTable("PostEditRec", "Blogs");

                entity.Property(e => e.EditDate).HasColumnName("editDate");

                entity.Property(e => e.EditNotes).HasColumnName("editNotes");

                entity.Property(e => e.EditedBy)
                    .HasMaxLength(100)
                    .HasColumnName("editedBy");

                entity.Property(e => e.EditedPostBody)
                    .IsRequired()
                    .HasColumnName("editedPostBody");

                entity.Property(e => e.EditedPostHeader)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("editedPostHeader");

                entity.Property(e => e.EditedPostSubheader)
                    .HasMaxLength(1000)
                    .HasColumnName("editedPostSubheader");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsDraft).HasColumnName("isDraft");

                entity.Property(e => e.LastEditId).HasColumnName("lastEditId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.HasOne(d => d.LastEdit)
                    .WithMany(p => p.InverseLastEdit)
                    .HasForeignKey(d => d.LastEditId)
                    .HasConstraintName("FK__PostEditR__lastE__0A9D95DB");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostEditRecs)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostEditR__postI__09A971A2");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "App");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session", "Training");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsDraft).HasColumnName("isDraft");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.SessionDate).HasColumnName("sessionDate");

                entity.Property(e => e.TermId).HasColumnName("termId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__groupId__0E6E26BF");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Session__postId__0D7A0286");

                entity.HasOne(d => d.Term)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.TermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Session__termId__0F624AF8");
            });

            modelBuilder.Entity<Term>(entity =>
            {
                entity.ToTable("Term", "Training");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.TermDescription).HasColumnName("termDescription");

                entity.Property(e => e.TermEndDate).HasColumnName("termEndDate");

                entity.Property(e => e.TermName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("termName");

                entity.Property(e => e.TermStartDate).HasColumnName("termStartDate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "App");

                entity.Property(e => e.CreatedDate).HasColumnName("createdDate");

                entity.Property(e => e.UserGuid).HasColumnName("userGUID");
            });

            modelBuilder.Entity<UserRoleRec>(entity =>
            {
                entity.ToTable("UserRoleRec", "App");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AssignedDate).HasColumnName("assignedDate");

                entity.Property(e => e.OrganizationId).HasColumnName("organizationId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.UserRoleRecs)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__UserRoleR__organ__1DB06A4F");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleRecs)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRoleR__roleI__1CBC4616");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleRecs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRoleR__userI__1BC821DD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
