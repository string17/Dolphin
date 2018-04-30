﻿// This file was automatically generated by the NPoco T4 Template
// Do not make changes dNewstly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `DolphinCon`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.\SQLEXPRESS; User ID =sa; password=**zapped**;`
//     Schema:                 ``
//     Include Views:          `True`

using System;
using System.Collections.Generic;
using System.Linq;
using NPoco;

namespace DolphinContext.Data.Models
{
	public partial class DolphinDb : Database
	{
		public DolphinDb() : base("DolphinCon")
		{
			CommonConstruct();
		}

		public DolphinDb(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		public virtual void CommonConstruct()
		{
		    Factory = new DefaultFactory();
		}
		
		public interface IFactory
		{
			DolphinDb GetInstance();
		    void BeginTransaction(DolphinDb database);
		    void CompleteTransaction(DolphinDb database);
		}

        public class DefaultFactory : IFactory
        {
            [ThreadStatic]
            static Stack<DolphinDb> _stack = new Stack<DolphinDb>();

            public DolphinDb GetInstance()
            {
               
			    if (_stack == null)
                { return new  DolphinDb(); }
                else { 
					return _stack.Count > 0 ? _stack.Peek() : new DolphinDb();
                }
			   
			    
            }

            public void BeginTransaction(DolphinDb database)
            {

			 if (_stack == null)
				 {
				  _stack = new  Stack<DolphinDb>();
				 }
                _stack.Push(database);
            }

            public void CompleteTransaction(DolphinDb database)
            {
			 if (_stack == null)
				 {
				  _stack = new Stack <DolphinDb>();
				 }
                _stack.Pop();
            }
        }
		
		public static IFactory Factory { get; set; }

        public static DolphinDb GetInstance()
        {
		 if (Factory == null)
                return new DolphinDb();
			return Factory.GetInstance();
        }

		protected override void OnBeginTransaction()
		{
            Factory.BeginTransaction(this);
		}

        protected override void OnCompleteTransaction()
		{
            Factory.CompleteTransaction(this);
		}
		public class Record<T> where T:new()
		{
			public bool IsNew(Database db) { return db.IsNew(this); }
			public object Insert(Database db) { return db.Insert(this); }  
			
			public int Update(Database db, IEnumerable<string> columns) { return db.Update(this, columns); }
			public static int Update(Database db, string sql, params object[] args) { return db.Update<T>(sql, args); }
			public static int Update(Database db, Sql sql) { return db.Update<T>(sql); }
			public int Delete(Database db) { return db.Delete(this); }
			public static int Delete(Database db, string sql, params object[] args) { return db.Delete<T>(sql, args); }
			public static int Delete(Database db, Sql sql) { return db.Delete<T>(sql); }
			public static int Delete(Database db, object primaryKey) { return db.Delete<T>(primaryKey); }
			public static bool Exists(Database db, object primaryKey) { return db.Exists<T>(primaryKey); }
			public static T SingleOrDefault(Database db, string sql, params object[] args) { return db.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Database db, Sql sql) { return db.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(Database db, string sql, params object[] args) { return db.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Database db, Sql sql) { return db.FirstOrDefault<T>(sql); }
			public static T Single(Database db, string sql, params object[] args) { return db.Single<T>(sql, args); }
			public static T Single(Database db, Sql sql) { return db.Single<T>(sql); }
			public static T First(Database db, string sql, params object[] args) { return db.First<T>(sql, args); }
			public static T First(Database db, Sql sql) { return db.First<T>(sql); }
			public static List<T> Fetch(Database db, string sql, params object[] args) { return db.Fetch<T>(sql, args); }
			public static List<T> Fetch(Database db, Sql sql) { return db.Fetch<T>(sql); }
			public static List<T> Fetch(Database db, long page, long itemsPerPage, string sql, params object[] args) { return db.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(Database db, long page, long itemsPerPage, Sql sql) { return db.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(Database db, long skip, long take, string sql, params object[] args) { return db.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(Database db, long skip, long take, Sql sql) { return db.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(Database db, long page, long itemsPerPage, string sql, params object[] args) { return db.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(Database db, long page, long itemsPerPage, Sql sql) { return db.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(Database db, string sql, params object[] args) { return db.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Database db, Sql sql) { return db.Query<T>(sql); }			
			
			protected HashSet<string> Tracker = new HashSet<string>();
			private void OnLoaded() { Tracker.Clear(); }
			protected void Track(string c) { if (!Tracker.Contains(c)) Tracker.Add(c); }

			public int Update(Database db) 
			{ 
				if (Tracker.Count == 0)
					return db.Update(this); 

				var retv = db.Update(this, Tracker.ToArray());
				Tracker.Clear();
				return retv;
			}
			public void Save(Database db) 
			{
                if (this.IsNew(db))
					Insert(db);
				else
					Update(db);
			}		
		}	
	}		
		[TableName("dbo.User_Tracking")]
		[PrimaryKey("Tid")]
		[ExplicitColumns]
		public partial class UserTracking : DolphinDb.Record<UserTracking>  
		{
	        [Column] public int Tid 
			{ 
				get { return _Tid; }
				set { _Tid = value; Track("Tid"); }
			}
			int _Tid;
			[Column("UserName")] public string Username 
			{ 
				get { return _Username; }
				set { _Username = value; Track("UserName"); }
			}
			string _Username;
			[Column("SessionId")] public string Sessionid 
			{ 
				get { return _Sessionid; }
				set { _Sessionid = value; Track("SessionId"); }
			}
			string _Sessionid;
			[Column("SystemIp")] public string Systemip 
			{ 
				get { return _Systemip; }
				set { _Systemip = value; Track("SystemIp"); }
			}
			string _Systemip;
			[Column("SystemName")] public string Systemname 
			{ 
				get { return _Systemname; }
				set { _Systemname = value; Track("SystemName"); }
			}
			string _Systemname;
			[Column("LoginDate")] public DateTime? Logindate 
			{ 
				get { return _Logindate; }
				set { _Logindate = value; Track("LoginDate"); }
			}
			DateTime? _Logindate;
		
			public static IEnumerable<UserTracking> Query(Database db, string[] columns = null, int[] Tid = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.User_Tracking (NOLOCK)");

				if (Tid != null)
					sql.Where("Tid IN (@0)", Tid);

                return db.Query<UserTracking>(sql);
            }
		}
		
		[TableName("dbo.User_Role")]
		[PrimaryKey("RoleId")]
		[ExplicitColumns]
		public partial class UserRole : DolphinDb.Record<UserRole>  
		{
			[Column("RoleId")] public int Roleid 
			{ 
				get { return _Roleid; }
				set { _Roleid = value; Track("RoleId"); }
			}
			int _Roleid;
	        [Column] public string Title 
			{ 
				get { return _Title; }
				set { _Title = value; Track("Title"); }
			}
			string _Title;
			[Column("_Desc")] public string Desc 
			{ 
				get { return _Desc; }
				set { _Desc = value; Track("_Desc"); }
			}
			string _Desc;
			[Column("IsRoleActive")] public bool? Isroleactive 
			{ 
				get { return _Isroleactive; }
				set { _Isroleactive = value; Track("IsRoleActive"); }
			}
			bool? _Isroleactive;
		
			public static IEnumerable<UserRole> Query(Database db, string[] columns = null, int[] Roleid = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.User_Role (NOLOCK)");

				if (Roleid != null)
					sql.Where("RoleId IN (@0)", Roleid);

                return db.Query<UserRole>(sql);
            }
		}
		
		[TableName("dbo.User_AuditTrail")]
		[PrimaryKey("Id")]
		[ExplicitColumns]
		public partial class UserAuditTrail : DolphinDb.Record<UserAuditTrail>  
		{
	        [Column] public int Id 
			{ 
				get { return _Id; }
				set { _Id = value; Track("Id"); }
			}
			int _Id;
			[Column("UserName")] public string Username 
			{ 
				get { return _Username; }
				set { _Username = value; Track("UserName"); }
			}
			string _Username;
			[Column("UserActivity")] public string Useractivity 
			{ 
				get { return _Useractivity; }
				set { _Useractivity = value; Track("UserActivity"); }
			}
			string _Useractivity;
	        [Column] public string Comment 
			{ 
				get { return _Comment; }
				set { _Comment = value; Track("Comment"); }
			}
			string _Comment;
			[Column("DateLog")] public DateTime? Datelog 
			{ 
				get { return _Datelog; }
				set { _Datelog = value; Track("DateLog"); }
			}
			DateTime? _Datelog;
			[Column("SystemName")] public string Systemname 
			{ 
				get { return _Systemname; }
				set { _Systemname = value; Track("SystemName"); }
			}
			string _Systemname;
			[Column("SystemIP")] public string Systemip 
			{ 
				get { return _Systemip; }
				set { _Systemip = value; Track("SystemIP"); }
			}
			string _Systemip;
		
			public static IEnumerable<UserAuditTrail> Query(Database db, string[] columns = null, int[] Id = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.User_AuditTrail (NOLOCK)");

				if (Id != null)
					sql.Where("Id IN (@0)", Id);

                return db.Query<UserAuditTrail>(sql);
            }
		}
		
		[TableName("dbo.Dol_User")]
		[PrimaryKey("UserId")]
		[ExplicitColumns]
		public partial class DolUser : DolphinDb.Record<DolUser>  
		{
			[Column("UserId")] public int Userid 
			{ 
				get { return _Userid; }
				set { _Userid = value; Track("UserId"); }
			}
			int _Userid;
			[Column("FirstName")] public string Firstname 
			{ 
				get { return _Firstname; }
				set { _Firstname = value; Track("FirstName"); }
			}
			string _Firstname;
			[Column("MiddleName")] public string Middlename 
			{ 
				get { return _Middlename; }
				set { _Middlename = value; Track("MiddleName"); }
			}
			string _Middlename;
			[Column("LastName")] public string Lastname 
			{ 
				get { return _Lastname; }
				set { _Lastname = value; Track("LastName"); }
			}
			string _Lastname;
			[Column("UserName")] public string Username 
			{ 
				get { return _Username; }
				set { _Username = value; Track("UserName"); }
			}
			string _Username;
	        [Column] public string Email 
			{ 
				get { return _Email; }
				set { _Email = value; Track("Email"); }
			}
			string _Email;
	        [Column] public string Password 
			{ 
				get { return _Password; }
				set { _Password = value; Track("Password"); }
			}
			string _Password;
			[Column("PhoneNo")] public string Phoneno 
			{ 
				get { return _Phoneno; }
				set { _Phoneno = value; Track("PhoneNo"); }
			}
			string _Phoneno;
			[Column("RoleId")] public int? Roleid 
			{ 
				get { return _Roleid; }
				set { _Roleid = value; Track("RoleId"); }
			}
			int? _Roleid;
			[Column("ClientId")] public int? Clientid 
			{ 
				get { return _Clientid; }
				set { _Clientid = value; Track("ClientId"); }
			}
			int? _Clientid;
			[Column("UserImg")] public string Userimg 
			{ 
				get { return _Userimg; }
				set { _Userimg = value; Track("UserImg"); }
			}
			string _Userimg;
			[Column("IsUserActive")] public bool? Isuseractive 
			{ 
				get { return _Isuseractive; }
				set { _Isuseractive = value; Track("IsUserActive"); }
			}
			bool? _Isuseractive;
			[Column("IsDelete")] public bool? Isdelete 
			{ 
				get { return _Isdelete; }
				set { _Isdelete = value; Track("IsDelete"); }
			}
			bool? _Isdelete;
			[Column("CreatedBy")] public string Createdby 
			{ 
				get { return _Createdby; }
				set { _Createdby = value; Track("CreatedBy"); }
			}
			string _Createdby;
			[Column("CreatedOn")] public DateTime? Createdon 
			{ 
				get { return _Createdon; }
				set { _Createdon = value; Track("CreatedOn"); }
			}
			DateTime? _Createdon;
			[Column("ModifiedBy")] public string Modifiedby 
			{ 
				get { return _Modifiedby; }
				set { _Modifiedby = value; Track("ModifiedBy"); }
			}
			string _Modifiedby;
			[Column("ModifiedOn")] public DateTime? Modifiedon 
			{ 
				get { return _Modifiedon; }
				set { _Modifiedon = value; Track("ModifiedOn"); }
			}
			DateTime? _Modifiedon;
		
			public static IEnumerable<DolUser> Query(Database db, string[] columns = null, int[] Userid = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.Dol_User (NOLOCK)");

				if (Userid != null)
					sql.Where("UserId IN (@0)", Userid);

                return db.Query<DolUser>(sql);
            }
		}
		
		[TableName("dbo.Dol_Client")]
		[PrimaryKey("ClientId")]
		[ExplicitColumns]
		public partial class DolClient : DolphinDb.Record<DolClient>  
		{
			[Column("ClientId")] public int Clientid 
			{ 
				get { return _Clientid; }
				set { _Clientid = value; Track("ClientId"); }
			}
			int _Clientid;
			[Column("ClientName")] public string Clientname 
			{ 
				get { return _Clientname; }
				set { _Clientname = value; Track("ClientName"); }
			}
			string _Clientname;
			[Column("ClientAlias")] public string Clientalias 
			{ 
				get { return _Clientalias; }
				set { _Clientalias = value; Track("ClientAlias"); }
			}
			string _Clientalias;
			[Column("ClientBanner")] public string Clientbanner 
			{ 
				get { return _Clientbanner; }
				set { _Clientbanner = value; Track("ClientBanner"); }
			}
			string _Clientbanner;
			[Column("RespTime")] public string Resptime 
			{ 
				get { return _Resptime; }
				set { _Resptime = value; Track("RespTime"); }
			}
			string _Resptime;
			[Column("RestTime")] public string Resttime 
			{ 
				get { return _Resttime; }
				set { _Resttime = value; Track("RestTime"); }
			}
			string _Resttime;
			[Column("IsClientActive")] public bool? Isclientactive 
			{ 
				get { return _Isclientactive; }
				set { _Isclientactive = value; Track("IsClientActive"); }
			}
			bool? _Isclientactive;
			[Column("CreatedOn")] public DateTime? Createdon 
			{ 
				get { return _Createdon; }
				set { _Createdon = value; Track("CreatedOn"); }
			}
			DateTime? _Createdon;
			[Column("CreatedBy")] public string Createdby 
			{ 
				get { return _Createdby; }
				set { _Createdby = value; Track("CreatedBy"); }
			}
			string _Createdby;
		
			public static IEnumerable<DolClient> Query(Database db, string[] columns = null, int[] Clientid = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.Dol_Client (NOLOCK)");

				if (Clientid != null)
					sql.Where("ClientId IN (@0)", Clientid);

                return db.Query<DolClient>(sql);
            }
		}
		
		[TableName("dbo.Role_Menu")]
		[PrimaryKey("Id")]
		[ExplicitColumns]
		public partial class RoleMenu : DolphinDb.Record<RoleMenu>  
		{
	        [Column] public int Id 
			{ 
				get { return _Id; }
				set { _Id = value; Track("Id"); }
			}
			int _Id;
			[Column("ItemId")] public int? Itemid 
			{ 
				get { return _Itemid; }
				set { _Itemid = value; Track("ItemId"); }
			}
			int? _Itemid;
			[Column("RoleId")] public int? Roleid 
			{ 
				get { return _Roleid; }
				set { _Roleid = value; Track("RoleId"); }
			}
			int? _Roleid;
			[Column("MenuDesc")] public string Menudesc 
			{ 
				get { return _Menudesc; }
				set { _Menudesc = value; Track("MenuDesc"); }
			}
			string _Menudesc;
		
			public static IEnumerable<RoleMenu> Query(Database db, string[] columns = null, int[] Id = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.Role_Menu (NOLOCK)");

				if (Id != null)
					sql.Where("Id IN (@0)", Id);

                return db.Query<RoleMenu>(sql);
            }
		}
		
		[TableName("dbo.Dol_MenuItem")]
		[ExplicitColumns]
		public partial class DolMenuItem : DolphinDb.Record<DolMenuItem>  
		{
			[Column("ItemId")] public int Itemid 
			{ 
				get { return _Itemid; }
				set { _Itemid = value; Track("ItemId"); }
			}
			int _Itemid;
			[Column("ItemName")] public string Itemname 
			{ 
				get { return _Itemname; }
				set { _Itemname = value; Track("ItemName"); }
			}
			string _Itemname;
			[Column("ItemURL")] public string Itemurl 
			{ 
				get { return _Itemurl; }
				set { _Itemurl = value; Track("ItemURL"); }
			}
			string _Itemurl;
			[Column("ItemDesc")] public string Itemdesc 
			{ 
				get { return _Itemdesc; }
				set { _Itemdesc = value; Track("ItemDesc"); }
			}
			string _Itemdesc;
			[Column("MenuId")] public int? Menuid 
			{ 
				get { return _Menuid; }
				set { _Menuid = value; Track("MenuId"); }
			}
			int? _Menuid;
	        [Column] public int? Sequence 
			{ 
				get { return _Sequence; }
				set { _Sequence = value; Track("Sequence"); }
			}
			int? _Sequence;
			[Column("ExternalURL")] public string Externalurl 
			{ 
				get { return _Externalurl; }
				set { _Externalurl = value; Track("ExternalURL"); }
			}
			string _Externalurl;
			[Column("ItemStatus")] public bool? Itemstatus 
			{ 
				get { return _Itemstatus; }
				set { _Itemstatus = value; Track("ItemStatus"); }
			}
			bool? _Itemstatus;
			[Column("ItemIcon")] public string Itemicon 
			{ 
				get { return _Itemicon; }
				set { _Itemicon = value; Track("ItemIcon"); }
			}
			string _Itemicon;
			[Column("ItemAlias")] public string Itemalias 
			{ 
				get { return _Itemalias; }
				set { _Itemalias = value; Track("ItemAlias"); }
			}
			string _Itemalias;
		}
		
		[TableName("dbo.Audit_Trail")]
		[PrimaryKey("AuditId")]
		[ExplicitColumns]
		public partial class AuditTrail : DolphinDb.Record<AuditTrail>  
		{
			[Column("AuditId")] public int Auditid 
			{ 
				get { return _Auditid; }
				set { _Auditid = value; Track("AuditId"); }
			}
			int _Auditid;
			[Column("UserName")] public string Username 
			{ 
				get { return _Username; }
				set { _Username = value; Track("UserName"); }
			}
			string _Username;
			[Column("UserActivity")] public string Useractivity 
			{ 
				get { return _Useractivity; }
				set { _Useractivity = value; Track("UserActivity"); }
			}
			string _Useractivity;
	        [Column] public string Comment 
			{ 
				get { return _Comment; }
				set { _Comment = value; Track("Comment"); }
			}
			string _Comment;
			[Column("DateLog")] public DateTime? Datelog 
			{ 
				get { return _Datelog; }
				set { _Datelog = value; Track("DateLog"); }
			}
			DateTime? _Datelog;
			[Column("SystemName")] public string Systemname 
			{ 
				get { return _Systemname; }
				set { _Systemname = value; Track("SystemName"); }
			}
			string _Systemname;
			[Column("SystemIP")] public string Systemip 
			{ 
				get { return _Systemip; }
				set { _Systemip = value; Track("SystemIP"); }
			}
			string _Systemip;
		
			public static IEnumerable<AuditTrail> Query(Database db, string[] columns = null, int[] Auditid = null)
            {
                var sql = new Sql();

                if (columns != null)
                    sql.Select(columns);

                sql.From("dbo.Audit_Trail (NOLOCK)");

				if (Auditid != null)
					sql.Where("AuditId IN (@0)", Auditid);

                return db.Query<AuditTrail>(sql);
            }
		}
}
