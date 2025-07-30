using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FiltersModel
{
    public class FilterRoleTable
    {
        public string? Name { get; set; }
        public DateTime? CreatedDateBetweenDates { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
