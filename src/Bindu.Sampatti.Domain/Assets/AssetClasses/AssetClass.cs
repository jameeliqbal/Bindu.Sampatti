using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bindu.Sampatti.Assets.AssetClasses
{
    public  class AssetClass : AuditedAggregateRoot<Guid>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public AssetClassType Type { get; private set; }
        public Guid Parent { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public int SerialNumber { get; set; }

        private AssetClass()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal AssetClass(Guid id,[NotNull] string code, [NotNull] int serialNumber, [NotNull]string name, [NotNull]bool IsComponent,  [NotNull]Guid parent, [CanBeNull] string notes,  [CanBeNull]bool status)
        {
            SetName(name);
            SetCode(code);
            SetType(IsComponent, parent);

            SerialNumber = serialNumber;
            Parent = parent;
            Notes = notes;
            Status = status;
        }

        private void SetType(bool isComponent, Guid parent)
        {
            if (parent== Guid.Empty)
            {
                Type = AssetClassType.Class;
            }
            else
            {
                if (isComponent)
                {
                    Type = AssetClassType.Component;
                }
                else
                {
                    Type = AssetClassType.SubClass;
                }
            }
              
        }

        private void SetName([NotNull] string newName)
        {
            Name = Check.NotNullOrWhiteSpace(newName, nameof(newName), AssetClassConsts.MaxNameLength);
        }

        internal AssetClass ChangeName([NotNull] string newName)
        {
            SetName(newName);
            return this;
        }

        internal AssetClass ChangeCode([NotNull] string newCode)
        {
            SetCode(newCode);
            return this;
        }

        private void SetCode([NotNull] string newCode)
        {
            Code = Check.NotNullOrWhiteSpace(newCode, nameof(newCode));
        }
    }
}
