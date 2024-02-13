using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceTemplate.DataAccess.Models.Users;
using Toolbelt.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataAccess.Context;

public class IdentityDatabaseContext: IdentityDbContext<User, IdentityRole<long>, long>
{
    public const string DefaultSchema = "users";
    public const string DefaultMigrationHistoryTableName = "__identity_MigrationsHistory";
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.HasDefaultSchema(DefaultSchema);
        builder.BuildIndexesFromAnnotations();
    }
}