# IDFv3Net
An easy to use library for working with IDFv3 files.
Board / Panel Files: *.emn
Library files: *.emp

##Features
- Reading and writing IDFv3 formatted files
- Converting units between THOU and MM
- Geometry functions: translation, rotation, scaling, flip horizontal, flip vertical

##Intermediate Data Format (IDF) Specification
[IDFv3](https://www.simplifiedsolutionsinc.com/images/idf_v30_spec.pdf)

## Usage
'''
var idf = new IDFBoardFile(@"IdfBoardFile.emn");
idf.ConvertUnitsTo(IDFv3Net.Sections.Units.THOU);
idf.Scale(0.5f, 0.5f);
idf.Translate(-100, 0);
idf.Rotate(90);
idf.FlipHorizontal();
idf.Notes.Add(new Note() { TextValue = "Making changes!", Point = new Point(100,100) });
idf.SaveAs("NewFile.emn");
'''