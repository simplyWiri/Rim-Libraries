using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Libraries
{
    public class Settings : ModSettings
    {
        // Bookshelves
        public bool DustyBookshelves = true;
        public float DustIncrementMultiplier = 1.0f;
        public float DustyBookshelfThreshold = 10f;

        // Skill books
        public bool SkillBookFancyNames = true;
        public float SkillBookSkillGainMultiplier = 1.0f;
        public override void ExposeData()
        {
            // bookshelves
            Scribe_Values.Look(ref DustyBookshelves, "DustyBookshelves", true);
            Scribe_Values.Look(ref DustIncrementMultiplier, "DistIncrementMultiplier", 1.0f);
            Scribe_Values.Look(ref DustyBookshelfThreshold, "DustyBookshelfThreshold", 10f);

            // skill books
            Scribe_Values.Look(ref SkillBookFancyNames, "SkillBookFancyNames", true);
            Scribe_Values.Look(ref SkillBookSkillGainMultiplier, "SkillBookSkillGainMultiplier", 1.0f);

            base.ExposeData();
        }

        public void ShowWindowContents(Rect rect)
        {
            var leftRect = rect.LeftPart(.48f);
            BookcaseSettings(ref leftRect);
            SkillBookSettings(ref leftRect);
            Widgets.DrawLineVertical(rect.x + rect.width / 2.0f, rect.y, rect.height);
        }


        public void BookcaseSettings(ref Rect rect)
        {
            var listing = new Listing_Standard();
            listing.Begin(rect);

            Heading(listing, "BookShelves");
            Checkbox(listing, "Dusty BookShelves", ref DustyBookshelves);
            LabeledSliderFloat(listing, "Dust Increment Multiplier", ref DustIncrementMultiplier, 0, 3, true);
            LabeledSliderFloat(listing, "Dust Threshold", ref DustyBookshelfThreshold, 0, 50);

            listing.GapLine(0f);
            listing.End();
            rect.y += listing.curY + 10;
        }

        public void SkillBookSettings(ref Rect rect)
        {
            var listing = new Listing_Standard();
            listing.Begin(rect);

            Heading(listing, "Skill Books");
            Checkbox(listing, "Fancy names for skill books", ref SkillBookFancyNames);
            LabeledSliderFloat(listing, "Skill book gain multiplier", ref SkillBookSkillGainMultiplier, 0, 3, true);

            listing.GapLine(0f);
            listing.End();
            rect.y += listing.curY;
        }

        // Utility
        private void Checkbox(Listing_Standard listing, string label, ref bool toggle)
        {
            var rect = listing.GetRect(Mathf.CeilToInt(label.GetWidthCached() / listing.ColumnWidth) * Text.LineHeight);

            if (Widgets.ButtonInvisible(rect))
            {
                toggle = !toggle;
                if (toggle) SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera();
                else SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera();
            }

            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.DrawTextureFitted(rect.LeftPartPixels(30f), toggle ? Widgets.CheckboxOnTex : Widgets.CheckboxOffTex, 0.5f);
            rect.x += 30;
            Widgets.Label(rect, label);
            Text.Anchor = anchor;
        }
        private void LabeledSliderFloat(Listing_Standard listing, string label, ref float value, float min, float max, bool percent = false)
        {

            var anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleCenter;
            Text.Font = GameFont.Tiny;

            var rect = listing.GetRect(Text.LineHeight);
            if(percent)
                Widgets.Label(rect, $"{label}: {value * 100}%");
            else
                Widgets.Label(rect, $"{label}: {value:0.00}");
            rect = listing.GetRect(Text.LineHeight);

            var minWidth = min.ToString().GetWidthCached();
            var maxWidth = max.ToString().GetWidthCached();

            Widgets.Label(rect.LeftPartPixels(minWidth), min.ToString());
            Widgets.Label(rect.RightPartPixels(maxWidth), max.ToString());

            rect.x += minWidth + 5;
            rect.width -= minWidth + 5;

            value = Widgets.HorizontalSlider(rect.LeftPartPixels(rect.width - (maxWidth + 5)), value, min, max, roundTo: 0.05f);

            Text.Font = GameFont.Small;
            Text.Anchor = anchor;

            listing.Gap(listing.verticalSpacing);
        }

        public static void Heading(Listing_Standard listing, string label)
        {
            var rect = listing.GetRect(Text.CalcHeight(label, listing.ColumnWidth));

            Text.Font = GameFont.Medium;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, label);
            ResetFont();
        }

        public static void ResetFont()
        {
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.UpperLeft;
        }

    }
}
