using System;
using System.Collections.Generic;
using IDFv3Net.Attributes;
using IDFv3Net.Sections;
using System.Linq;

namespace IDFv3Net
{
    public abstract class IDFBoardLibraryCommonFile : IDFFile
    {
        public HeaderSection Header { get; private set; }
        public List<OtherOutlineSection> OtherOutlines { get; private set; }
        public List<RoutingOutlineSection> RoutingOutlines { get; private set; }
        public List<PlacementOutlineSection> PlacementOutlines { get; private set; }
        public List<RoutingKeepoutSection> RoutingKeepouts { get; private set; }
        public List<ViaKeepoutSection> ViaKeepouts { get; private set; }
        public List<PlacementKeepoutSection> PlacementKeepouts { get; private set; }
        public List<PlacementGroupAreaSection> PlacementGroupAreas { get; private set; }

        public List<DrilledHole> DrilledHoles { get; private set; }
        public List<Note> Notes { get; private set; }
        public List<ComponentPlacement> ComponentPlacements { get; private set; }

        public IDFBoardLibraryCommonFile(FileType fileType) : base()
        {
            Header = new HeaderSection();
            Header.FileType = fileType;

            OtherOutlines = new List<OtherOutlineSection>();
            RoutingOutlines = new List<RoutingOutlineSection>();
            PlacementOutlines = new List<PlacementOutlineSection>();
            RoutingKeepouts = new List<RoutingKeepoutSection>();
            ViaKeepouts = new List<ViaKeepoutSection>();
            PlacementKeepouts = new List<PlacementKeepoutSection>();
            PlacementGroupAreas = new List<PlacementGroupAreaSection>();
            DrilledHoles = new List<Sections.DrilledHole>();
            Notes = new List<Sections.Note>();
            ComponentPlacements = new List<Sections.ComponentPlacement>();
        }

        public IDFBoardLibraryCommonFile(string file) : base(file, SectionFileType.BoardPanel)
        {
            Header = this.sections.OfType<HeaderSection>().SingleOrDefault();
            if (Header == null) throw new Exception("Header section not found"); 
            OtherOutlines = this.sections.OfType<OtherOutlineSection>().ToList();
            RoutingOutlines = this.sections.OfType<RoutingOutlineSection>().ToList();
            PlacementOutlines = this.sections.OfType<PlacementOutlineSection>().ToList();
            RoutingKeepouts = this.sections.OfType<RoutingKeepoutSection>().ToList();
            ViaKeepouts = this.sections.OfType<ViaKeepoutSection>().ToList();
            PlacementKeepouts = this.sections.OfType<PlacementKeepoutSection>().ToList();
            PlacementGroupAreas = this.sections.OfType<PlacementGroupAreaSection>().ToList();

            var drilledHolesSection = this.sections.OfType<DrilledHolesSection>().SingleOrDefault();
            if (drilledHolesSection != null)
            {
                DrilledHoles = new List<DrilledHole>(drilledHolesSection.DrilledHoles);
            }

            var notesSection = this.sections.OfType<NotesSection>().SingleOrDefault();
            if (notesSection != null)
            {
                Notes = new List<Note>(notesSection.Notes);
            }

            var placementSection = this.sections.OfType<ComponentPlacementSection>().SingleOrDefault();
            if (placementSection != null)
            {
                ComponentPlacements = new List<ComponentPlacement>(placementSection.Placements);
            }
        }

        protected AbstractSection[] GetCommonSections()
        {
            List<AbstractSection> list = new List<Sections.AbstractSection>();
            list.Add(Header);
            list.AddRange(OtherOutlines);
            list.AddRange(RoutingOutlines);
            list.AddRange(PlacementOutlines);
            list.AddRange(RoutingKeepouts);
            list.AddRange(ViaKeepouts);
            list.AddRange(PlacementKeepouts);
            list.AddRange(PlacementGroupAreas);
            list.Add(new DrilledHolesSection() { DrilledHoles = this.DrilledHoles.ToArray() });
            list.Add(new NotesSection() { Notes = this.Notes.ToArray() });
            list.Add(new ComponentPlacementSection() {  Placements = this.ComponentPlacements.ToArray() });
            return list.ToArray();
        }
    }
}