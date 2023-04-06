using System;
using Microsoft.EntityFrameworkCore;

namespace ACProject.Models
{
   public class ACDBContext : DbContext
{
    public DbSet<AC> ACs { get; set; }
}
}