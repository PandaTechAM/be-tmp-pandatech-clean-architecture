﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pandatech.CleanArchitecture.Infrastructure.Context;

#nullable disable

namespace Pandatech.CleanArchitecture.Infrastructure.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20240320132329_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireCounter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expire_at");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("key");

                    b.Property<long>("Value")
                        .HasColumnType("bigint")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_counter");

                    b.HasIndex("ExpireAt")
                        .HasDatabaseName("ix_hangfire_counter_expire_at");

                    b.HasIndex("Key", "Value")
                        .HasDatabaseName("ix_hangfire_counter_key_value");

                    b.ToTable("hangfire_counter", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireHash", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("key");

                    b.Property<string>("Field")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("field");

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expire_at");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Key", "Field")
                        .HasName("pk_hangfire_hash");

                    b.HasIndex("ExpireAt")
                        .HasDatabaseName("ix_hangfire_hash_expire_at");

                    b.ToTable("hangfire_hash", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireJob", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expire_at");

                    b.Property<string>("InvocationData")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("invocation_data");

                    b.Property<long?>("StateId")
                        .HasColumnType("bigint")
                        .HasColumnName("state_id");

                    b.Property<string>("StateName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("state_name");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_job");

                    b.HasIndex("ExpireAt")
                        .HasDatabaseName("ix_hangfire_job_expire_at");

                    b.HasIndex("StateId")
                        .HasDatabaseName("ix_hangfire_job_state_id");

                    b.HasIndex("StateName")
                        .HasDatabaseName("ix_hangfire_job_state_name");

                    b.ToTable("hangfire_job", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireJobParameter", b =>
                {
                    b.Property<long>("JobId")
                        .HasColumnType("bigint")
                        .HasColumnName("job_id");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("JobId", "Name")
                        .HasName("pk_hangfire_job_parameter");

                    b.ToTable("hangfire_job_parameter", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireList", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("key");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expire_at");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Key", "Position")
                        .HasName("pk_hangfire_list");

                    b.HasIndex("ExpireAt")
                        .HasDatabaseName("ix_hangfire_list_expire_at");

                    b.ToTable("hangfire_list", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireLock", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("id");

                    b.Property<DateTime>("AcquiredAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("acquired_at");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_lock");

                    b.ToTable("hangfire_lock", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireQueuedJob", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("FetchedAt")
                        .IsConcurrencyToken()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("fetched_at");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint")
                        .HasColumnName("job_id");

                    b.Property<string>("Queue")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("queue");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_queued_job");

                    b.HasIndex("JobId")
                        .HasDatabaseName("ix_hangfire_queued_job_job_id");

                    b.HasIndex("Queue", "FetchedAt")
                        .HasDatabaseName("ix_hangfire_queued_job_queue_fetched_at");

                    b.ToTable("hangfire_queued_job", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireServer", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("id");

                    b.Property<DateTime>("Heartbeat")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("heartbeat");

                    b.Property<string>("Queues")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("queues");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("started_at");

                    b.Property<int>("WorkerCount")
                        .HasColumnType("integer")
                        .HasColumnName("worker_count");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_server");

                    b.HasIndex("Heartbeat")
                        .HasDatabaseName("ix_hangfire_server_heartbeat");

                    b.ToTable("hangfire_server", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireSet", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("key");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("value");

                    b.Property<DateTime?>("ExpireAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expire_at");

                    b.Property<double>("Score")
                        .HasColumnType("double precision")
                        .HasColumnName("score");

                    b.HasKey("Key", "Value")
                        .HasName("pk_hangfire_set");

                    b.HasIndex("ExpireAt")
                        .HasDatabaseName("ix_hangfire_set_expire_at");

                    b.HasIndex("Key", "Score")
                        .HasDatabaseName("ix_hangfire_set_key_score");

                    b.ToTable("hangfire_set", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("data");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint")
                        .HasColumnName("job_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("Reason")
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.HasKey("Id")
                        .HasName("pk_hangfire_state");

                    b.HasIndex("JobId")
                        .HasDatabaseName("ix_hangfire_state_job_id");

                    b.ToTable("hangfire_state", (string)null);
                });

            modelBuilder.Entity("Pandatech.CleanArchitecture.Core.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("ForcePasswordChange")
                        .HasColumnType("boolean")
                        .HasColumnName("force_password_change");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_hash");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("FullName")
                        .HasDatabaseName("ix_users_full_name");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("ix_users_username");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Pandatech.CleanArchitecture.Core.Entities.UserTokenEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AccessTokenExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("access_token_expires_at");

                    b.Property<byte[]>("AccessTokenHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("access_token_hash");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("InitialRefreshTokenCreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initial_refresh_token_created_at");

                    b.Property<long?>("PreviousUserTokenId")
                        .HasColumnType("bigint")
                        .HasColumnName("previous_user_token_id");

                    b.Property<DateTime>("RefreshTokenExpiresAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expires_at");

                    b.Property<byte[]>("RefreshTokenHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("refresh_token_hash");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_tokens");

                    b.HasIndex("AccessTokenHash")
                        .IsUnique()
                        .HasDatabaseName("ix_user_tokens_access_token_hash");

                    b.HasIndex("PreviousUserTokenId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_tokens_previous_user_token_id");

                    b.HasIndex("RefreshTokenHash")
                        .IsUnique()
                        .HasDatabaseName("ix_user_tokens_refresh_token_hash");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_tokens_user_id");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireJob", b =>
                {
                    b.HasOne("Hangfire.EntityFrameworkCore.HangfireState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .HasConstraintName("fk_hangfire_job_hangfire_state_state_id");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireJobParameter", b =>
                {
                    b.HasOne("Hangfire.EntityFrameworkCore.HangfireJob", "Job")
                        .WithMany("Parameters")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_hangfire_job_parameter_hangfire_job_job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireQueuedJob", b =>
                {
                    b.HasOne("Hangfire.EntityFrameworkCore.HangfireJob", "Job")
                        .WithMany("QueuedJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_hangfire_queued_job_hangfire_job_job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireState", b =>
                {
                    b.HasOne("Hangfire.EntityFrameworkCore.HangfireJob", "Job")
                        .WithMany("States")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_hangfire_state_hangfire_job_job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Pandatech.CleanArchitecture.Core.Entities.UserTokenEntity", b =>
                {
                    b.HasOne("Pandatech.CleanArchitecture.Core.Entities.UserTokenEntity", "PreviousUserTokenEntity")
                        .WithOne()
                        .HasForeignKey("Pandatech.CleanArchitecture.Core.Entities.UserTokenEntity", "PreviousUserTokenId")
                        .HasConstraintName("fk_user_tokens_user_tokens_previous_user_token_id");

                    b.HasOne("Pandatech.CleanArchitecture.Core.Entities.UserEntity", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_tokens_users_user_id");

                    b.Navigation("PreviousUserTokenEntity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hangfire.EntityFrameworkCore.HangfireJob", b =>
                {
                    b.Navigation("Parameters");

                    b.Navigation("QueuedJobs");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Pandatech.CleanArchitecture.Core.Entities.UserEntity", b =>
                {
                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
