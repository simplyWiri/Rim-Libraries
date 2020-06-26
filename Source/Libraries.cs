using Verse;

namespace Libraries
{
    public class Libraries : Mod
    {
        private HarmonyLib.Harmony harmony;

        public HarmonyLib.Harmony Instance
        {
            get
            {
                if (harmony == null)
                    harmony = new HarmonyLib.Harmony("wiri.libraries");
                return harmony;
            }
        }

        public Libraries(ModContentPack modContent) : base(modContent)
        {
            Instance.PatchAll();
        }
    }
}