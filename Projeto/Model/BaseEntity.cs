using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Model
{
    public class BaseEntity
    {
        [Key]
        [Column(name: "ID")]
        public virtual int Id { get; set; }
    }
}
