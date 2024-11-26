﻿// <auto-generated />
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241102015156_footer-content-button")]
    partial class footercontentbutton
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Concrete.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Anasayfa", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("İlkyazi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Anasayfa");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Banner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Banner1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Dil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DilAdi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Dil");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Egitmen", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Bakiye")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FotoğrafUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("KisaBilgi")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Soyisim")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Egitmen");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Footer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ButtonLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ButtonStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ButtonText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Footer");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Hakkimizda", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("İlkyazi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Hakkimizda");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Header", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Menu1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu1Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu1Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu2Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu2Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu3Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu3Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu4Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu4Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu5Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu5Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu6Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu6Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu7Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu7Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu8Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu8Status")
                        .HasColumnType("bit");

                    b.Property<string>("Menu9")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Menu9Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu9Status")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Header");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Iletisim", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("İlkyazi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Iletisim");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Kurs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DersSayisi")
                        .HasColumnType("int");

                    b.Property<int>("DilID")
                        .HasColumnType("int");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<int>("EgitmenID")
                        .HasColumnType("int");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("KursAciklama")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("KursAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("DilID");

                    b.HasIndex("EgitmenID");

                    b.ToTable("Kurs");
                });

            modelBuilder.Entity("EntityLayer.Concrete.KursDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int>("KursID")
                        .HasColumnType("int");

                    b.Property<string>("VideoLink")
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("ID");

                    b.HasIndex("KursID");

                    b.ToTable("KursDetail");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Kursiyer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Bakiye")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FotoğrafUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Soyisim")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Kursiyer");
                });

            modelBuilder.Entity("EntityLayer.Concrete.KursiyerKursDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Durum")
                        .HasColumnType("int");

                    b.Property<int>("KursID")
                        .HasColumnType("int");

                    b.Property<int>("KursiyerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KursID");

                    b.HasIndex("KursiyerID");

                    b.ToTable("KursiyerKursDetail");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Kurumsal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("İlkyazi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Kurumsal");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Link", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Gmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GmailStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Linkedin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LinkedinStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Telegram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TelegramStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Tiktok")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TiktokStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Whatsapp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WhatsappStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Youtube")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("YoutubeStatus")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Kurs", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Dil", "Dil")
                        .WithMany()
                        .HasForeignKey("DilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrete.Egitmen", "Egitmen")
                        .WithMany()
                        .HasForeignKey("EgitmenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dil");

                    b.Navigation("Egitmen");
                });

            modelBuilder.Entity("EntityLayer.Concrete.KursDetail", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kurs");
                });

            modelBuilder.Entity("EntityLayer.Concrete.KursiyerKursDetail", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Kurs", "Kurs")
                        .WithMany()
                        .HasForeignKey("KursID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrete.Kursiyer", "Kursiyer")
                        .WithMany()
                        .HasForeignKey("KursiyerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kurs");

                    b.Navigation("Kursiyer");
                });
#pragma warning restore 612, 618
        }
    }
}
