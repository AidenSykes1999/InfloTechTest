using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("UserManagement.Data.DataContext");

    protected override void OnModelCreating(ModelBuilder model)
        => model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateofBirth = new DateTime(1985, 5, 10) },
        new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateofBirth = new DateTime(1978, 8, 22) },
        new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateofBirth = new DateTime(1990, 3, 15) },
        new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateofBirth = new DateTime(1982, 6, 5) },
        new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateofBirth = new DateTime(1987, 10, 18) },
        new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateofBirth = new DateTime(1975, 12, 7) },
        new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateofBirth = new DateTime(1995, 2, 28) },
        new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateofBirth = new DateTime(1988, 9, 14) },
        new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateofBirth = new DateTime(1980, 7, 3) },
        new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateofBirth = new DateTime(1983, 11, 20) },
        new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateofBirth = new DateTime(1992, 4, 12) },
        });

    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    // Update already exists in DataContext, but requires additional implementation.
    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        Entry(entity).State = EntityState.Modified;
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
    public TEntity? GetById<TEntity>(long id) where TEntity : class
    {
        return base.Set<TEntity>().Find(id);
    }
}
