using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoId { get; set; }
        public IEnumerable<State> States { get; set; }
    }
}
