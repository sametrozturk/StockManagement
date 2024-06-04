using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using StockManagement.Domain.Common;
using StockManagement.Domain.Identity.DomainEvents;

namespace StockManagement.Domain.Identity
{
    public class User : IdentityUser<Guid>, IAggregateRoot
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        private User(string firstName, string lastName, string phoneNumber, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            UserName = email + "_" + Guid.NewGuid().ToString();
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static User CreateNew(
            string firstName,
            string lastName,
            string phoneNumber,
            string email
        )
        {
            var user = new User(firstName, lastName, phoneNumber, email);
            user.RaiseDomainEvent(new NewUserCreatedDomainEvent(user.Id));
            return user;
        }

        [NotMapped]
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
