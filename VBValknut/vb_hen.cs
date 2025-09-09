using VBValknut.Utillites;

namespace VBValknut
{
    [HarmonyPatch]
    public static class vb_hen
    {
        public static void Init()
        {
            var Val_C_Hen = VBValknut.asset.LoadAsset<GameObject>("Val_C_Hen");
            CreatureManager.Instance.AddCreature(new CustomCreature(Val_C_Hen, fixReference: true, new CreatureConfig
            {
                SpawnConfigs = new SpawnConfig[]
                {
                  new SpawnConfig
                  {
                      WorldSpawnEnabled = true,
                      Biome = Heightmap.Biome.Plains, BiomeArea = Heightmap.BiomeArea.Everything,
                      MaxSpawned = 1, SpawnInterval = 1500, SpawnChance = 1,
                    //  SpawnDistance = 0, MinSpawnRadius = 0, MaxSpawnRadius = 0,
                      MinLevel = 0, MaxLevel = 3,
                   //   RequiredGlobalKey = null, RequiredEnvironments = null,
                   //   MinGroupSize = 0, MaxGroupSize = 0, GroupRadius = 0,
                      SpawnAtDay = true, SpawnAtNight = true,
                      MinAltitude = 1, MaxAltitude = 5, MinTilt = 0, MaxTilt = 45,
                   //   MinOceanDepth = 0, MaxOceanDepth = 0,
                      SpawnInForest = true, SpawnOutsideForest = true,
                  //    HuntPlayer = false,
                      GroundOffset = 0.5f
                  }
                },
                DropConfigs = new DropConfig[]
                {
                    new DropConfig { Item = "ChickenMeat", MinAmount = 1, MaxAmount = 2, Chance = 100, OnePerPlayer = false, LevelMultiplier = true },
                    new DropConfig { Item = "Feathers", MinAmount = 2, MaxAmount = 5, Chance = 100, OnePerPlayer = false, LevelMultiplier = true }
                }
            }));
            ShaderFix.ReplacePack(Val_C_Hen);
            
            RegisterPrefab.VB_AddItem("val_hen_attack");
         //   RegisterPrefab.VB_AddPrefab("val_ChickenEgg_projectile");
        }
        
        [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        [HarmonyPostfix]
        public static void Patch()
        {
            Val_C_Hen();
            Val_C_Hen_attack();
        }

        private static void Val_C_Hen()
        {
            ComponentPatch.Replace<Humanoid>("Val_C_Hen", "Hen", (c, v) =>
            {
                c.m_hitEffects.m_effectPrefabs = v.m_hitEffects.m_effectPrefabs;
                c.m_critHitEffects.m_effectPrefabs = v.m_critHitEffects.m_effectPrefabs;
                c.m_backstabHitEffects.m_effectPrefabs = v.m_backstabHitEffects.m_effectPrefabs;
                c.m_deathEffects.m_effectPrefabs = v.m_deathEffects.m_effectPrefabs;
                c.m_waterEffects.m_effectPrefabs = v.m_waterEffects.m_effectPrefabs;
                c.m_consumeItemEffects.m_effectPrefabs = v.m_consumeItemEffects.m_effectPrefabs;
            });
            ComponentPatch.Replace<MonsterAI>("Val_C_Hen", "Hen", (c, v) =>
            {
                c.m_idleSound.m_effectPrefabs = v.m_idleSound.m_effectPrefabs;
                c.m_consumeItems = v.m_consumeItems;
            });
            ComponentPatch.AddEffect<MonsterAI>("Val_C_Hen", "sfx_chicken_hurt", h => h.m_alertedEffects);
            ComponentPatch.Replace<FootStep>("Val_C_Hen", "Hen", (c, v) => { c.m_effects = v.m_effects; });
            ComponentPatch.Replace<Procreation>("Val_C_Hen", "Hen", (c, v) =>
            {
                c.m_birthEffects.m_effectPrefabs = v.m_birthEffects.m_effectPrefabs;
                c.m_loveEffects.m_effectPrefabs = v.m_loveEffects.m_effectPrefabs;
            });
            ComponentPatch.Replace<Tameable>("Val_C_Hen", "Hen", (c, v) =>
            {
                c.m_tamedEffect.m_effectPrefabs = v.m_tamedEffect.m_effectPrefabs;
                c.m_sootheEffect.m_effectPrefabs = v.m_sootheEffect.m_effectPrefabs;
                c.m_petEffect.m_effectPrefabs = v.m_petEffect.m_effectPrefabs;
            });
            
            ZNetScene.instance.GetPrefab("Val_C_Hen").GetComponent<Procreation>().m_offspring = ZNetScene.instance.GetPrefab("Hen").GetComponent<Procreation>().m_offspring;
        }

        private static void Val_C_Hen_attack()
        {
         //   ZNetScene.instance.GetPrefab("val_ChickenEgg_projectile").GetComponent<Projectile>().m_spawnOnHit = ZNetScene.instance.GetPrefab("ChickenEgg");
            ComponentPatch.AddEffect<ItemDrop>("val_hen_attack", "sfx_chicken_hurt", h => h.m_itemData.m_shared.m_triggerEffect);
          //  ComponentPatch.AddEffect<ItemDrop>("val_hen_attack1", "sfx_chicken_hurt", h => h.m_itemData.m_shared.m_triggerEffect);
        }
    }
}