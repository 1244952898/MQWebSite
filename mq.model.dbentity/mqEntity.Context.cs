﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace mq.model.dbentity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class POSEntities : DbContext
    {
        public POSEntities()
            : base("name=POSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_User> T_User { get; set; }
        public virtual DbSet<T_BG_User> T_BG_User { get; set; }
        public virtual DbSet<T_BG_LoginLog> T_BG_LoginLog { get; set; }
        public virtual DbSet<T_BG_Menu> T_BG_Menu { get; set; }
        public virtual DbSet<T_BG_Role> T_BG_Role { get; set; }
        public virtual DbSet<T_BG_Role_Menu> T_BG_Role_Menu { get; set; }
        public virtual DbSet<T_BG_UpFiles> T_BG_UpFiles { get; set; }
        public virtual DbSet<T_BG_PublishFile> T_BG_PublishFile { get; set; }
        public virtual DbSet<V_BG_PublishFile_User> V_BG_PublishFile_User { get; set; }
        public virtual DbSet<T_BG_ActiveFile> T_BG_ActiveFile { get; set; }
        public virtual DbSet<T_BG_Department> T_BG_Department { get; set; }
        public virtual DbSet<T_BG_DisplayGuideFile> T_BG_DisplayGuideFile { get; set; }
        public virtual DbSet<V_BG_ActiveFile_Department> V_BG_ActiveFile_Department { get; set; }
        public virtual DbSet<V_BG_DisplayGuideFile_User> V_BG_DisplayGuideFile_User { get; set; }
        public virtual DbSet<T_BG_DisplayPartition> T_BG_DisplayPartition { get; set; }
        public virtual DbSet<T_BG_Area> T_BG_Area { get; set; }
        public virtual DbSet<T_BG_Shop> T_BG_Shop { get; set; }
    }
}
