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
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using System.Collections.Generic;
using UnityEngine;
using Il2Cpp;

[assembly: MelonInfo(typeof(MiniTowersCollection.MiniTowersCollection), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MiniTowersCollection;

public class MiniTowersCollection : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<MiniTowersCollection>("2 Mini Towers loaded!");
    }

    public class MiniTower : ModTowerSet
    {
        public override string DisplayName => "Mini Towers Collection";
        public override bool AllowInRestrictedModes => true;
        public override int GetTowerSetIndex(List<TowerSet> towerSets) => towerSets.IndexOf(TowerSet.Support) + 1;
    }

    public class UpgradeableLegendTower
    {
        public class UpgradeableLegend : ModTower<MiniTower>
        {
            public override string BaseTower => "SuperMonkey-005";
            public override string DisplayName => "Upgradable Legend";
            public override string Description => "The Legend Of The Night as a upgradeable tower.";
            public override int Cost => 200000;
            public override int TopPathUpgrades => 2;
            public override int MiddlePathUpgrades => 2;
            public override int BottomPathUpgrades => 5;
            public override bool IsValidCrosspath(int[] tiers) => ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                towerModel.ignoreBlockers = true;
                towerModel.GetWeapon().projectile.ignoreBlockers = true;
                towerModel.GetWeapon().projectile.canCollisionBeBlockedByMapLos = false;
                towerModel.GetAttackModel().attackThroughWalls = true;
            }
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
            public override string Description => "Legend of the Night but stronger.";
            public override int Cost => 103888;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range = towerModel.GetAttackModel().range *= 1.1f;
                towerModel.GetWeapon().rate *= 0.9f;//1 ÷ 1.1 = 0.90909...
                towerModel.GetWeapon().projectile.pierce *= 1.1f;
                towerModel.GetWeapon().projectile.GetDamageModel().damage *= 1.1f;
            }
        }
        public class Bottom2_Legend : ModUpgrade<UpgradeableLegend>
        {
            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override string DisplayName => "Golden Shuriken";
            public override string Description => "High pierce golden shurikens deal way more damage.";
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
            public override string Description => "The golden cape makes the legend a master of MOAB destruction.";
            public override int Cost => 311666;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Bottom3Display_Legend>();
                towerModel.GetAbilities()[1].icon = GetSpriteReference(mod, "Ability1_Legend");

                towerModel.range = towerModel.GetAttackModel().range *= 1.25f;
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
            public override string Description => "Every bloon fears the Golden Gegend...";
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
            public override string Description => "Ability: Shoots 24 deadly sun beams in a 360° angle!";
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
                Ability.cooldown = 45;
                Ability.icon = GetSpriteReference("Bottom5_Legend-Icon");

                towerModel.AddBehavior(Ability);
            }
        }
    }
    public class MrStickmanTower
    {
        public class MrStickman : ModTower<MiniTower>
        {
            public override string BaseTower => "WizardMonkey";
            public override string DisplayName => "Mr Stickman's Tower";
            public override string Description => "Mr Stickman's personal tower.";
            public override int Cost => 375;
            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 0;
            public override void ModifyBaseTowerModel(TowerModel towerModel) { }
            public override string Icon => VanillaSprites.Wizard000;
            public override string Portrait => VanillaSprites.Wizard000;
        }
        public class Middle1_MrStickman : ModUpgrade<MrStickman>
        {
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override string DisplayName => "Ninja Discipline";
            public override string Description => "Increases attack range and attack speed.";
            public override int Cost => 300;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle1Display_MrStickman>();

                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel", true));
                towerModel.towerSelectionMenuThemeId = "Camo";
                towerModel.range = towerModel.GetAttackModel().range += 7;
                towerModel.GetWeapon().rate *= 0.62f;
                towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Lead;
            }
            public override string Icon => "NinjaDisciplineUpgradeIcon";
            internal class Middle1Display_MrStickman : ModDisplay
            {
                public override string BaseDisplay => "d8a45c17dcf700a499c031dff73684a1";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Middle2_MrStickman : ModUpgrade<MrStickman>
        {
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override string DisplayName => "Pin";
            public override string Description => "Pins Bloons in place for a short time when struck.";
            public override int Cost => 220;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle2Display_MrStickman>();

                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("EngineerMonkey-002").GetWeapon().projectile.GetBehavior<SlowOnPopModel>().Duplicate());
                towerModel.GetWeapon().projectile.collisionPasses = new int[] { 0, -1 };
            }
            public override string Icon => "PinUpgradeIcon";
            internal class Middle2Display_MrStickman : ModDisplay
            {
                public override string BaseDisplay => "d8a45c17dcf700a499c031dff73684a1";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Middle3_MrStickman : ModUpgrade<MrStickman>
        {
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override string DisplayName => "Glaive Ricochet";
            public override string Description => "Glaives will bounce from Bloon to Bloon automatically and aggressively.";
            public override int Cost => 1200;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle3Display_MrStickman>();

                towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan = 2;
                towerModel.GetWeapon().projectile.pierce += 99; //extra bounce monkey knowledge
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("BoomerangMonkey-300").GetWeapon().projectile.GetBehavior<RetargetOnContactModel>().Duplicate());
                towerModel.GetWeapon().projectile.ignoreBlockers = true;
                towerModel.GetWeapon().projectile.canCollisionBeBlockedByMapLos = false;
            }
            public override string Icon => "GlaiveRicochetUpgradeIcon";
            internal class Middle3Display_MrStickman : ModDisplay
            {
                public override string BaseDisplay => "d8a45c17dcf700a499c031dff73684a1";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Middle4_MrStickman : ModUpgrade<MrStickman>
        {
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override string DisplayName => "Maim MOAB";
            public override string Description => "Deals much more damage and immobilizes MOAB class bloons for a short time.";
            public override int Cost => 5400;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle4Display_MrStickman>();

                towerModel.GetWeapon().projectile.GetDamageModel().damage += 10;
                towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("SniperMonkey-400").GetWeapon().projectile.GetBehavior<SlowMaimMoabModel>().Duplicate());
            }
            public override string Icon => "MaimMoabUpgradeIcon";
            internal class Middle4Display_MrStickman : ModDisplay
            {
                public override string BaseDisplay => "d8a45c17dcf700a499c031dff73684a1";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                }
            }
        }
        public class Middle5_MrStickman : ModUpgrade<MrStickman>
        {
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override string DisplayName => "Grandmaster Ninja";
            public override string Description => "Throws incredibly fast, 8 shurikens per shot!";
            public override int Cost => 35000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle5Display_MrStickman>();

                towerModel.range = towerModel.GetAttackModel().range += 10;
                towerModel.GetWeapon().rate *= 0.5f;
                towerModel.GetWeapon().projectile.GetDamageModel().damage += 1;
                towerModel.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel", 9, 0, 72, null, false); //deadly tranquility monkey knowledge
            }
            public override string Icon => "GrandmasterNinjaUpgradeIcon";
            internal class Middle5Display_MrStickman : ModDisplay
            {
                public override string BaseDisplay => "5997a2a57b894734286d05f054d6f91b";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
    }
    public class Tobyman009Tower
    {
        public class Tobyman009 : ModTower<MiniTower>
        {
            public override string BaseTower => "SniperMonkey";
            public override string DisplayName => "Tobyman009's Tower";
            public override string Description => "Tobyman009's personal tower.";
            public override int Cost => 350;
            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 0;
            public override void ModifyBaseTowerModel(TowerModel towerModel) { }
            public override string Icon => VanillaSprites.SniperMonkey000;
            public override string Portrait => VanillaSprites.SniperMonkey000;
        }
        public class Middle1_Tobyman009 : ModUpgrade<Tobyman009>
        {
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override string DisplayName => "Twin Guns";
            public override string Description => "Added twin gun doubles attack speed. Enhances Ballistic Missile and Bloontonium Reactor.";
            public override int Cost => 450;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle1Display_Tobyman009>();

                towerModel.GetWeapon().rate *= 0.5f;
            }
            public override string Icon => "TwinGunsUpgradeIcon";
            internal class Middle1Display_Tobyman009 : ModDisplay
            {
                public override string BaseDisplay => "26a654b46fa1fa6498a4a6e40c93a406";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
        public class Middle2_Tobyman009 : ModUpgrade<Tobyman009>
        {
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override string DisplayName => "Ultravision";
            public override string Description => "Enables Super Monkey to shoot slightly further, to see and do more damage to Camo Bloons.";
            public override int Cost => 1200;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle2Display_Tobyman009>();

                towerModel.AddBehavior(new OverrideCamoDetectionModel("CamoDetect", true));
                towerModel.towerSelectionMenuThemeId = "Camo";
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("SuperMonkey-002").GetWeapon().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());

                towerModel.ignoreBlockers = true; //x-ray ultra monkey knowledge
                towerModel.GetWeapon().projectile.ignoreBlockers = true;
                towerModel.GetWeapon().projectile.canCollisionBeBlockedByMapLos = false;
                towerModel.GetAttackModel().attackThroughWalls = true;
            }
            public override string Icon => "UltraVisionUpgradeIcon";
            internal class Middle2Display_Tobyman009 : ModDisplay
            {
                public override string BaseDisplay => "26a654b46fa1fa6498a4a6e40c93a406";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
        public class Middle3_Tobyman009 : ModUpgrade<Tobyman009>
        {
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override string DisplayName => "Glaive Ricochet";
            public override string Description => "Glaives will bounce from Bloon to Bloon automatically and aggressively.";
            public override int Cost => 1200;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle3Display_Tobyman009>();

                towerModel.GetWeapon().projectile.GetBehavior<AgeModel>().lifespan = 2;
                towerModel.GetWeapon().projectile.pierce = 100; //extra bounce monkey knowledge
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("BoomerangMonkey-300").GetWeapon().projectile.GetBehavior<RetargetOnContactModel>().Duplicate());
            }
            public override string Icon => "GlaiveRicochetUpgradeIcon";
            internal class Middle3Display_Tobyman009 : ModDisplay
            {
                public override string BaseDisplay => "26a654b46fa1fa6498a4a6e40c93a406";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
        public class Middle4_Tobyman009 : ModUpgrade<Tobyman009>
        {
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override string DisplayName => "Full Auto Rifle";
            public override string Description => "Fully automatic weapon with incredible popping power.";
            public override int Cost => 4250;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle4Display_Tobyman009>();

                towerModel.GetWeapon().rate *= 0.25f; //had to make it a bit better so the tower doesn't suck
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("SniperMonkey-004").GetWeapon().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());
            }
            public override string Icon => "FullAutoRifleUpgradeIcon";
            internal class Middle4Display_Tobyman009 : ModDisplay
            {
                public override string BaseDisplay => "26a654b46fa1fa6498a4a6e40c93a406";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
        public class Middle5_Tobyman009 : ModUpgrade<Tobyman009>
        {
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override string DisplayName => "Avatar of Wrath";
            public override string Description => "The more bloons there are, the more damage it does!";
            public override int Cost => 45000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle5Display_Tobyman009>();

                towerModel.GetWeapon().rate *= 0.5f;
                towerModel.GetWeapon().projectile.GetDamageModel().damage += 3;
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("Druid-005").GetWeapon().projectile.GetBehavior<DamageModifierWrathModel>().Duplicate());
            }
            public override string Icon => "AvatarofWrathUpgradeIcon";
            internal class Middle5Display_Tobyman009 : ModDisplay
            {
                public override string BaseDisplay => "26a654b46fa1fa6498a4a6e40c93a406";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
        }
    }
    public class VascoTower
    {
        public class Vasco : ModTower<MiniTower>
        {
            public override string BaseTower => "DartMonkey";
            public override string DisplayName => "vasco's Tower";
            public override string Description => "vasco's personal tower.";
            public override int Cost => 200;
            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 5;
            public override int BottomPathUpgrades => 0;
            public override void ModifyBaseTowerModel(TowerModel towerModel) { }
            public override string Icon => VanillaSprites.DartMonkey000;
            public override string Portrait => VanillaSprites.DartMonkey000;
        }
        public class Middle1_Vasco : ModUpgrade<Vasco>
        {
            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override string DisplayName => "Better Darts";
            public override string Description => "Doubles the damage and the speed.";
            public override int Cost => 500;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle1Display_Vasco>();

                towerModel.GetWeapon().rate /= 2;
                towerModel.GetWeapon().projectile.GetDamageModel().damage = 2;
            }
            internal class Middle1Display_Vasco : ModDisplay
            {
                public override string BaseDisplay => "303631384c6f454408d3c4fd48c7ecf4";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshTexture(node, Name, 2);
                }
            }
        }
        public class Middle2_Vasco : ModUpgrade<Vasco>
        {
            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override string DisplayName => "Educated Monkey";
            public override string Description => "Gains camo detection and lead popping power.";
            public override int Cost => 1500;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle2Display_Vasco>();

                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel", true));
                towerModel.towerSelectionMenuThemeId = "Camo";

                towerModel.range = towerModel.GetAttackModel().range += 10;

                towerModel.GetWeapon().projectile.GetDamageModel().damage = 4;
                towerModel.GetWeapon().projectile.GetDamageModel().immuneBloonProperties = 0;
            }
            internal class Middle2Display_Vasco : ModDisplay
            {
                public override string BaseDisplay => "971b7733796fca8408f7f68a21e0797c";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshTexture(node, Name, 2);
                }
            }
        }
        public class Middle3_Vasco : ModUpgrade<Vasco>
        {
            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override string DisplayName => "Deadly Precision";
            public override string Description => "20 damage per shot, plus bonus damage to Ceramics.";
            public override int Cost => 6000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle3Display_Vasco>();

                towerModel.GetWeapon().projectile.GetDamageModel().damage = 20;
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("SniperMonkey-300").GetWeapon().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());
            }
            internal class Middle3Display_Vasco : ModDisplay
            {
                public override string BaseDisplay => "971b7733796fca8408f7f68a21e0797c";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshTexture(node, Name, 2);
                }
            }
        }
        public class Middle4_Vasco : ModUpgrade<Vasco>
        {
            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override string DisplayName => "Laser Crossbow";
            public override string Description => "Shoots high pierce laser darts at a high rate.";
            public override int Cost => 18000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.ApplyDisplay<Middle4Display_Vasco>();

                towerModel.range = towerModel.GetAttackModel().range += 15;

                towerModel.GetWeapon().projectile.ApplyDisplay<Middle4Projectile_Vasco>();

                towerModel.GetWeapon().ejectX = -1.5f;
                towerModel.GetWeapon().ejectY = 4.83f;
                towerModel.GetWeapon().ejectZ = 9.0f;

                towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan = 0.75f;

                towerModel.GetWeapon().rate /= 2;
                towerModel.GetWeapon().projectile.pierce = 15;
                towerModel.GetWeapon().projectile.GetDamageModel().damage = 40;
            }
            internal class Middle4Display_Vasco : ModDisplay
            {
                public override string BaseDisplay => "f7a1b5c14ded01146b80bd7121f3fcd7";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshTexture(node, Name, 2);
                    SetMeshTexture(node, Name, 3);
                }
            }
            internal class Middle4Projectile_Vasco : ModDisplay
            {
                public override string BaseDisplay => "6c11e1432d6321c44b216600b2cdbac6";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    Set2DTexture(node, Name);
                }
            }
        }
        public class Middle5_Vasco : ModUpgrade<Vasco>
        {
            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override string DisplayName => "Ultra-Juggernaut";
            public override string Description => "Gigantic spiked ball splits twice into 6 Juggernaut balls for even more destructive power against Ceramic and Fortified Bloons.";
            public override int Cost => 50000;
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.RemoveBehavior<AttackModel>();
                towerModel.AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey-500").GetBehavior<AttackModel>().Duplicate());

                towerModel.ApplyDisplay<Middle5Display_Vasco>();

                towerModel.GetWeapon().projectile.ApplyDisplay<Middle5Projectile_Vasco>();
                towerModel.GetWeapon().projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.ApplyDisplay<Middle5Projectile2_Vasco>();

                towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel", true));
                towerModel.towerSelectionMenuThemeId = "Camo";

                towerModel.range = towerModel.GetAttackModel().range = 75;
                towerModel.GetWeapon().rate = 0.2f;
                towerModel.GetWeapon().projectile.pierce *= 2;
                towerModel.GetWeapon().projectile.GetDamageModel().damage = 100;

                towerModel.GetWeapon().projectile.RemoveBehavior<DamageModifierForTagModel>();
                towerModel.GetWeapon().projectile.AddBehavior(Game.instance.model.GetTowerFromId("SniperMonkey-300").GetWeapon().projectile.GetBehavior<DamageModifierForTagModel>().Duplicate());   
            }
            internal class Middle5Display_Vasco : ModDisplay
            {
                public override string BaseDisplay => "b194c58ed09f1aa468e935b453c6843c";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                    SetMeshTexture(node, Name, 1);
                    SetMeshOutlineColor(node, new Color(0 / 255, 0 / 255, 0 / 255), 1);
                }
            }
            internal class Middle5Projectile_Vasco : ModDisplay
            {
                public override string BaseDisplay => "c4b8e7aa3e07d764fb9c3c773ceec2ab";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                }
            }
            internal class Middle5Projectile2_Vasco : ModDisplay
            {
                public override string BaseDisplay => "ee74983d627954e4e9765d86e05b4500";
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, Name);
                }
            }
        }
    }
}