using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bindu.Sampatti.Departments
{
    public class Department : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }

        private Department()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Department(Guid id, [NotNull] string name,  [CanBeNull] string notes, bool status) : base(id)
        {
            SetName(name);
            Notes = notes;
            Status = status;
        }

        internal Department ChangeName([NotNull] string newName)
        {
            SetName(newName);
            return this;
        }


        private void SetName([NotNull] string newName)
        {
            Name = Check.NotNullOrWhiteSpace(newName, nameof(newName), DepartmentConsts.MaxNameLength);
        }
    }
}
