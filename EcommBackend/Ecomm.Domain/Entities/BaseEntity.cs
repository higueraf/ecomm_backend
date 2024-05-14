using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public Guid CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? DeleteBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int State { get; set; }
    }
}
