using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageModelViewController.Models;

namespace GarageModelViewController.Data
{
    public class GarageModelViewControllerContext : DbContext
    {
        public GarageModelViewControllerContext (DbContextOptions<GarageModelViewControllerContext> options)
            : base(options)
        {
        }

        public DbSet<GarageModelViewController.Models.ParkedVehicle> ParkedVehicle { get; set; } = default!;
    }
}
