using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainCore.Entities
{
    public abstract class Entity : Notifiable<Notification>, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;
    }
}
