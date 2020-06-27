using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Libraries.Harmony
{
    //[DefOf]
    //public class RL_TimeAssignmentDefOf
    //{
    //    public static TimeAssignmentDef RL_Studying;

    //    static RL_TimeAssignmentDefOf()
    //    {
    //        DefOfHelper.EnsureInitializedInCtor(typeof(RL_TimeAssignmentDefOf));
    //    }
    //}

    //[HarmonyPatch(typeof(TimeAssignmentSelector), nameof(TimeAssignmentSelector.DrawTimeAssignmentSelectorGrid))]
    //public static class H_TimeAssignmentSelector
    //{
    //    public static void Postfix(Rect rect)
    //    {
    //        rect.xMax = rect.center.x;
    //        rect.yMax = rect.center.y;

    //        rect.x += rect.width * 2;
    //        if (ModsConfig.RoyaltyActive)
    //        {
    //            rect.x += rect.width;
    //            DrawTimeAssignmentSelectorFor(rect, RL_TimeAssignmentDefOf.RL_Studying);
    //        }
    //        else
    //        {
    //            rect.x += rect.width;
    //            rect.y -= rect.height;
    //            DrawTimeAssignmentSelectorFor(rect, RL_TimeAssignmentDefOf.RL_Studying);
    //        }
    //    }

    //    private static void DrawTimeAssignmentSelectorFor(Rect rect, TimeAssignmentDef ta)
    //    {
    //        rect = GenUI.ContractedBy(rect, 2f);
    //        GUI.DrawTexture(rect, ta.ColorTexture);
    //        if (Widgets.ButtonInvisible(rect, true))
    //        {
    //            TimeAssignmentSelector.selectedAssignment = ta;
    //            SoundStarter.PlayOneShotOnCamera(SoundDefOf.Tick_High, null);
    //        }
    //        GUI.color = Color.white;
    //        if (Mouse.IsOver(rect))
    //        {
    //            Widgets.DrawHighlight(rect);
    //        }
    //        Text.Font = GameFont.Small;
    //        Text.Anchor = TextAnchor.MiddleCenter;
    //        GUI.color = Color.white;
    //        Widgets.Label(rect, ta.LabelCap);
    //        Text.Anchor = TextAnchor.UpperLeft;
    //        if (TimeAssignmentSelector.selectedAssignment == ta)
    //        {
    //            Widgets.DrawBox(rect, 2);
    //        }
    //    }
    //}

}
