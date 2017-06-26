using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ItemModifier
{
    public class ItemModification
    {
        [XmlAttribute]
        public ushort ID;

        public byte? Width;
        public byte? Height;

        public ushort? Health;
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
