using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Assets.AssetClasses
{
    public class AssetClassManager : DomainService
    {
        private readonly IAssetClassRepository _assetClassRepository;
        public AssetClassManager(IAssetClassRepository assetClassRepository)
        {
            _assetClassRepository = assetClassRepository;
        }

        public async Task<AssetClass> CreateAsync(Guid id, [NotNull] string code, [NotNull] string name, [NotNull] bool isComponent,
                                                    [NotNull] Guid parent, [NotNull] string notes, [NotNull] bool status)
        {

            //validate name
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingAssetClassByName = await _assetClassRepository.FindByNameAsync(name);
            if (existingAssetClassByName != null)
            {
                throw new AssetClassAlreadyExistsByNameExcepiton(name);
            }

 
            //validate code
            int serialNumber = 1; //if type is class set  value as 1
            string parentClassCode = string.Empty;

            if (parent != Guid.Empty)
            {
                //if type is sub-class or component get parent and new serial number
                var  parentClass = await _assetClassRepository.GetAsync(parent);
                parentClassCode = parentClass.Code;
                serialNumber = await GenerateNewSerialNumber(parent);
            }

            var newAssetClass = new AssetClass(GuidGenerator.Create(), code, serialNumber, name, isComponent, parent,
                                                parentClassCode, notes, status);

            var existingAssetClassByCode = await _assetClassRepository.FindByAssetClassCodeAsync(newAssetClass.Code);
            if (existingAssetClassByCode != null)
            {
                throw new AssetClassAlreadyExistsByCodeException(newAssetClass.Code);
            }
             

            return newAssetClass;
        }

        private async Task<int> GenerateNewSerialNumber(Guid parent)
        {
            // get the Maximum value of the serial numbers assigned under the parent.
            var maxValue = await _assetClassRepository.GetMaxValueOfSerialNumber(parent);
            var newSerialNumber = maxValue + 1;// increment  the maxValue by 1 to generate new serial number

            return newSerialNumber;  
        }

        public async Task ChangeNameAsync([NotNull] AssetClass assetClass, [NotNull] string newName)
        {
            Check.NotNull(assetClass, nameof(assetClass));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAssetClass = await _assetClassRepository.FindByNameAsync(newName);
            if (existingAssetClass != null && existingAssetClass.Id != assetClass.Id)
            {
                throw new AssetClassAlreadyExistsByNameExcepiton(newName);
            }

            assetClass.ChangeName(newName);

        }

        public async Task ChangeCodeAsync([NotNull] AssetClass assetClass, [NotNull] string newCode)
        {
            Check.NotNull(assetClass, nameof(assetClass));
            Check.NotNullOrWhiteSpace(newCode, nameof(newCode));

            var existingAssetClass = await _assetClassRepository.FindByAssetClassCodeAsync(newCode);
            if (existingAssetClass != null && existingAssetClass.Code != assetClass.Code)
            {
                throw new AssetClassAlreadyExistsByCodeException(newCode);
            }

            var parentOfExistingAssetClass = await _assetClassRepository.FindAsync(assetClass.Parent);
            assetClass.ChangeCode(newCode, parentOfExistingAssetClass.Code);

        }
    }
}
