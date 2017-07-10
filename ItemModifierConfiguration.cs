using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemModifier
{
    public class ItemModification
    {
        [XmlAttribute]
        public ushort ID;

        #region Bags
        [XmlElement(IsNullable = false)]
        public byte? Width;
        [XmlElement(IsNullable = false)]
        public byte? Height;
        #endregion

        #region Structures/Barricades
        [XmlElement(IsNullable = false)]
        public ushort? Health;
        #endregion

        #region Weapons
        [XmlElement(IsNullable = false)]
        public float? PlayerDamage;
        [XmlElement(IsNullable = false)]
        public float? PlayerLegMultiplier;
        [XmlElement(IsNullable = false)]
        public float? PlayerArmMultiplier;
        [XmlElement(IsNullable = false)]
        public float? PlayerSpineMultiplier;
        [XmlElement(IsNullable = false)]
        public float? PlayerSkullMultiplier;

        [XmlElement(IsNullable = false)]
        public float? ZombieDamage;
        [XmlElement(IsNullable = false)]
        public float? ZombieLegMultiplier;
        [XmlElement(IsNullable = false)]
        public float? ZombieArmMultiplier;
        [XmlElement(IsNullable = false)]
        public float? ZombieSpineMultiplier;
        [XmlElement(IsNullable = false)]
        public float? ZombieSkullMultiplier;

        [XmlElement(IsNullable = false)]
        public float? AnimalDamage;
        [XmlElement(IsNullable = false)]
        public float? AnimalLegMultiplier;
        [XmlElement(IsNullable = false)]
        public float? AnimalSpineMultiplier;
        [XmlElement(IsNullable = false)]
        public float? AnimalSkullMultiplier;

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

        [XmlElement(IsNullable = false)]
        public bool? Invulnerable;
        #endregion

        #region Guns
        [XmlElement(IsNullable = false)]
        public ushort? Caliber;
        [XmlElement(IsNullable = false)]
        public float? Range;
        [XmlElement(IsNullable = false)]
        public float? SpreadAim;
        [XmlElement(IsNullable = false)]
        public float? SpreadHip;
        [XmlElement(IsNullable = false)]
        public ushort? Muzzle;
        [XmlElement(IsNullable = false)]
        public ushort? Explosion; // For barricades/structures also
        #endregion

        #region Barrels
        [XmlElement(IsNullable = false)]
        public float? BallisticDrop;
        [XmlElement(IsNullable = false)]
        public bool? Braked;
        [XmlElement(IsNullable = false)]
        public bool? Silenced;
        [XmlElement(IsNullable = false)]
        public float? Volume;
        #endregion
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
