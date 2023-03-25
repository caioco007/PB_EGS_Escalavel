using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }

        protected BaseEntity()
        {

        }
    }
}
