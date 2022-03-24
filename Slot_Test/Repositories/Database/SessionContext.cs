using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Slot_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slot_Test.Repositories.Database
{
    public class SessionContext : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session");

            builder.HasKey(ti => ti.Id);

            builder.Property(ti => ti.Win);

            builder.Property(ti => ti.Value);

            builder.Property(ti => ti.UserId);

            builder.Property(ti => ti.Multiplier);

            builder.Property(ti => ti.Combination);

        }
    }
}
