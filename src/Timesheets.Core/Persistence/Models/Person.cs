using Microsoft.EntityFrameworkCore;

namespace Timesheets.Core.Persistence.Models
{
    [PrimaryKey(nameof(Id))]
    public class Person
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
