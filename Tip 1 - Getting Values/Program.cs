using OSIsoft.AF;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tip_1___Getting_Values
{
    class Program
    {
        static void Main(string[] args)
        {
            var AFServerName = "SKYPI05";
            var AFDatabaseName = "MBMC";
            var user = @"skypi\lbowling";
            var password = @"OFNxBKOZmG1w";

            NetworkCredential credential = new NetworkCredential(user, password);
            var piSystem = (new PISystems())[AFServerName];
            piSystem.Connect(credential);
            var afdb = piSystem.Databases[AFDatabaseName];


            foreach (var item in afdb.Elements)
            {
                Console.WriteLine(item.Name);
            }

            var selectedElementName = "Element1";
            var selectedElement = afdb.Elements[selectedElementName];

            Console.WriteLine("Attributes for " + selectedElementName);

            foreach (var attribute in selectedElement.Attributes)
            {
                Console.WriteLine(attribute.Name);
            }

            var selectedAttributeName = "Attribute1";
            var selectedAttribute = selectedElement.Attributes[selectedAttributeName];


            var start = DateTime.Today.AddDays(-1);
            var end = start.AddDays(1);
            var timeRange = new AFTimeRange(start, end);
            var span = AFTimeSpan.Parse("1h");


            //var afValues = selectedAttribute.Data.PlotValues(timeRange, 24, null);
            //var afValues = selectedAttribute.Data.InterpolatedValues(timeRange, span, null, null, false);
            var afValues = selectedAttribute.Data.RecordedValues(timeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, null, null, false);


            foreach (var afValue in afValues)
            {
                Console.WriteLine("Time: {0} \t Value: {1} \t", afValue.Timestamp, afValue.Value.ToString());
            }

            Console.WriteLine("Count: " + afValues.Count);





            Console.ReadKey();
        }
    }
}
