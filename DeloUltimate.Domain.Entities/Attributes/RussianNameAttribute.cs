using System;


namespace DeloUltimate.Domain.Entities.Attributes
{
    public class RussianNameAttribute : Attribute
    {
        public string Name;

        public RussianNameAttribute() { }

        public RussianNameAttribute(string name)
        {
            Name = name;
        }
    }
}
