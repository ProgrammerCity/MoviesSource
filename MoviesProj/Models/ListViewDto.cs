using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesProj.Models
{
    public record ListViewDto
    {
        public string Titele { get; init; } = default!;
        public ListViewDto(string titele)
        {
            Titele = titele;
        }
    }
}
