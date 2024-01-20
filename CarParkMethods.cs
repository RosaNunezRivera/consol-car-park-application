using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace car_park_application
{
    internal class CarParkMethods
    {

        //Create a Dictionary structure with pair values int and string to use in the car aplication
        Dictionary<int, string> carParkDictionary = new Dictionary<int, string>();


        /// <summary>
        /// Method to set a initical capacity of sport in to manage in the Car Park Application 
        /// Creates and adds a set number of keys, from 1 to Capacity
        /// All values must be set in null
        /// </summary>
        /// <param name="capacity"></param>
        public void GetCapacity()
        {
            //Set variables
            string capacityString;
            int capacity;
            bool validCapacity = false;

           
            Console.WriteLine("Car Park Application\n");
            Console.WriteLine("\nSet the number of stall's capacicity ");
            Console.WriteLine("------------------------------------- ");
            do
            {
                Console.Write("\nEnter a number to set the spots' capacity: ");
                capacityString = Console.ReadLine();
                capacityString = capacityString.Trim();
                if (int.TryParse(capacityString, out capacity) && capacity>0)
                {
                    validCapacity = true;
                }

                else
                {
                    Console.WriteLine("Please enter a number greater than cero");
                }

            } while (!validCapacity );

            //Method to initilize CarPark with the capacity indicated
            InitializeCarPark(carParkDictionary, capacity);
        }

        /// <summary>
        /// Method that receive a char variable and convert a integer 
        /// Use a switch case to call each method to the options:
        ///  (1) Add a vehicle
        ///  (2) Vacate a Stall
        ///  (3) Leave a Parkade
        ///  (4) Manifest 
        ///  (5) Exit 
        /// </summary>
        public void OptionsCarApplication()
        {
            try
            {
                //Declare variables
                int option;
                string optionString;
                bool isValidInt = false;
              
                //Do-while to be in a loop while option is not exit
                do
                {
                    do
                    {
                        Console.WriteLine("\nOptions:");
                        Console.WriteLine("--------");
                        Console.WriteLine("(1) Add a vehicle ");
                        Console.WriteLine("(2) Vacate a Stall ");
                        Console.WriteLine("(3) Leave a Parkade");
                        Console.WriteLine("(4) Manifest");
                        Console.WriteLine("(5) Exit");
                        //Gettint the string enter by the user and parse if is a integer 
                        Console.Write("\nEnter an option (1-5): ");
                        optionString = Console.ReadLine();

                        //Set the caracter enter by the user without whitespace
                        optionString = optionString.Trim();

                        // Get a string and using a Char.TryParse() method to get a boolean value to validate a valid char input
                        isValidInt = int.TryParse(optionString, out option);

                        if (!isValidInt)
                        {
                            Console.WriteLine("Only numbers are valid, please enter numbers 1-5");
                        }
                        else if (option <= 0 || option > 5)
                        {
                            Console.WriteLine("Please, enter numbers 1-5");
                        }

                    } while (!isValidInt || option <= 0 || option > 5);

                   //Evaluating each value of the option enter by the user
                    switch (option)
                    {
                        case 1:
                            GetInfoVehicule(carParkDictionary);
                            break;
                        case 2:
                            GetVacateStall(carParkDictionary);
                            break;
                        case 3:
                            GetLicenseToLeaveAParkade(carParkDictionary);
                            break;
                        case 4:
                            Manifest(carParkDictionary);
                            break;
                        case 5:
                            ClearCarApplication(carParkDictionary);
                            Console.WriteLine("\nThanks for use this CarApplication");
                            break;
                        default:
                            break;
                    }
                } while (option !=5);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
        }

        //Method to set the capacity Adding n elementos representing of the stall in a directory<int, string>. 
        public void InitializeCarPark(Dictionary<int, string> carParkDictionary, int capacity) 
        {
            try 
            {
                //Add stall until achieve the capacity enter by the user with null value
                string keyValue = null;
                for (int key = 1; key <= capacity; key++)
                {
                    // Adding key-value pairs in the directory 
                    carParkDictionary.Add(key, keyValue);
                }
                Console.WriteLine($"\nThe capacity has been created with {capacity} stalls");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }

        }

        /// <summary>
        /// Methods to find the first available stall (null value) and “reserves” it by setting the value to the
        /// car’s license.
        /// This method must return the integer value of the stall (Ie. its key) if successful insted of 
        /// must return -1 that means there are no unoccupied stalls or the license is invalid.
        /// </summary>
        public void GetInfoVehicule(Dictionary<int, string> carParkDictionary) 
        {
            try 
            {
                //Declaring variables 
                string license;
                bool isLicenseValid = false;
                int resultAddVehicle = -1;

                //Ask and validate a licence to add an vehicule 
                do
                {
                    Console.WriteLine("\nOption 1: Add a vehicle ");
                    Console.WriteLine("---------------------------------------- ");
                    Console.Write("Enter a licence ( 6 letters and numbers separed with '-' like A0B-C1A ): ");
                    license = Console.ReadLine().ToUpper();
                    license = license.Trim();

                    //Ckecking if the licence is valid like a A0B-C1A
                    //first three catacters must be letters and the last ended must be numbers, both are by separated by hyphen in the middle (-)  
                    if (ValidLicense(license) && license.Length > 0)
                    {
                        isLicenseValid = true;
                        //Getting the result of method AddVehicle -1 or key number of stall 
                        resultAddVehicle = AddVehicule(carParkDictionary, license);

                        if (resultAddVehicle != -1)
                        {
                            Console.WriteLine($"\nThe vehicle with license {license} was parked in the stall {resultAddVehicle}");
                        }
                        else
                        {
                            Console.WriteLine($"\nThe vehicle with license {license} cannot be parked");
                        }
                    }
                    else
                    {
                        Console.Write("\nPlease, enter a valid licence using this format A0B-C1A");
                    }
                } while (!isLicenseValid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
        }

        //Method to use a regex to check if user enter a valid licence
        public bool ValidLicense(string license)
        {
            // Define the regular expression pattern
            string pattern = @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{3}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Return a boolen value after check if the license matches the pattern
            return Regex.IsMatch(license, pattern);
        }

        /// <summary>
        //Find the first “unoccupied” (null) stall and “reserves” it by setting the value to the car’s license.
        //If found any value with null (that means it is unoccupied) return the key.value (integer)
        //If does not found eny value with null(trat means all stall are occupied) return -1
        /// </summary>
        /// <param name="carParkDictionary"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public int AddVehicule(Dictionary<int, string> carParkDictionary, string license) 
        {
            //Declaring the variable and setting with -1 that means any stall was unoccupaid 
            int availableStall = -1;

            try 
            {
                //Cheking if the car already was not parked - validating if the license is founded in any value
                if (!carParkDictionary.ContainsValue(license))
                {
                    foreach (var kvp in carParkDictionary)
                    {
                        if (kvp.Value == null)
                        {
                            //Find the first null value and storing the key
                            availableStall = kvp.Key;
                            break;
                        }
                    }

                    //Defining resultAddVehicle 
                    // -1 all occupied stall founded
                    // Positive number means the key number if a stall was found unoccupied 
                    if (availableStall != -1)
                    {
                        //Adding the license's car in the stall
                        carParkDictionary[availableStall] = license;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
            return availableStall;
        }

        /// <summary>
        /// Method to get a stall, call the method VacateStall and show the result
        /// </summary>
        /// <param name="carParkDictionary"></param>       
        public void GetVacateStall(Dictionary<int, string> carParkDictionary)
        {
            //Set variables
            string vacateStallString="";
            int vacateStall;
            bool validVacate = false;

            Console.WriteLine("\nOption 3: Vacate a stall");
            Console.WriteLine("---------------------------------------- ");

            try 
            {
                do
                {
                    Console.Write("Enter a stall number: ");
                    vacateStallString = Console.ReadLine();
                    vacateStallString = vacateStallString.Trim();
                    if (int.TryParse(vacateStallString, out vacateStall) && vacateStall > 0)
                    {
                        validVacate = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number greater than cero");
                    }

                } while (!validVacate);

                //Get boolean value from method that vacate a stall 
                bool resultVacateStall = VacateStall(carParkDictionary, vacateStall);

                //Showing the result of method VacateStall true or false
                if (resultVacateStall)
                {
                    Console.WriteLine($"The stall {vacateStall} has been vacated");
                }
                else
                {
                    Console.WriteLine($"The stall {vacateStall} is unoccupied or does not exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
        }
        /// <summary>
        /// Method to remove an occupying vehicle from a stall. If that stall is unoccupied or does not exist,
        /// return false, otherwise, return true.
        /// </summary>
        public bool VacateStall(Dictionary<int, string> carParkDictionary, int vacateStall) 
        {
            bool resultVacated = false;

            try 
            {
                if (carParkDictionary.ContainsKey(vacateStall))
                {
                    // Find the key of the vacated stall 
                    var pairVacatedStall = carParkDictionary.FirstOrDefault(item => item.Key == vacateStall);

                    // Get the key to vacate
                    int keyToVacate = pairVacatedStall.Key;

                    if (carParkDictionary[keyToVacate] != null) 
                    {
                        // Vacate the stall by setting its value to null
                        carParkDictionary[keyToVacate] = null;
                        resultVacated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
            return resultVacated;
        }
        /// <summary>
        /// Method that get a license, call the method LeaveParkade and show the result
        /// </summary>
        /// <param name="carParkDictionary"></param>
        public void GetLicenseToLeaveAParkade(Dictionary<int, string> carParkDictionary) 
        {
            //Declaring variables 
            string license;
            bool isLicenseValid=false;
            bool resultLeaveAParked = false;

            try 
            {
                //Ask and validate a licence to add an vehicule 
                do
                {
                    Console.WriteLine("\nOption 3:  Leave a Parkade ");
                    Console.WriteLine("---------------------------------------- ");
                    Console.Write("Enter a licence ( 6 letters and numbers separed with '-' like A0B-C1A ): ");
                    license = Console.ReadLine().ToUpper();
                    license = license.Trim();

                    //Ckecking if the licence is valid like a A0B-C1A
                    //first three catacters must be letters and the last ended must be numbers, both are by separated by hyphen in the middle (-)  
                    if (ValidLicense(license) && license.Length > 0)
                    {
                        isLicenseValid = true;
                        //Getting the result of method LeaveParkade is true or false  
                        resultLeaveAParked = LeaveParkade(carParkDictionary, license);

                        if (resultLeaveAParked)
                        {
                            Console.WriteLine($"\nThe vehicle with license {license} is no longer parked now");
                        }
                        else
                        {
                            Console.WriteLine($"\nThe vehicle with license {license} was not founded in the parking lot");
                        }
                    }
                    else
                    {
                        Console.Write("\nPlease, enter a valid licence using this format A0B-C1A");
                    }
                } while (!isLicenseValid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
        }

        /// <summary>
        /// Method to Leave a parkade using the license's car to find the key 
        /// in a directory and set the value in null
        /// </summary>
        /// <param name="carParkDictionary"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public bool LeaveParkade(Dictionary<int, string> carParkDictionary, string license) 
        {
            //Declaring variables
            bool resultLeaveParkade = false;

            try 
            {
                if (carParkDictionary.ContainsValue(license))
                {
                    // Find the key of the stall to leave a parkade
                    var pairLeaveStall = carParkDictionary.FirstOrDefault(item => item.Value == license);

                    // Get the key of the stall
                    int keyToLeave = pairLeaveStall.Key;

                    // Leave the stall by setting its value to null
                    carParkDictionary[keyToLeave] = null;

                    resultLeaveParkade = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred! " + ex.Message);
            }
            return resultLeaveParkade;
        }

        /// <summary>
        /// Printing the stalles, license of the car or show Unoccupied if the stall is free
        /// </summary>
        /// <param name="carParkDictionary"></param>
        public void Manifest(Dictionary<int, string> carParkDictionary)
        {
            // Iterating through the dictionary
            Console.WriteLine("\nOption 4: Manifest Car Park Application");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(" Stall |  License's car       ");
            Console.WriteLine("-----------------------------------------");

            foreach (var kvp in carParkDictionary)
            {
                //Using null-coalescing operator to show null in each value
                string valueToPrint = kvp.Value ?? "UNOCCUPIED";
                Console.WriteLine($"   {kvp.Key}   |  {valueToPrint}");
            }
            Console.WriteLine("-----------------------------------------");
        }

        //Method to clear struct of data before exit of the application    
        public void ClearCarApplication(Dictionary<int, string> carParkDictionary)
        {
            //Clear a dictionary 
            carParkDictionary.Clear();
        }
    }
}