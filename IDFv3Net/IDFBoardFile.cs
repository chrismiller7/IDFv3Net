using System;
using System.Collections.Generic;
using System.Linq;
using IDFv3Net.Sections;

namespace IDFv3Net
{
    public class IDFBoardFile : IDFBoardPanelCommonFile
    {
        public BoardOutlineSection BoardOutline { get; private set; }

        public IDFBoardFile() : base(FileType.BOARD_FILE)
        {
            BoardOutline = new Sections.BoardOutlineSection();
        }

        public IDFBoardFile(string file) : base(file)
        {
            if (Header.FileType != FileType.BOARD_FILE) throw new Exception("FileType is not a Board file.");
            BoardOutline = this.sections.OfType<BoardOutlineSection>().SingleOrDefault();
            if (BoardOutline == null) throw new Exception("BoardOutline section not found in file");
        }

        public override AbstractSection[] GetAllSections()
        {
            List<AbstractSection> list = new List<AbstractSection>(this.GetCommonSections());
            list.Insert(1, BoardOutline);
            return list.ToArray();
        }
    }
}