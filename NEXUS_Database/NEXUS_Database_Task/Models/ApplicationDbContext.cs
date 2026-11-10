using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NEXUS_Database_Task.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingService> BookingServices { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Consultation> Consultations { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Expert> Experts { get; set; }

    public virtual DbSet<ExpertAvailability> ExpertAvailabilities { get; set; }

    public virtual DbSet<FavoriteExpert> FavoriteExperts { get; set; }

    public virtual DbSet<FavoriteSpace> FavoriteSpaces { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payout> Payouts { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Space> Spaces { get; set; }

    public virtual DbSet<SpaceService> SpaceServices { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    public virtual DbSet<ThreadParticipant> ThreadParticipants { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.ToTable("amenities");

            entity.HasIndex(e => e.Code, "UQ_amenities_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Label)
                .HasMaxLength(60)
                .HasColumnName("label");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("audit_log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(80)
                .HasColumnName("action");
            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Detail)
                .HasMaxLength(300)
                .HasColumnName("detail");

            entity.HasOne(d => d.Actor).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ActorId)
                .HasConstraintName("FK_audit_log_actor_id");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("bookings");

            entity.HasIndex(e => e.PayBy, "IX_bookings_payby").HasFilter("([status]='approved')");

            entity.HasIndex(e => new { e.SpaceId, e.BookingDate }, "IX_bookings_space_date");

            entity.HasIndex(e => new { e.UserId, e.Status }, "IX_bookings_user_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddonsFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("addons_fee");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.BaseFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base_fee");
            entity.Property(e => e.BookingDate).HasColumnName("booking_date");
            entity.Property(e => e.CancelPenalty)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cancel_penalty");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.DurationType)
                .HasMaxLength(10)
                .HasDefaultValue("daily")
                .HasColumnName("duration_type");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.OwnerCommission)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("owner_commission");
            entity.Property(e => e.PaidAt).HasColumnName("paid_at");
            entity.Property(e => e.PayBy).HasColumnName("pay_by");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .HasDefaultValue("unpaid")
                .HasColumnName("payment_status");
            entity.Property(e => e.People).HasColumnName("people");
            entity.Property(e => e.ServiceFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("service_fee");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_fee");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_bookings_company_id");

            entity.HasOne(d => d.Space).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bookings_space_id");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bookings_user_id");

            entity.HasMany(d => d.Units).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingUnit",
                    r => r.HasOne<Unit>().WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_booking_units_unit_id"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .HasConstraintName("FK_booking_units_booking_id"),
                    j =>
                    {
                        j.HasKey("BookingId", "UnitId");
                        j.ToTable("booking_units");
                        j.IndexerProperty<int>("BookingId").HasColumnName("booking_id");
                        j.IndexerProperty<int>("UnitId").HasColumnName("unit_id");
                    });
        });

        modelBuilder.Entity<BookingService>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.ServiceId });

            entity.ToTable("booking_services");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingServices)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_booking_services_booking_id");

            entity.HasOne(d => d.Service).WithMany(p => p.BookingServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_booking_services_service_id");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("companies");

            entity.HasIndex(e => e.TaxId, "UQ_companies_tax_id")
                .IsUnique()
                .HasFilter("([tax_id] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(60)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Industry)
                .HasMaxLength(60)
                .HasColumnName("industry");
            entity.Property(e => e.MonthlyBudget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monthly_budget");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
            entity.Property(e => e.SizeRange)
                .HasMaxLength(20)
                .HasColumnName("size_range");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TaxId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tax_id");
            entity.Property(e => e.Website)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.ToTable("consultations");

            entity.HasIndex(e => new { e.ExpertId, e.SessionDate }, "IX_consultations_expert_date");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.BaseFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base_fee");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpertCommission)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("expert_commission");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.Mode)
                .HasMaxLength(10)
                .HasDefaultValue("online")
                .HasColumnName("mode");
            entity.Property(e => e.PaidAt).HasColumnName("paid_at");
            entity.Property(e => e.PayBy).HasColumnName("pay_by");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(10)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .HasDefaultValue("unpaid")
                .HasColumnName("payment_status");
            entity.Property(e => e.ServiceFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("service_fee");
            entity.Property(e => e.SessionDate).HasColumnName("session_date");
            entity.Property(e => e.SessionTime).HasColumnName("session_time");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("requested")
                .HasColumnName("status");
            entity.Property(e => e.Topic)
                .HasMaxLength(300)
                .HasColumnName("topic");
            entity.Property(e => e.TotalFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_fee");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_consultations_company_id");

            entity.HasOne(d => d.Expert).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.ExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_consultations_expert_id");

            entity.HasOne(d => d.Space).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_consultations_space_id");

            entity.HasOne(d => d.User).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_consultations_user_id");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("coupons");

            entity.HasIndex(e => e.Code, "UQ_coupons_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppliesTo)
                .HasMaxLength(15)
                .HasDefaultValue("any")
                .HasColumnName("applies_to");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(10)
                .HasColumnName("discount_type");
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("discount_value");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .HasColumnName("title");
            entity.Property(e => e.UsedAt).HasColumnName("used_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_coupons_user_id");
        });

        modelBuilder.Entity<Expert>(entity =>
        {
            entity.ToTable("experts");

            entity.HasIndex(e => e.UserId, "UQ_experts_user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bio)
                .HasMaxLength(1000)
                .HasColumnName("bio");
            entity.Property(e => e.HomeSpaceId).HasColumnName("home_space_id");
            entity.Property(e => e.HourlyRate)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("hourly_rate");
            entity.Property(e => e.RatingAvg)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating_avg");
            entity.Property(e => e.SessionsCount).HasColumnName("sessions_count");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.HomeSpace).WithMany(p => p.Experts)
                .HasForeignKey(d => d.HomeSpaceId)
                .HasConstraintName("FK_experts_home_space_id");

            entity.HasOne(d => d.User).WithOne(p => p.Expert)
                .HasForeignKey<Expert>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_experts_user_id");

            entity.HasMany(d => d.Skills).WithMany(p => p.Experts)
                .UsingEntity<Dictionary<string, object>>(
                    "ExpertSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("FK_expert_skills_skill_id"),
                    l => l.HasOne<Expert>().WithMany()
                        .HasForeignKey("ExpertId")
                        .HasConstraintName("FK_expert_skills_expert_id"),
                    j =>
                    {
                        j.HasKey("ExpertId", "SkillId");
                        j.ToTable("expert_skills");
                        j.IndexerProperty<int>("ExpertId").HasColumnName("expert_id");
                        j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                    });
        });

        modelBuilder.Entity<ExpertAvailability>(entity =>
        {
            entity.ToTable("expert_availability");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.Mode)
                .HasMaxLength(10)
                .HasDefaultValue("space")
                .HasColumnName("mode");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Weekday).HasColumnName("weekday");

            entity.HasOne(d => d.Expert).WithMany(p => p.ExpertAvailabilities)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK_expert_availability_expert_id");

            entity.HasOne(d => d.Space).WithMany(p => p.ExpertAvailabilities)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_expert_availability_space_id");
        });

        modelBuilder.Entity<FavoriteExpert>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ExpertId });

            entity.ToTable("favorite_experts");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ExpertId).HasColumnName("expert_id");
            entity.Property(e => e.SavedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("saved_at");

            entity.HasOne(d => d.Expert).WithMany(p => p.FavoriteExperts)
                .HasForeignKey(d => d.ExpertId)
                .HasConstraintName("FK_favorite_experts_expert_id");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteExperts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_favorite_experts_user_id");
        });

        modelBuilder.Entity<FavoriteSpace>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.SpaceId });

            entity.ToTable("favorite_spaces");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.SavedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("saved_at");

            entity.HasOne(d => d.Space).WithMany(p => p.FavoriteSpaces)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_favorite_spaces_space_id");

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteSpaces)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_favorite_spaces_user_id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");

            entity.HasIndex(e => new { e.ThreadId, e.SentAt }, "IX_messages_thread_sent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasMaxLength(2000)
                .HasColumnName("body");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("sent_at");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_messages_sender_id");

            entity.HasOne(d => d.Thread).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_messages_thread_id");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");

            entity.HasIndex(e => new { e.UserId, e.IsRead, e.CreatedAt }, "IX_notifications_user_unread");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body)
                .HasMaxLength(400)
                .HasColumnName("body");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.ConsultationId).HasColumnName("consultation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Href)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("href");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Title)
                .HasMaxLength(120)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(15)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_notifications_booking_id");

            entity.HasOne(d => d.Consultation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.ConsultationId)
                .HasConstraintName("FK_notifications_consultation_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_notifications_user_id");
        });

        modelBuilder.Entity<Payout>(entity =>
        {
            entity.ToTable("payouts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Method)
                .HasMaxLength(60)
                .HasColumnName("method");
            entity.Property(e => e.Note)
                .HasMaxLength(200)
                .HasColumnName("note");
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("requested_at");
            entity.Property(e => e.ResolvedAt).HasColumnName("resolved_at");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("requested")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_payouts_user_id");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("reviews");

            entity.HasIndex(e => e.BookingId, "UQ_reviews_booking_id")
                .IsUnique()
                .HasFilter("([booking_id] IS NOT NULL)");

            entity.HasIndex(e => e.ConsultationId, "UQ_reviews_consultation_id")
                .IsUnique()
                .HasFilter("([consultation_id] IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasColumnName("comment");
            entity.Property(e => e.ConsultationId).HasColumnName("consultation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Tags)
                .HasMaxLength(200)
                .HasColumnName("tags");

            entity.HasOne(d => d.Author).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reviews_author_id");

            entity.HasOne(d => d.Booking).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.BookingId)
                .HasConstraintName("FK_reviews_booking_id");

            entity.HasOne(d => d.Consultation).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.ConsultationId)
                .HasConstraintName("FK_reviews_consultation_id");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("services");

            entity.HasIndex(e => e.Code, "UQ_services_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Kind)
                .HasMaxLength(15)
                .HasDefaultValue("addon")
                .HasColumnName("kind");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ServiceRequest>(entity =>
        {
            entity.ToTable("service_requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Note)
                .HasMaxLength(300)
                .HasColumnName("note");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_service_requests_booking_id");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_service_requests_service_id");

            entity.HasOne(d => d.Unit).WithMany(p => p.ServiceRequests)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_service_requests_unit_id");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("settings");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("updated_at");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("skills");

            entity.HasIndex(e => e.Name, "UQ_skills_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Space>(entity =>
        {
            entity.ToTable("spaces");

            entity.HasIndex(e => new { e.City, e.Status }, "IX_spaces_city_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Area)
                .HasMaxLength(80)
                .HasColumnName("area");
            entity.Property(e => e.City)
                .HasMaxLength(60)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.DailyPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("daily_price");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.HourlyPrice)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("hourly_price");
            entity.Property(e => e.InstantApproval).HasColumnName("instant_approval");
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.RatingAvg)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating_avg");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("status");

            entity.HasOne(d => d.Owner).WithMany(p => p.Spaces)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_spaces_owner_id");

            entity.HasMany(d => d.Amenities).WithMany(p => p.Spaces)
                .UsingEntity<Dictionary<string, object>>(
                    "SpaceAmenity",
                    r => r.HasOne<Amenity>().WithMany()
                        .HasForeignKey("AmenityId")
                        .HasConstraintName("FK_space_amenities_amenity_id"),
                    l => l.HasOne<Space>().WithMany()
                        .HasForeignKey("SpaceId")
                        .HasConstraintName("FK_space_amenities_space_id"),
                    j =>
                    {
                        j.HasKey("SpaceId", "AmenityId");
                        j.ToTable("space_amenities");
                        j.IndexerProperty<int>("SpaceId").HasColumnName("space_id");
                        j.IndexerProperty<int>("AmenityId").HasColumnName("amenity_id");
                    });
        });

        modelBuilder.Entity<SpaceService>(entity =>
        {
            entity.HasKey(e => new { e.SpaceId, e.ServiceId });

            entity.ToTable("space_services");

            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.Service).WithMany(p => p.SpaceServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_space_services_service_id");

            entity.HasOne(d => d.Space).WithMany(p => p.SpaceServices)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_space_services_space_id");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.ToTable("support_tickets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(30)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Message)
                .HasMaxLength(2000)
                .HasColumnName("message");
            entity.Property(e => e.Reply)
                .HasMaxLength(2000)
                .HasColumnName("reply");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(150)
                .HasColumnName("subject");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_support_tickets_user_id");
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.ToTable("threads");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.Subject)
                .HasMaxLength(150)
                .HasColumnName("subject");

            entity.HasOne(d => d.Space).WithMany(p => p.Threads)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_threads_space_id");
        });

        modelBuilder.Entity<ThreadParticipant>(entity =>
        {
            entity.HasKey(e => new { e.ThreadId, e.UserId });

            entity.ToTable("thread_participants");

            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.LastReadAt).HasColumnName("last_read_at");

            entity.HasOne(d => d.Thread).WithMany(p => p.ThreadParticipants)
                .HasForeignKey(d => d.ThreadId)
                .HasConstraintName("FK_thread_participants_thread_id");

            entity.HasOne(d => d.User).WithMany(p => p.ThreadParticipants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_thread_participants_user_id");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("units");

            entity.HasIndex(e => new { e.SpaceId, e.Code }, "UQ_units_space_id_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity)
                .HasDefaultValue(1)
                .HasColumnName("capacity");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.SpaceId).HasColumnName("space_id");
            entity.Property(e => e.UnitType)
                .HasMaxLength(10)
                .HasDefaultValue("desk")
                .HasColumnName("unit_type");
            entity.Property(e => e.Zone)
                .HasMaxLength(10)
                .HasColumnName("zone");

            entity.HasOne(d => d.Space).WithMany(p => p.Units)
                .HasForeignKey(d => d.SpaceId)
                .HasConstraintName("FK_units_space_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ_users_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .HasColumnName("bio");
            entity.Property(e => e.City)
                .HasMaxLength(60)
                .HasColumnName("city");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("user")
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .HasColumnName("status");

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_users_company_id");
        });

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("wallet_transactions");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "CIX_wallet_transactions").IsClustered();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.ConsultationId).HasColumnName("consultation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Type)
                .HasMaxLength(15)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_wallet_transactions_booking_id");

            entity.HasOne(d => d.Consultation).WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.ConsultationId)
                .HasConstraintName("FK_wallet_transactions_consultation_id");

            entity.HasOne(d => d.User).WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wallet_transactions_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
