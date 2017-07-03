using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemModifier
{
    public class ItemModification
    {
        [XmlAttribute]
        public ushort ID;

        [XmlElement(IsNullable = false)]
        public byte? Width;
        [XmlElement(IsNullable = false)]
        public byte? Height;

        [XmlElement(IsNullable = false)]
        public ushort? Health;

        [XmlElement(IsNullable = false)]
        public float? PlayerDamage;
        [XmlElement(IsNullable = false)]
        public float? PlayerLegMultipler;
        [XmlElement(IsNullable = false)]
        public float? PlayerArmMultipler;
        [XmlElement(IsNullable = false)]
        public float? PlayerSpineMultipler;
        [XmlElement(IsNullable = false)]
        public float? PlayerSkullMultipler;

        [XmlElement(IsNullable = false)]
        public float? ZombieDamage;
        [XmlElement(IsNullable = false)]
        public float? ZombieLegMultipler;
        [XmlElement(IsNullable = false)]
        public float? ZombieArmMultipler;
        [XmlElement(IsNullable = false)]
        public float? ZombieSpineMultipler;
        [XmlElement(IsNullable = false)]
        public float? ZombieSkullMultipler;

        [XmlElement(IsNullable = false)]
        public float? AnimalDamage;
        [XmlElement(IsNullable = false)]
        public float? AnimalLegMultipler;
        [XmlElement(IsNullable = false)]
        public float? AnimalSpineMultipler;
        [XmlElement(IsNullable = false)]
        public float? AnimalSkullMultipler;

        [XmlElement(IsNullable = false)]
        public float? BarricadeDamage;
        [XmlElement(IsNullable = false)]
        public float? StructureDamage;
        [XmlElement(IsNullable = false)]
        public float? VehicleDamage;
        [XmlElement(IsNullable = false)]
        public float? ResourceDamage;
        [XmlElement(IsNullable = false)]
        public float? ObjectDamage;
    }

    public class ItemModifierConfiguration : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "Item")]
        public List<ItemModification> Items;

        public void LoadDefaults()
        {
            Items = new List<ItemModification>();
        }
    }
}
