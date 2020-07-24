using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
