using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Reflection;

namespace ItemModifier
{
    public class ItemModifier : RocketPlugin<ItemModifierConfiguration>
    {
        public static ItemModifier Instance;

        internal static class Fields
        {
            internal static FieldInfo Width;
            internal static FieldInfo Height;

            internal static FieldInfo Barricade_Health;
            internal static FieldInfo Barricade_Explosion;

            internal static FieldInfo Structure_Health;
            internal static FieldInfo Structure_Explosion;

            internal static FieldInfo Barrel_Braked;
            internal static FieldInfo Barrel_Silenced;
            internal static FieldInfo Barrel_Volume;
            internal static FieldInfo Barrel_BallisticDrop;
        }

        protected override void Load()
        {
            Instance = this;

            Log("Item Modifier by SilK");
            Log("Loading item modifications...");

            Type type = typeof(ItemBagAsset);
            Fields.Width = type.GetField("_width", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Height = type.GetField("_height", BindingFlags.NonPublic | BindingFlags.Instance);

            type = typeof(ItemBarricadeAsset);
            Fields.Barricade_Health = type.GetField("_health", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Barricade_Explosion = type.GetField("_explosion", BindingFlags.NonPublic | BindingFlags.Instance);

            type = typeof(ItemStructureAsset);
            Fields.Structure_Health = type.GetField("_health", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Structure_Explosion = type.GetField("_explosion", BindingFlags.NonPublic | BindingFlags.Instance);

            type = typeof(ItemBarrelAsset);
            Fields.Barrel_BallisticDrop = type.GetField("_ballisticDrop", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Barrel_Braked = type.GetField("_isBraked", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Barrel_Silenced = type.GetField("_isSilenced", BindingFlags.NonPublic | BindingFlags.Instance);
            Fields.Barrel_Volume = type.GetField("_volume", BindingFlags.NonPublic | BindingFlags.Instance);

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
                LogError("Item ID {0} is invalid.", modification.ID);
                return;
            }

            if (asset.isPro)
            {
                LogError("Item ID {0} is a cosmetic.", modification.ID);
                return;
            }

            if (modification.Height.HasValue || modification.Width.HasValue)
            {
                if (asset is ItemBagAsset)
                {
                    ItemBagAsset bagAsset = asset as ItemBagAsset;
                    if (modification.Width.HasValue) SetWidth(bagAsset, modification.Width.Value);
                    if (modification.Height.HasValue) SetHeight(bagAsset, modification.Height.Value);
                }
                else
                {
                    LogError("Item ID {0} isn't a clothing item with storage.", modification.ID);
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
                    LogError("Item ID {0} isn't a structure or barricade.", modification.ID);
                }
            }

            if (modification.PlayerDamage.HasValue ||
                modification.PlayerLegMultiplier.HasValue || 
                modification.PlayerArmMultiplier.HasValue ||
                modification.PlayerSpineMultiplier.HasValue ||
                modification.PlayerSkullMultiplier.HasValue ||
                modification.ZombieDamage.HasValue ||
                modification.ZombieLegMultiplier.HasValue ||
                modification.ZombieArmMultiplier.HasValue ||
                modification.ZombieSpineMultiplier.HasValue ||
                modification.ZombieSkullMultiplier.HasValue ||
                modification.AnimalDamage.HasValue ||
                modification.AnimalLegMultiplier.HasValue ||
                modification.AnimalSpineMultiplier.HasValue ||
                modification.AnimalSkullMultiplier.HasValue ||
                modification.BarricadeDamage.HasValue ||
                modification.StructureDamage.HasValue ||
                modification.VehicleDamage.HasValue || 
                modification.ResourceDamage.HasValue ||
                modification.ObjectDamage.HasValue ||
                modification.Invulnerable.HasValue) // Isn't this pretty?
            {
                if (asset is ItemWeaponAsset)
                {
                    ItemWeaponAsset weaponAsset = asset as ItemWeaponAsset;
                    if (modification.PlayerDamage.HasValue) SetPlayerDamage(weaponAsset, modification.PlayerDamage.Value);
                    if (modification.PlayerLegMultiplier.HasValue) SetPlayerLegMultiplier(weaponAsset, modification.PlayerLegMultiplier.Value);
                    if (modification.PlayerArmMultiplier.HasValue) SetPlayerArmMultiplier(weaponAsset, modification.PlayerArmMultiplier.Value);
                    if (modification.PlayerSpineMultiplier.HasValue) SetPlayerSpineMultiplier(weaponAsset, modification.PlayerSpineMultiplier.Value);
                    if (modification.PlayerSkullMultiplier.HasValue) SetPlayerSkullMultiplier(weaponAsset, modification.PlayerSkullMultiplier.Value);
                    if (modification.ZombieDamage.HasValue) SetZombieDamage(weaponAsset, modification.ZombieDamage.Value);
                    if (modification.ZombieLegMultiplier.HasValue) SetZombieLegMultiplier(weaponAsset, modification.ZombieLegMultiplier.Value);
                    if (modification.ZombieArmMultiplier.HasValue) SetZombieArmMultiplier(weaponAsset, modification.ZombieArmMultiplier.Value);
                    if (modification.ZombieSpineMultiplier.HasValue) SetZombieSpineMultiplier(weaponAsset, modification.ZombieSpineMultiplier.Value);
                    if (modification.ZombieSkullMultiplier.HasValue) SetZombieSkullMultiplier(weaponAsset, modification.ZombieSkullMultiplier.Value);
                    if (modification.AnimalDamage.HasValue) SetAnimalDamage(weaponAsset, modification.AnimalDamage.Value);
                    if (modification.AnimalLegMultiplier.HasValue) SetAnimalLegMultiplier(weaponAsset, modification.AnimalLegMultiplier.Value);
                    if (modification.AnimalSpineMultiplier.HasValue) SetAnimalSpineMultiplier(weaponAsset, modification.AnimalSpineMultiplier.Value);
                    if (modification.AnimalSkullMultiplier.HasValue) SetAnimalSkullMultiplier(weaponAsset, modification.AnimalSkullMultiplier.Value);
                    if (modification.BarricadeDamage.HasValue) SetBarricadeDamage(weaponAsset, modification.BarricadeDamage.Value);
                    if (modification.StructureDamage.HasValue) SetStructureDamage(weaponAsset, modification.StructureDamage.Value);
                    if (modification.VehicleDamage.HasValue) SetVehicleDamage(weaponAsset, modification.VehicleDamage.Value);
                    if (modification.ResourceDamage.HasValue) SetResourceDamage(weaponAsset, modification.ResourceDamage.Value);
                    if (modification.ObjectDamage.HasValue) SetObjectDamage(weaponAsset, modification.ObjectDamage.Value);
                    if (modification.Invulnerable.HasValue) SetIsInvulnerable(weaponAsset, modification.Invulnerable.Value);
                }
                else
                {
                    LogError("Item ID {0} isn't a weapon.", modification.ID);
                }
            }

            if (modification.Caliber.HasValue || 
                modification.Range.HasValue ||
                modification.SpreadAim.HasValue ||
                modification.SpreadHip.HasValue ||
                modification.Muzzle.HasValue)
            {
                if (asset is ItemWeaponAsset)
                {
                    ItemGunAsset gunAsset = asset as ItemGunAsset;
                    if (modification.Caliber.HasValue) SetCaliber(gunAsset, modification.Caliber.Value);
                    if (modification.Range.HasValue) SetRange(gunAsset, modification.Range.Value);
                    if (modification.SpreadAim.HasValue) SetSpreadAim(gunAsset, modification.SpreadAim.Value);
                    if (modification.SpreadHip.HasValue) SetSpreadHip(gunAsset, modification.SpreadHip.Value);
                    if (modification.Muzzle.HasValue) SetMuzzle(gunAsset, modification.Muzzle.Value);
                }
                else
                {
                    LogError("Item ID {0} isn't a gun.", modification.ID);
                }
            }

            if (modification.Explosion.HasValue)
            {
                if (asset is ItemStructureAsset)
                {
                    SetExplosion(asset as ItemStructureAsset, modification.Explosion.Value);
                }
                else if (asset is ItemBarricadeAsset)
                {
                    SetExplosion(asset as ItemBarricadeAsset, modification.Explosion.Value);
                }
                else if (asset is ItemGunAsset)
                {
                    SetExplosion(asset as ItemGunAsset, modification.Explosion.Value);
                }
                else
                {
                    LogError("Item ID {0} doesn't have an explosion tag.");
                }
            }
            
            if (modification.BallisticDrop.HasValue ||
                modification.Braked.HasValue ||
                modification.Silenced.HasValue ||
                modification.Volume.HasValue)
            {
                if (asset is ItemBarrelAsset)
                {
                    ItemBarrelAsset barrelAsset = asset as ItemBarrelAsset;
                    if (modification.BallisticDrop.HasValue) SetBallisticDrop(barrelAsset, modification.BallisticDrop.Value);
                    if (modification.Braked.HasValue) SetIsBraked(barrelAsset, modification.Braked.Value);
                    if (modification.Silenced.HasValue) SetIsSilenced(barrelAsset, modification.Silenced.Value);
                    if (modification.Volume.HasValue) SetVolume(barrelAsset, modification.Volume.Value);
                }
                else
                {
                    LogError("Item ID {0} isn't a barrel.");
                }
            }
        }

        #region Bag
        public static bool SetWidth(ItemBagAsset asset, byte width)
        {
            if (Fields.Width == null)
            {
                LogError("Setting width of Item ID {0}", asset.id);
                return false;
            }

            Fields.Width.SetValue(asset, width);
            return true;
        }

        public static bool SetHeight(ItemBagAsset asset, byte height)
        {            
            if (Fields.Height == null)
            {
                LogError("Setting height of Item ID {0}", asset.id);
                return false;
            }

            Fields.Height.SetValue(asset, height);
            return true;
        }
        #endregion

        #region Barricade/Structure
        public static bool SetHealth(ItemBarricadeAsset asset, ushort health)
        {
            if (Fields.Barricade_Health == null)
            {
                LogError("Setting health of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barricade_Health.SetValue(asset, health);
            return true;
        }

        public static bool SetExplosion(ItemBarricadeAsset asset, ushort explosion)
        {
            if (Fields.Barricade_Explosion == null)
            {
                LogError("Setting explosion of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barricade_Explosion.SetValue(asset, explosion);
            return true;
        }

        public static bool SetHealth(ItemStructureAsset asset, ushort health)
        {
            if (Fields.Structure_Health == null)
            {
                LogError("Setting health of Item ID {0}", asset.id);
                return false;
            }

            Fields.Structure_Health.SetValue(asset, health);
            return true;
        }

        public static bool SetExplosion(ItemStructureAsset asset, ushort explosion)
        {
            if (Fields.Structure_Explosion == null)
            {
                LogError("Setting explosion of Item ID {0}", asset.id);
                return false;
            }

            Fields.Structure_Explosion.SetValue(asset, explosion);
            return true;
        }
        #endregion

        #region Weapon
        public static bool SetPlayerDamage(ItemWeaponAsset asset, float damage)
        {
            asset.playerDamageMultiplier.damage = damage;
            return true;
        }
        public static bool SetPlayerLegMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.playerDamageMultiplier.leg = multiplier;
            return true;
        }
        public static bool SetPlayerArmMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.playerDamageMultiplier.arm = multiplier;
            return true;
        }
        public static bool SetPlayerSpineMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.playerDamageMultiplier.spine = multiplier;
            return true;
        }
        public static bool SetPlayerSkullMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.playerDamageMultiplier.skull = multiplier;
            return true;
        }


        public static bool SetZombieDamage(ItemWeaponAsset asset, float damage)
        {
            asset.zombieDamageMultiplier.damage = damage;
            return true;
        }
        public static bool SetZombieLegMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.zombieDamageMultiplier.leg = multiplier;
            return true;
        }
        public static bool SetZombieArmMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.zombieDamageMultiplier.arm = multiplier;  
            return true;
        }
        public static bool SetZombieSpineMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.zombieDamageMultiplier.spine = multiplier;
            return true;
        }
        public static bool SetZombieSkullMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.zombieDamageMultiplier.skull = multiplier;
            return true;
        }

