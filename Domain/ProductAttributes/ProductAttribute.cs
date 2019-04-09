using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Domain.Common.Domain;
using Domain.ProductAttributes.Factory;

namespace Domain.ProductAttributes
{
    public enum AttributeType
    {   
        [EnumMember(Value = "Discrete")]
        Discrete = 1,
        [EnumMember(Value = "Continuous")]
        Continuous = 2,
    }
    public abstract class ProductAttribute: ValueObject
    {
        public string Name { get; private set; }
        
        public IList<AttributeOption> AttributeOptions { get; protected set; }
        
        protected ProductAttribute(){}
        public ProductAttribute(string name)
        {
            AssertionConcerns.AssertArgumentNotEmpty(name,"ProductAttribute Name cannot be Empty");
            Name = name;
        }

        public bool isValidOption(AttributeOption option)
        {
            if (option.Equals(AttributeOption.AnyValue)) return true;
            return checkIsValidOption(option);
        }
        
        protected abstract bool checkIsValidOption(AttributeOption option);

        public bool isBaseAttribute(string option)
        {
            return option.Equals("ANY");
        }

        public abstract AttributeType GetAttributeType();
        
        public override IEnumerable<object> GetMembersForEqualityComparision()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return $"AttributeName: {Name}";
        }
    }
}