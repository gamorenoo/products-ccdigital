using products_ccdigital.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace products_ccdigital.Domain.Products
{
    public class Product: AuditableEntity
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Código del producto.
        /// </summary>
        [Required]
        public string Code { get; set; } = null!;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        [Required]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Precio del producto.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Cantidad existente del producto.
        /// </summary>
        [Required]
        public int Stock { get; set; }

    }
}
