using MelonLoader;
using BTD_Mod_Helper;
using MiniTowersCollection;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(MiniTowersCollection.MiniTowersCollection), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MiniTowersCollection;

public class MiniTowersCollection : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<MiniTowersCollection>("Upgradeable Legend loaded!");
        ModHelper.Msg<MiniTowersCollection>("MiniTowersCollection loaded!");
    }

    public class UpgradeableLegendTower
    {
        public class UpgradeableLegend : ModTower
        {
            public override TowerSet TowerSet => TowerSet.Magic;
            public override string BaseTower => "SuperMonkey-005";
            public override string DisplayName => "Upgradable Legend";
            public override string Description => "The Legend Of The Night as a upgradeable tower.";
            public override int Cost => 200000;
            public override int TopPathUpgrades => 2;
            public override int MiddlePathUpgrades => 2;
            public override int BottomPathUpgrades => 5;
            public override int GetTowerIndex(List<TowerDetailsModel> towerSet) => towerSet.First(model => model.towerId == TowerType.SuperMonkey).towerIndex + 1;
            public override bool IsValidCrosspath(int[] tiers) => ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
            public override void ModifyBaseTowerModel(TowerModel towerModel) { }
            public override string Icon => VanillaSprites.SuperMonkey005;
            public override string Portrait => VanillaSprites.SuperMonkey005;
        }
        public class Top1_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => TOP;
            public override int Tier => 1;
            public override string DisplayName => "Laser Blasts";
            public override string Description => "Shoots powerful blasts of laser instead of darts.";
            public override int Cost => 2500;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Top1Display_Legend>();

                towerModel.GetWeapon().projectile.display = new() { guidRef = "5d88a6eeaf733324ea8fcfc9d19013b3" };
                towerModel.GetWeapon().projectile.pierce += 1;
            }
            public override string Icon => "LaserBlastUpgradeIcon";
            internal class Top1Display_Legend : ModDisplay
            {
                public override string BaseDisplay => "791716873c2d0bb49b9c94e965eee468";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                }
            }
        }
        public class Top2_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => TOP;
            public override int Tier => 2;
            public override string DisplayName => "Plasma Blasts";
            public override string Description => "Super fast plasma vaporizes everything it touches.";
            public override int Cost => 3000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Top2Display_Legend>();

                towerModel.GetWeapon().projectile.display = new() { guidRef = "98010195051b16341bab67a674472835" };
                towerModel.GetWeapon().rate *= 0.6667f;
            }
            public override string Icon => "PlasmaBlastUpgradeIcon";
            internal class Top2Display_Legend : ModDisplay
            {
                public override string BaseDisplay => "e6c683076381222438dfc733a602c157";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                }
            }
        }
        public class Middle1_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override string DisplayName => "Super Range";
            public override string Description => "Super Monkeys need Super Range.";
            public override int Cost => 1000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range = towerModel.GetAttackModel().range += 10;
                towerModel.GetWeapon().projectile.pierce += 1;
            }
            public override string Icon => "SuperRangeUpgradeIcon";
        }
        public class Middle2_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override string DisplayName => "Epic Range";
            public override string Description => "Why settle for super when you can have EPIC?";
            public override int Cost => 1400;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range = towerModel.GetAttackModel().range += 12;
                towerModel.GetWeapon().projectile.pierce += 2;
                towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().speed += 70;
            }
            public override string Icon => "EpicRangeUpgradeIcon";
        }
        public class Bottom1_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 1;
            public override string DisplayName => "Better Legend";
            public override string Description => "all stats +10%";
            public override int Cost => 103888;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range = towerModel.GetAttackModel().range *= 1.1f;
                towerModel.GetWeapon().rate *= 0.9f;//1 � 1.1 = 0.90909...
                towerModel.GetWeapon().projectile.pierce *= 1.1f;
                towerModel.GetWeapon().projectile.GetDamageModel().damage *= 1.1f;
            }
        }
        public class Bottom2_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override string DisplayName => "Golden Shuriken";
            public override string Description => "+50% pierce +50% damage";
            public override int Cost => 207777;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Bottom2Display_Legend>();
                towerModel.GetWeapon().projectile.ApplyDisplay<Bottom2Projectile_Legend>();

                towerModel.GetWeapon().projectile.GetDamageModel().damage *= 1.5f;
                towerModel.GetWeapon().projectile.pierce *= 1.5f;
            }
            internal class Bottom2Display_Legend : ModDisplay
            {
                public override string BaseDisplay => "791716873c2d0bb49b9c94e965eee468";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                }
            }
            internal class Bottom2Projectile_Legend : ModDisplay
            {
                public override string BaseDisplay => Generic2dDisplay;
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    Set2DTexture(node, Name);
                }
            }
        }
        public class Bottom3_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 3;
            public override string DisplayName => "Golden Cape";
            public override string Description => "+25% range +25% pierce, +500 moab damage";
            public override int Cost => 311666;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Bottom3Display_Legend>();
                towerModel.GetAbilities()[1].icon = GetSpriteReference(mod, "Ability1_Legend");

                towerModel.range = towerModel.GetAttackModel().range *= 1.25f;
                towerModel.GetWeapon().projectile.pierce *= 1.25f;
                towerModel.GetWeapon().projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel", "Moabs", 1, 500, false, true));
            }
            internal class Bottom3Display_Legend : ModDisplay
            {
                public override string BaseDisplay => "791716873c2d0bb49b9c94e965eee468";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Bottom4_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 4;
            public override string DisplayName => "Golden Legend";
            public override string Description => "all stats doubled (except range and rate), +5000 moab damage";
            public override int Cost => 415550;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Bottom4Display_Legend>();
                towerModel.GetWeapon().RemoveBehavior<EjectEffectModel>(); //removes the black hand blur effect

                towerModel.range = towerModel.GetAttackModel().range *= 1.25f;
                towerModel.GetWeapon().projectile.pierce *= 2;
                towerModel.GetWeapon().projectile.GetDamageModel().damage *= 2;
                towerModel.GetWeapon().projectile.GetBehavior<DamageModifierForTagModel>().damageAddative = 5000;
            }
            internal class Bottom4Display_Legend : ModDisplay
            {
                public override string BaseDisplay => "791716873c2d0bb49b9c94e965eee468";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Bottom5_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 5;
            public override string DisplayName => "Solar Eruption";
            public override string Description => "ability: shoots 24 deadly sun beams in a 360� angle";
            public override int Cost => 415550;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel[] attack = { towerModel.GetBehavior<AttackModel>().Duplicate() };

                attack[0].weapons[0].projectile.display = new() { guidRef = "5737e26c93d5fc149ade2f7df1156c74" };
                attack[0].weapons[0].projectile.radius = 7;
                attack[0].weapons[0].ejectX = 0;
                attack[0].weapons[0].ejectY = 0;
                attack[0].weapons[0].ejectZ = 10;
                attack[0].weapons[0].emission = new RandomArcEmissionModel("RandomArcEmissionModel", 24, 0, 360, 0, 1, null);
                attack[0].weapons[0].projectile.RemoveBehavior<RetargetOnContactModel>();
                attack[0].weapons[0].projectile.GetBehavior<TravelStraitModel>().speed = 100;
                attack[0].weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan = 5;
                attack[0].range = 999;
                attack[0].weapons[0].projectile.pierce = 9999999;
                attack[0].weapons[0].projectile.GetDamageModel().damage = 9999999;
                attack[0].weapons[0].projectile.GetDamageModel().immuneBloonProperties = 0;

                var Ability = Game.instance.model.GetTower(TowerType.BombShooter, 0, 5, 0).GetAbilities()[0].Duplicate();

                Ability.GetBehavior<ActivateAttackModel>().attacks = attack;
                Ability.maxActivationsPerRound = 9999999;
                Ability.cooldown = 30;
                Ability.icon = GetSpriteReference("Bottom5_Legend-Icon");

                towerModel.AddBehavior(Ability);
            }
        }
    }
}