        public static bool SetAnimalDamage(ItemWeaponAsset asset, float damage)
        {
            asset.animalDamageMultiplier.damage = damage;
            return true;
        }
        public static bool SetAnimalLegMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.animalDamageMultiplier.leg = multiplier;
            return true;
        }
        public static bool SetAnimalSpineMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.animalDamageMultiplier.spine = multiplier;
            return true;
        }
        public static bool SetAnimalSkullMultiplier(ItemWeaponAsset asset, float multiplier)
        {
            asset.animalDamageMultiplier.skull = multiplier;
            return true;
        }

        public static bool SetBarricadeDamage(ItemWeaponAsset asset, float damage)
        {
            asset.barricadeDamage = damage;
            return true;
        }
        public static bool SetStructureDamage(ItemWeaponAsset asset, float damage)
        {
            asset.structureDamage = damage;
            return true;
        }
        public static bool SetVehicleDamage(ItemWeaponAsset asset, float damage)
        {
            asset.vehicleDamage = damage;
            return true;
        }
        public static bool SetResourceDamage(ItemWeaponAsset asset, float damage)
        {
            asset.resourceDamage = damage;
            return true;
        }
        public static bool SetObjectDamage(ItemWeaponAsset asset, float damage)
        {
            asset.objectDamage = damage;
            return true;
        }

        public static bool SetIsInvulnerable(ItemWeaponAsset asset, bool isInvulnerable)
        {
            asset.isInvulnerable = isInvulnerable;
            return true;
        }
        #endregion

        #region Guns
        public static bool SetCaliber(ItemGunAsset asset, ushort caliber)
        {
            asset.caliber = caliber;
            return true;
        }

        public static bool SetRange(ItemGunAsset asset, float range)
        {
            asset.range = range;
            return true;
        }

        public static bool SetSpreadAim(ItemGunAsset asset, float spread)
        {
            asset.spreadAim = spread;
            return true;
        }

        public static bool SetSpreadHip(ItemGunAsset asset, float spread)
        {
            asset.spreadHip = spread;
            return true;
        }

        public static bool SetMuzzle(ItemGunAsset asset, ushort muzzle)
        {
            asset.muzzle = muzzle;
            return true;
        }

        public static bool SetExplosion(ItemGunAsset asset, ushort explosion)
        {
            asset.explosion = explosion;
            return true;
        }
        #endregion

        #region Barrels
        public static bool SetBallisticDrop(ItemBarrelAsset asset, float ballisticDrop)
        {
            if (Fields.Barrel_BallisticDrop == null)
            {
                LogError("Setting ballistic drop of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barrel_BallisticDrop.SetValue(asset, ballisticDrop);
            return true;
        }

        public static bool SetIsBraked(ItemBarrelAsset asset, bool isBraked)
        {
            if (Fields.Barrel_Braked == null)
            {
                LogError("Setting is braked of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barrel_Braked.SetValue(asset, isBraked);
            return true;
        }

        public static bool SetIsSilenced(ItemBarrelAsset asset, bool isSilenced)
        {
            if (Fields.Barrel_Silenced == null)
            {
                LogError("Setting is silenced of Item ID {0}", asset.id);
                return false;
            }

            Fields.Barrel_Silenced.SetValue(asset, isSilenced);
            return true;
        }

        public static bool SetVolume(ItemBarrelAsset asset, float volume)
        {
            if (Fields.Barrel_Volume == null)
            {
                LogError("Setting volume of Item ID {0}", asset.id);
                return true;
            }

            Fields.Barrel_Volume.SetValue(asset, volume);
            return true;
        }
        #endregion

        internal static void Log(string format, params object[] parameters)
        {
            Logger.Log(string.Format(format, parameters), ConsoleColor.Green);
        }

        internal static void LogError(string format, params object[] parameters)
        {
            Logger.Log("Error: " + string.Format(format, parameters), ConsoleColor.Red);
        }
    }
}
