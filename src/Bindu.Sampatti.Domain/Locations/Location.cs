using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bindu.Sampatti.Locations
{
    public class Location : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool IsEnabled { get; set; }

        private Location()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Location(Guid id, [NotNull]string name, [CanBeNull]string notes, bool isEnabled):base(id)
        {
            Notes = notes;
            IsEnabled = isEnabled;
        }

        internal Location ChangeName([NotNull]string newName)
        {
            SetName(newName);
            return this;
        }

        private void SetName([NotNull]string newName)
        {
            Name = Check.NotNullOrWhiteSpace(
                newName, 
                nameof(newName), 
                maxLength: LocationConsts.MaxNameLength);
        }
    }

}
