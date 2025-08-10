using Conserv.Api.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Context
{
	public partial class DBContext : DbContext
	{
		public DBContext()
		{
		}

		public DBContext(DbContextOptions<DBContext> options)
			: base(options)
		{
		}
		public virtual DbSet<Localidad> Localidad { get; set; } = null!;
		public virtual DbSet<Provincia> Provincia { get; set; } = null!;
		public virtual DbSet<Genero> Genero { get; set; } = null!;
		public virtual DbSet<TipoDocumento> TipoDocumento { get; set; } = null!;
		public virtual DbSet<Usuario> Usuario { get; set; } = null!;
		public virtual DbSet<Menu> Menues { get; set; } = null!;
		public virtual DbSet<MenuUsuario> MenuUsuarios { get; set; } = null!;
		public virtual DbSet<Empresa> Empresa { get; set; } = null!;
		public virtual DbSet<Equipo> Equipo { get; set; } = null!;


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.UseCollation("utf8_general_ci")
				.HasCharSet("utf8");
			
			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
