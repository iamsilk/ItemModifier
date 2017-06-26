using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ItemModifier
{
    public class ItemModifier : RocketPlugin<ItemModifierConfiguration>
    {
        public static ItemModifier Instance;

        internal static class Fields
        {
            internal static FieldInfo Width;
            internal static FieldInfo Height;

            internal static FieldInfo Structure_Health;
            internal static FieldInfo Barricade_Health;
        }

        protected override void Load()
        {
            Instance = this;

            Log("Item Modifier by SilK");
            Log("Loading item modifications...");
            
            Type type = typeof(ItemBagAsset);
            Fields.Width = type.GetField("_width", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Height = type.GetField("_height", BindingFlags.NonPublic | BindingFlags.Instance);

            type = typeof(ItemStructureAsset);
            Fields.Structure_Health = type.GetField("_health", BindingFlags.NonPublic | BindingFlags.Instance);

            type = typeof(ItemBarricadeAsset);
            Fields.Barricade_Health = type.GetField("_health", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (ItemModification modification in Configuration.Instance.Items)
            {
                Modify(modification);
            }
        }

        internal static void Modify(ItemModification modification)
        {
            ItemAsset asset = (ItemAsset)Assets.find(EAssetType.ITEM, modification.ID);
            if (asset == null)
            {
                LogError("Error: Item ID {0} is invalid.", modification.ID);
                return;
            }

            if (asset.isPro)
            {
                LogError("Error: Item ID {0} is a cosmetic.", modification.ID);
                return;
            }

            if (modification.Height.HasValue || modification.Width.HasValue)
            {
                ItemBagAsset bagAsset = asset as ItemBagAsset;
                if (bagAsset == null)
                {
                    LogError("Error: Item ID {0} has no width/height settings.", modification.ID);
                }
                else
                {
                    if (modification.Width.HasValue) SetWidth(bagAsset, modification.Width.Value);
                    if (modification.Height.HasValue) SetHeight(bagAsset, modification.Height.Value);
                }
            }

            if (modification.Health.HasValue)
            {
                if (asset is ItemBarricadeAsset)
                {
                    SetHealth(asset as ItemBarricadeAsset, modification.Health.Value);
                }
                else if (asset is ItemStructureAsset)
                {
                    SetHealth(asset as ItemStructureAsset, modification.Health.Value);
                }
                else
                {
                    LogError("Error: Item ID {0} has no health setting.");
                }
            }
        }

        public static bool SetWidth(ItemBagAsset asset, byte width)
        {
            if (Fields.Width == null)
            {
                LogError("Error: Setting width of Item ID {0}", asset.id);
                return false;
            }

            Fields.Width.SetValue(asset, width);

            return true;
        }

        public static bool SetHeight(ItemBagAsset asset, byte height)
        {            
            if (Fields.Height == null)
            {
                LogError("Error: Setting height of Item ID {0}", asset.id);
                return false;
            }

            Fields.Height.SetValue(asset, height);

            return true;
        }

        public static bool SetHealth(ItemBarricadeAsset asset, ushort health)
        {
            if (Fields.Barricade_Health == null)
            {
                LogError("Error: Setting health of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barricade_Health.SetValue(asset, health);

            return true;
        }

        public static bool SetHealth(ItemStructureAsset asset, ushort health)
        {
            if (Fields.Structure_Health == null)
            {
                LogError("Error: Setting health of Item ID {0}", asset.id);
                return false;
            }

            Fields.Structure_Health.SetValue(asset, health);

            return true;
        }

        

        internal static void Log(string format, params object[] parameters)
        {
            Logger.Log(string.Format(format, parameters), ConsoleColor.Green);
        }

        internal static void LogError(string format, params object[] parameters)
        {
            Logger.Log(string.Format(format, parameters), ConsoleColor.Red);
        }
    }
}
