using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bindu.Sampatti.Employees
{
    public class Employee : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid Designation { get; set; }
        public Guid Department { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }

        private Employee()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal Employee(Guid id, [NotNull] string name, [NotNull] string code, [CanBeNull] Guid designation, 
            [CanBeNull] Guid department, [CanBeNull] string notes, bool status) : base(id)
        {

            SetName(name);
            SetCode(code);

            Designation = designation;
            Department = department;
            Notes = notes;
            Status = status;
        }

        internal Employee ChangeName([NotNull] string newName)
        {
            SetName(newName);
            return this;
        }

        internal Employee ChangeCode([NotNull] string newCode)
        {
            SetCode(newCode);
            return this;
        }

        private void SetName([NotNull] string newName)
        {
            Name = Check.NotNullOrWhiteSpace(newName, nameof(newName), EmployeeConsts.MaxNameLength);
        }

        private void SetCode([NotNull] string newCode)
        {
            Code = Check.NotNullOrWhiteSpace(newCode, nameof(newCode), EmployeeConsts.MaxCodeLength);
        }
    }
}
