﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LastTodoApp.DataContext.Data;

namespace LastTodoApp.Data
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public RoleConfiguration(IServiceProvider services) => this.Services = services;
        public IServiceProvider Services { get; set; }

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = Enum.GetNames<ERole>().Select(x => new IdentityRole(x.ToUpper()) { NormalizedName = roleManager.NormalizeKey(x.ToUpper()) });
            builder.HasData(roles);
        }

    }
}
