using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_park_application
{
    internal class Program
    {
        /// <summary>
        /// Create a Car Park application to handle to reserve or vacate spots to vehicules 
        /// Using a Dictionary with int key and string value. 
        /// A integer key indicates the number of a parking stall and the string value is
        /// the license number of a parked car.
        /// A license should be an alphanumeric combination of six values, separated by a hyphen in the middle.
        /// For example, “A1A-00V” and “789-30A” are valid licenses
        /// </summary>
        static void Main(string[] args)
        {

            try
            {
                //New object - Creating an instance of the class 
                CarParkMethods carParkApplication  = new CarParkMethods();

                //Calling the method to start Car application getting the capacity
                carParkApplication.GetCapacity();

                //Show options of Car Application after to set the capacity
                carParkApplication.OptionsCarApplication();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError ocurred! " + ex.Message);
            }
            finally
            {
                Console.WriteLine("\nProgram completed"); ;
            }
        }
    }
}
