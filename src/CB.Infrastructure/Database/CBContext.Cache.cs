﻿using System.Collections.Concurrent;
using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Database {

    public partial class CBContext {
        private static readonly ConcurrentDictionary<string, Timer> timers = new();
        private static readonly ConcurrentDictionary<string, Permission> permissions = new();

        public async Task<List<Permission>> GetPermissions(Func<Permission, bool>? predicate = null, CancellationToken cancellationToken = default) {
            timers.GetOrAdd(nameof(Permission), _ => new Timer(_ => permissions.Clear(), null, TimeSpan.Zero, TimeSpan.FromSeconds(3600)));
            if (permissions.IsEmpty) {
                var items = await this.Permissions.AsNoTracking().ToListAsync(cancellationToken);
                foreach (var item in items) {
                    permissions.AddOrUpdate(item.Id, item, (_, _) => item);
                }
            }

            var result = permissions.Select(o => (Permission)o.Value.Clone()).ToList();
            if (predicate != null) result = result.Where(predicate).ToList();
            return result;
        }
    }
}
