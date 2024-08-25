﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solution.Infrastructure.Persistence.Context;

#nullable disable

namespace Solution.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240813232941_Add Total OrderBook")]
    partial class AddTotalOrderBook
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Solution.Core.Entities.AdmConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("MinSpread")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "ADM_CONFIG_ID_INDEX")
                        .IsUnique();

                    b.ToTable("ADM_CONFIGURATIONS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.CoinNetwork", b =>
                {
                    b.Property<Guid>("CoinId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("NetworkId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CoinId", "NetworkId")
                        .HasName("FOREIGN");

                    b.HasIndex("NetworkId");

                    b.HasIndex(new[] { "Id" }, "COIN_NETWORK_ID_INDEX")
                        .IsUnique();

                    b.ToTable("COINS_NETWORKS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.Crypto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "COIN_ID_INDEX")
                        .IsUnique();

                    b.ToTable("CRYPTOS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.Exchange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ApiSecretKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ApiUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "EXCHANGE_ID_INDEX")
                        .IsUnique();

                    b.ToTable("EXCHANGES", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.Network", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "NETWORK_ID_INDEX")
                        .IsUnique();

                    b.ToTable("NETWORKS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.Opportunity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CoinId")
                        .HasColumnType("char(36)");

                    b.Property<double>("DifferencePercentage")
                        .HasColumnType("double");

                    b.Property<Guid>("ExchangeToBuyId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ExchangeToSellId")
                        .HasColumnType("char(36)");

                    b.Property<double>("HighestValue")
                        .HasColumnType("double");

                    b.Property<double>("LowerValue")
                        .HasColumnType("double");

                    b.Property<Guid?>("OrderBookId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("CoinId");

                    b.HasIndex("ExchangeToBuyId");

                    b.HasIndex("ExchangeToSellId");

                    b.HasIndex("OrderBookId");

                    b.HasIndex(new[] { "Id" }, "OPPORTUNITY_ID_INDEX")
                        .IsUnique();

                    b.ToTable("OPPORTUNITIES", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.OrderBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<Guid>("CoinId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ExchangeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Side")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("double");

                    b.Property<double>("TotalValue")
                        .HasColumnType("double");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("CoinId");

                    b.HasIndex("ExchangeId");

                    b.HasIndex(new[] { "Id" }, "ORDER_BOOK_ID_INDEX")
                        .IsUnique();

                    b.ToTable("ORDER_BOOKS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Trial")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("TrialExpiration")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Id" }, "USER_ID_INDEX")
                        .IsUnique();

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("Solution.Core.Entities.CoinNetwork", b =>
                {
                    b.HasOne("Solution.Core.Entities.Crypto", "Crypto")
                        .WithMany("CoinNetworks")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solution.Core.Entities.Network", "Network")
                        .WithMany("CoinNetworks")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crypto");

                    b.Navigation("Network");
                });

            modelBuilder.Entity("Solution.Core.Entities.Opportunity", b =>
                {
                    b.HasOne("Solution.Core.Entities.Crypto", "Crypto")
                        .WithMany("Opportunities")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solution.Core.Entities.Exchange", "ExchangeToBuy")
                        .WithMany("OpportunitiesToBuy")
                        .HasForeignKey("ExchangeToBuyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solution.Core.Entities.Exchange", "ExchangeToSell")
                        .WithMany("OpportunitiesToSell")
                        .HasForeignKey("ExchangeToSellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solution.Core.Entities.OrderBook", null)
                        .WithMany("Opportunities")
                        .HasForeignKey("OrderBookId");

                    b.Navigation("Crypto");

                    b.Navigation("ExchangeToBuy");

                    b.Navigation("ExchangeToSell");
                });

            modelBuilder.Entity("Solution.Core.Entities.OrderBook", b =>
                {
                    b.HasOne("Solution.Core.Entities.Crypto", "Crypto")
                        .WithMany("OrderBooks")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Solution.Core.Entities.Exchange", "Exchange")
                        .WithMany("OrderBooks")
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crypto");

                    b.Navigation("Exchange");
                });

            modelBuilder.Entity("Solution.Core.Entities.Crypto", b =>
                {
                    b.Navigation("CoinNetworks");

                    b.Navigation("Opportunities");

                    b.Navigation("OrderBooks");
                });

            modelBuilder.Entity("Solution.Core.Entities.Exchange", b =>
                {
                    b.Navigation("OpportunitiesToBuy");

                    b.Navigation("OpportunitiesToSell");

                    b.Navigation("OrderBooks");
                });

            modelBuilder.Entity("Solution.Core.Entities.Network", b =>
                {
                    b.Navigation("CoinNetworks");
                });

            modelBuilder.Entity("Solution.Core.Entities.OrderBook", b =>
                {
                    b.Navigation("Opportunities");
                });
#pragma warning restore 612, 618
        }
    }
}
