using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace products_ccdigital.Domain.Common
{
    /// <summary>
    /// Entidad de auditoria de otras entidades
    /// </summary>
    public class AuditableEntity
    {
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public Guid RowVersion { get; set; }
    }
}
