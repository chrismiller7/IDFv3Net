using System.Linq;
using IDFv3Net.Attributes;
using IDFv3Net.Sections;
using System.Reflection;

namespace IDFv3Net.Extensions
{
    public static class ConvertUnitsToExtensions
    {
        const float OneMilInMillimeters = 0.0254f;

        public static void ConvertUnitsTo(this IDFFile idf, Units toUnits)
        {
            var header = idf.GetHeader();
            if (header is HeaderSection)
            {
                var fromUnits = ((HeaderSection)header).Units;
                foreach (var section in idf.GetAllSections())
                {
                    ConvertSectionUnits(section, fromUnits, toUnits);
                }
                ((HeaderSection)header).Units = toUnits;
            }
            else
            {
                foreach (var section in idf.GetAllSections().OfType<ElectricalComponentSection>())
                {
                    ConvertSectionUnits(section, section.Units, toUnits);
                    section.Units = toUnits;
                }
                foreach (var section in idf.GetAllSections().OfType<MechanicalComponentSection>())
                {
                    ConvertSectionUnits(section, section.Units, toUnits);
                    section.Units = toUnits;
                }
            }
        }

        static void ConvertSectionUnits(AbstractSection section, Units fromUnits, Units toUnits)
        {
            if (fromUnits != toUnits)
            {
                foreach (var field in section.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    var attrib = field.GetCustomAttributes(true).OfType<LengthUnitAttribute>().SingleOrDefault();
                    if (attrib != null && field.FieldType == typeof(float))
                    {
                        ConvertFieldUnits(section, field, fromUnits, toUnits);
                    }
                }
            }
        }

        static void ConvertFieldUnits(AbstractSection section, FieldInfo field, Units fromUnits, Units toUnits)
        {
            if (fromUnits == Units.MM && toUnits == Units.THOU)
            {
                var val = (float)field.GetValue(section);
                var newVal = val / OneMilInMillimeters;
                field.SetValue(section, newVal);
            }
            else if (fromUnits == Units.THOU && toUnits == Units.MM)
            {
                var val = (float)field.GetValue(section);
                var newVal = val * OneMilInMillimeters;
                field.SetValue(section, newVal);
            }
        }
    }
}
