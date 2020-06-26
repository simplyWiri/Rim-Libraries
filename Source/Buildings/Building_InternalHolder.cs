using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Libraries.Buildings
{
    public class Building_InternalStorage : Building, IThingHolder, IStoreSettingsParent
    {
        protected ThingOwner innerContainer;
        private StorageSettings storageSettings;
        private CompStorage compStorageGraphic = null;

        public CompStorage CompStorageGraphic => compStorageGraphic ??= this.TryGetComp<CompStorage>();
        public override Graphic Graphic => CompStorageGraphic.CurrentGraphic ?? DefaultGraphic;

        public bool StorageTabVisible => true;

        public Building_InternalStorage()
        {
            innerContainer = new ThingOwner<Thing>(this, false, LookMode.Deep);
        }

        public bool TryAccept(Thing thing)
        {
            return true;
        }

        public bool Accepts(Thing thing)
        {
            if (!storageSettings.AllowedToAccept(thing))
            {
                return false;
            }
            if (innerContainer.Count + 1 > CompStorageGraphic.Props.fullthreshold)
            {
                return false;
            }
            return true;
        }

        public override void PostMake()
        {
            base.PostMake();
            storageSettings = new StorageSettings(this);
            if (def.building.defaultStorageSettings != null)
            {
                storageSettings.CopyFrom(def.building.defaultStorageSettings);
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref innerContainer, "innerContainer", this);
            Scribe_Deep.Look(ref storageSettings, "storageSettings", this);
        }

        public bool TryDrop(Thing item, bool forbid = true)
        {
            if (this.innerContainer.Contains(item))
            {
                Thing outThing;
                this.innerContainer.TryDrop(item, ThingPlaceMode.Near, out outThing);
                if (forbid) outThing.SetForbidden(true);
                return true;
            }
            return false;
        }

        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return innerContainer;
        }

        public StorageSettings GetParentStoreSettings()
        {
            return def.building.fixedStorageSettings;
        }

        public StorageSettings GetStoreSettings()
        {
            return storageSettings;
        }
    }
}