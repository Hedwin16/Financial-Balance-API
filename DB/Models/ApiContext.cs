using Microsoft.EntityFrameworkCore;

namespace DB.Models;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionsAccount> TransactionsAccounts { get; set; }

    public virtual DbSet<TransactionsType> TransactionsTypes { get; set; }

    public virtual DbSet<TypeOfTransaction> TypeOfTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currencies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Factor)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("factor");
            entity.Property(e => e.IsoCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("iso_code");
            entity.Property(e => e.Symbol)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("symbol");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.ToTable("privileges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Factor)
                .HasColumnType("money")
                .HasColumnName("factor");
            entity.Property(e => e.IdCurrency).HasColumnName("id_currency");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdCurrency)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_currencies");
        });

        modelBuilder.Entity<TransactionsAccount>(entity =>
        {
            entity.ToTable("transactions_accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdAccount).HasColumnName("id_account");
            entity.Property(e => e.IdTransaction).HasColumnName("id_transaction");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.TransactionsAccounts)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_accounts_accounts");

            entity.HasOne(d => d.IdTransactionNavigation).WithMany(p => p.TransactionsAccounts)
                .HasForeignKey(d => d.IdTransaction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_accounts_transactions");
        });

        modelBuilder.Entity<TransactionsType>(entity =>
        {
            entity.ToTable("transactions_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdTransaction).HasColumnName("id_transaction");
            entity.Property(e => e.IdType).HasColumnName("id_type");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.TransactionsTypes)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transactions_types_type_of_transaction");
        });

        modelBuilder.Entity<TypeOfTransaction>(entity =>
        {
            entity.ToTable("type_of_transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pass");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_accounts");

            entity.ToTable("user_account");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdAccount).HasColumnName("id_account");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_accounts_accounts");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_accounts_users");
        });

        modelBuilder.Entity<UserPrivilege>(entity =>
        {
            entity.ToTable("user_privileges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPrivilege).HasColumnName("id_privilege");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdPrivilegeNavigation).WithMany(p => p.UserPrivileges)
                .HasForeignKey(d => d.IdPrivilege)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_privileges_privileges");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPrivileges)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_privileges_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
