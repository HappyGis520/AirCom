using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aircom.EDS.Loader;
using Aircom.EDS.Loader.Data;
using Aircom.EDS.Loader.ErrorHandler;
using System.IO;

namespace EDSLoaderClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContainer ldrData = new DataContainer();                        
            LoaderCommandManager ldr = new LoaderCommandManager(true, ldrData);

            StringBuilder LoaderReturn = new StringBuilder();
            TextWriter twr = Console.Out;
            
            ErrorContainer.ErrorState errorresult = ldr.ProcessCommand(LoaderCommandManager.LoaderCommand.Read,
                new string[]
                {
                    "-read",
                    "-type=umtscelltype",
                    //"-filter=\"iid st NJ01\"",
                    "-maxrecords=10"
                },
                ref twr);
                
            
        }
    }
}
