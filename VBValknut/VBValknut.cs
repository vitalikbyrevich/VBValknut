using LocalizationManager;

namespace VBValknut
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class VBValknut : BaseUnityPlugin
    {
        private const string ModName = "VBValknut";
        private const string ModVersion = "0.0.2";
        private const string ModGUID = "VitByr.VBValknut";
        internal static VBValknut _self;
        public static AssetBundle asset;

        public void Awake()
        {
            _self = this;
            Localizer.Load();
            asset = AssetUtils.LoadAssetBundleFromResources("valknut", typeof(VBValknut).Assembly);
//bvbn
            vb_hen.Init();
            
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "VitByr.VBValknut");
        }

        public void OnDestroy()
        {
            base.Config.Save();
        }

        public void Debug(string msg)
        {
            Logger.LogInfo(msg);
        }

        public void DebugError(string msg)
        {
            Logger.LogError(msg);
        }

    }
}