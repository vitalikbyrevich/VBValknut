namespace VBValknut.Utillites
{
	public static class ShaderFix
	{
		private static readonly List<GameObject> GOToSwap;

		static ShaderFix()
		{
			GOToSwap = new List<GameObject>();
			new Harmony("ShaderFix").Patch(AccessTools.DeclaredMethod(typeof(FejdStartup), "Awake"), null, new HarmonyMethod(AccessTools.DeclaredMethod(typeof(ShaderFix), "ReplaceShaderPatch")));
			GOToSwap.Clear();
		}

		public static void Replace(GameObject gameObject) => GOToSwap.Add(gameObject);
		
		public static void ReplacePack(params GameObject[] gameObjects)
		{
			if (gameObjects == null) return;
			foreach (var go in gameObjects) if (go) GOToSwap.Add(go);
		}
		
		[HarmonyPriority(700)]
		private static void ReplaceShaderPatch()
		{
			foreach (Material item in from gameObject in GOToSwap
			         select gameObject.GetComponentsInChildren<Renderer>(includeInactive: true) into renderers
			         from renderer in renderers
			         where renderer
			         from material in renderer.sharedMaterials
			         where material
			         select material)
			{
				Shader[] array = Resources.FindObjectsOfTypeAll<Shader>();
				foreach (Shader shader in array) if (!(item.shader.name == "Standard") && item.shader.name == shader.name) item.shader = shader;
			}
		}
	}
}