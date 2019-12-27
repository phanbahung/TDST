namespace TDST_CRUD.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TDSTDbContext : DbContext
    {
        public TDSTDbContext()
            : base("name=TDSTDbContext")
        {
        }

        public virtual DbSet<BoChiTieuChiTiet> BoChiTieuChiTiets { get; set; }
        public virtual DbSet<BoChiTieu> BoChiTieus { get; set; }
        public virtual DbSet<Chuong> Chuongs { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<DuToanChiTiet> DuToanChiTiets { get; set; }
        public virtual DbSet<DuToan> DuToans { get; set; }
        public virtual DbSet<GiaoDich> GiaoDichs { get; set; }
        public virtual DbSet<Group_Roles> Group_Roles { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<NguoiNopThue> NguoiNopThues { get; set; }
        public virtual DbSet<NhomCH_CH> NhomCH_CH { get; set; }
        public virtual DbSet<NhomChuong> NhomChuongs { get; set; }
        public virtual DbSet<NhomTieuMuc> NhomTieuMucs { get; set; }
        public virtual DbSet<NhomTM_TM> NhomTM_TM { get; set; }
        public virtual DbSet<QuanLyNNT> QuanLyNNTs { get; set; }
        public virtual DbSet<QuanLyTM> QuanLyTMs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TieuMuc> TieuMucs { get; set; }
        public virtual DbSet<UDController> UDControllers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Groups> User_Groups { get; set; }
        public virtual DbSet<LOG_UNGDUNG> LOG_UNGDUNG { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoChiTieuChiTiet>()
                .Property(e => e.MaDonVi_Tao)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BoChiTieuChiTiet>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BoChiTieu>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Chuong>()
                .Property(e => e.MaChuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.MaDonVi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.MaDonViCha)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.MaCqThu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonVi>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.MaDonVi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.SoDuToanGiao)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.SoThucHien)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.SoThucHien_KBac)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToanChiTiet>()
                .Property(e => e.MaDonVi_Tao)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToan>()
                .Property(e => e.CoQuanBanHanh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToan>()
                .Property(e => e.MaDonVi_Tao)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DuToan>()
                .Property(e => e.UserUpdate)
                .IsFixedLength();

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.Tin)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.Ma_cqthu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.Ma_chuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.So_tien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.MaDonViQLKhoanThu)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<GiaoDich>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Group_Roles>()
                .Property(e => e.RoleName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiNopThue>()
                .Property(e => e.MST)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiNopThue>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomCH_CH>()
                .Property(e => e.MaChuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomCH_CH>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomChuong>()
                .Property(e => e.Ds_MaChuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomChuong>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomChuong>()
                .Property(e => e.MaDonVi_Tao)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomTieuMuc>()
                .Property(e => e.Ds_MaTieuMuc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomTieuMuc>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomTM_TM>()
                .Property(e => e.MaTieuMuc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhomTM_TM>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QuanLyNNT>()
                .Property(e => e.MST)
                .IsFixedLength();

            modelBuilder.Entity<QuanLyNNT>()
                .Property(e => e.MaDonVi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QuanLyNNT>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QuanLyTM>()
                .Property(e => e.MaDonVi)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QuanLyTM>()
                .Property(e => e.TieuMuc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QuanLyTM>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ActionName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ControllerName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TieuMuc>()
                .Property(e => e.MaTieuMuc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TieuMuc>()
                .Property(e => e.Muc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TieuMuc>()
                .Property(e => e.Loai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UDController>()
                .Property(e => e.ControllerName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IdUser)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User_Groups>()
                .Property(e => e.UserName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOG_UNGDUNG>()
                .Property(e => e.FieldName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOG_UNGDUNG>()
                .Property(e => e.TableName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOG_UNGDUNG>()
                .Property(e => e.UserUpdate)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOG_UNGDUNG>()
                .Property(e => e.IDValue)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOG_UNGDUNG>()
                .Property(e => e.MaDonVi)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
