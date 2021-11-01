using System;

namespace immersed.dive.shop.model
{
    public class Person : IEntity
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string OtherNames { get; set; }
        public string KnownAs { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public string IdentifiesAs { get; set; }
    }
}