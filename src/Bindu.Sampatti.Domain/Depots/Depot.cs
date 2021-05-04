using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bindu.Sampatti.Depots
{
    public class Depot : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Guid Location { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }

        private Depot()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Depot(Guid id,[NotNull]string name,[CanBeNull]Guid location, [CanBeNull]string notes, bool status):base(id)
        {
            SetName(name);
            Location = location;
            Notes = notes;
            Status = status;
        }

        internal Depot ChangeName([NotNull]string newName)
        {
            SetName(newName);
            return this;    
        }


        private void SetName([NotNull]string newName)
        {
            Name = Check.NotNullOrWhiteSpace(newName, nameof(newName), DepotConsts.MaxNameLength);
        }
    }
}
