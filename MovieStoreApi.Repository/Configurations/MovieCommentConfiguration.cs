using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStoreApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Repository.Configurations
{
    internal class MovieCommentConfiguration : IEntityTypeConfiguration<MovieComment>
    {
        public void Configure(EntityTypeBuilder<MovieComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(m => m.Rating).HasColumnType("decimal(2,1)").IsRequired(true);

            builder.HasOne(m => m.Movie).WithMany(a => a.Comments).HasForeignKey(m => m.MovieId);

            builder.ToTable("MovieComments");
        }
    }
}
