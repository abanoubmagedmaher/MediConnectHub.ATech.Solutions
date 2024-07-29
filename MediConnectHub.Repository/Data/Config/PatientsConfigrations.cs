using MediConnectHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnectHub.Repository.Data.Config
{
    internal class PatientsConfigrations : IEntityTypeConfiguration<Patients>
    {
        public void Configure(EntityTypeBuilder<Patients> builder)
        {
            builder.Property(P => P.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(P => P.MiddleName)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(P => P.LastName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(P => P.PhoneNumber)
               .IsRequired()
               .HasMaxLength(11);



        }
    }
}
