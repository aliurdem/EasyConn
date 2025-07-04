﻿using EasyConnect.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyConnect.Extensions
{
    public static class ApplyMigraions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
        }
    }
}
