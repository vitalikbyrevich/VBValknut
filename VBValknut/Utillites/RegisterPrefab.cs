namespace VBValknut.Utillites
{
	public static class RegisterPrefab
	{
		public static void VB_AddPrefab(params string[] prefabNames)
		{
			foreach (var name in prefabNames)
			{
				var prefab = VBValknut.asset.LoadAsset<GameObject>(name);
				if (prefab)
				{
					GameObject prefabToRegister = prefab;
					PrefabManager.Instance.AddPrefab(prefabToRegister);
					ShaderFix.Replace(prefabToRegister);
				}
			}
		}
		public static void VB_AddItem(params string[] prefabNames)
		{
			foreach (var name in prefabNames)
			{
				var prefab = VBValknut.asset.LoadAsset<GameObject>(name);
				if (prefab)
				{
					GameObject prefabToRegister = prefab;
					ItemManager.Instance.AddItem(new CustomItem(prefabToRegister, fixReference: true) { Recipe = null });
					ShaderFix.Replace(prefabToRegister);
				}
			}
		}
		public static void VB_AddSE(params string[] prefabNames)
		{
			foreach (var name in prefabNames)
			{
				var prefab = VBValknut.asset.LoadAsset<StatusEffect>(name);
				if (prefab)
				{
					StatusEffect prefabToRegister = prefab;
					ItemManager.Instance.AddStatusEffect(new CustomStatusEffect( prefabToRegister, fixReference: true));
				}
			}
		}
	}
}