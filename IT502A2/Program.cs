using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo;
        public bool IsAllocated;
        public Customer AllocatedCustomer;
        public Room(int roomNo)
        {
            RoomNo = roomNo;
            IsAllocated = false;
        }
    }
    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo;
        public string CustomerName;
    }
    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int AllocatedRoomNo;
        public Customer AllocatedCustomer;

        public RoomAllocation()
        {
            AllocatedRoomNo = -1;
            AllocatedCustomer = new Customer();
        }
    }
    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static Room[] listofRooms = new Room[100];
        public static RoomAllocation[] listOfRoomAllocations = new RoomAllocation[100];
        public static string filePath;
        // Main function
        static void Main(string[] args)
        {
            try
            {
                RoomInitializing();

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                filePath = Path.Combine(folderPath, "HotelManagement.txt");

                char ans;
                do
                {
                    Menu();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    MenuChoice(choice);
                    Console.Write("\nWould You Like To Continue(Y/N): ");
                    ans = Convert.ToChar(Console.ReadLine());
                } while (ans == 'y' || ans == 'Y');
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException handled. Exception is: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationExeption handled. Exception is: " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("FileNotFoundException handled. Exception is: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("UnauthorizedAccessException handled. Exception is: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Thank you for visiting. Application will be closed now.");
            }
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine(" LANGHAM HOTEL MANAGEMENT SYSTEM ");
            Console.WriteLine(" MENU ");
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("1. Add Rooms");
            Console.WriteLine("2. Display Rooms");
            Console.WriteLine("3. Allocate Rooms");
            Console.WriteLine("4. De-Allocate Rooms");
            Console.WriteLine("5. Display Room Allocation Details");
            Console.WriteLine("6. Billing");
            Console.WriteLine("7. Save the Room Allocations To a File");
            Console.WriteLine("8. Show the Room Allocations From a File");
            Console.WriteLine("9. Exit");
            Console.WriteLine("10. Backup");
            Console.WriteLine("***********************************************************************************");
            Console.Write("Enter Your Choice Number Here: ");
        }

        static void MenuChoice(int choice)
        {
                switch (choice)
                {
                    case 1:
                        // adding Rooms function
                        AddRooms();
                        break;
                    case 2:
                        DisplayRooms();
                        break;
                    case 3:
                        AllocateRooms();
                        break;
                    case 4:
                        DeAllocateRooms();
                        break;
                    case 5:
                        DisplayRoomAllocations();
                        break;
                    case 6:
                        // Display "Billing Feature is Under Construction and will be added soon...!!!.
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        SaveRoomAllocationsToFile();
                        break;
                    case 8:
                        //Show Room Allocations From File
                        ShowRoomAllocationsFromFile();
                        break;
                    case 9:
                        // Exit Application
                        Environment.Exit(0);
                        break;
                    case 10:
                        //Backup
                        break;
                    default:
                        Console.WriteLine("Choice is invalid. Pick a number from 1-10.");
                        break;
                }
        }

        static void RoomInitializing()
        {
            for (int i = 0; i < listofRooms.Length; i++)
            {
                listofRooms[i] = new Room(i + 1);
                listOfRoomAllocations[i] = new RoomAllocation();
            }
        }

        static void AddRooms()
        {
            try
            {
                Console.WriteLine("|| Add Rooms ||");

                Console.Write("Enter how many rooms you would like to add: ");
                int numOfRooms = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("The number of rooms you would like to add is: " + numOfRooms);

                for (int i = 0; i < numOfRooms; i++)
                {
                    listofRooms[i] = new Room(i + 1);
                }

                Console.WriteLine("You have successfully added the rooms.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input, please enter an integer number. The exception is " + ex.Message);
            }
        }

        static void DisplayRooms()
        {
            Console.WriteLine("|| List Of Rooms ||");

            for (int i = 0; i < listofRooms.Length;i++)
            {
                Room roomNow = listofRooms[i];
                string Status;
                if (roomNow.IsAllocated)
                {
                    Status = "Allocated";
                }
                else
                {
                    Status = "Not Allocated";
                }
                Console.WriteLine("Room Number: " + roomNow.RoomNo + " Status: " + Status);
                Console.WriteLine();
            }
        }

        static void AllocateRooms()
        {
            try
            {
                Console.WriteLine("|| Allocate Rooms ||");

                Console.Write("Which room would you like to allocate (1-100): ");
                int RoomNumber = Convert.ToInt32(Console.ReadLine());

                if (RoomNumber < 1 || RoomNumber > 100)
                {
                    Console.WriteLine("Invalid input. Enter an integer number from 1 - 100.");
                    return;
                }
                
                if (listofRooms[RoomNumber - 1].IsAllocated)
                {
                    Console.WriteLine("This room has already been allocated.");
                    return;
                }
                Customer thecustomer = new Customer();

                Console.Write("Enter customer number: ");
                int CustomerNumber = Convert.ToInt32(Console.ReadLine());
                thecustomer.CustomerNo = CustomerNumber;

                Console.Write("Enter customer name: ");
                string NameOfCustomer = Console.ReadLine();
                thecustomer.CustomerName = NameOfCustomer;

                RoomAllocation AllocationRoom = new RoomAllocation();

                AllocationRoom.AllocatedCustomer = thecustomer;

                AllocationRoom.AllocatedRoomNo = RoomNumber;

                listOfRoomAllocations[RoomNumber - 1] = AllocationRoom;

                listofRooms[RoomNumber - 1].IsAllocated = true;

                Console.WriteLine("Room " + RoomNumber + " has been allocated successfully by " + NameOfCustomer);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Your input is invalid. Only enter integer values from 1 - 100. The exception is " + ex.Message);
            }
        }
        static void DeAllocateRooms()
        {
            try
            {
                Console.WriteLine("|| De-Allocate Room ||");

                Console.WriteLine("Current Room Allocation Status: ");
                for (int i = 0; i < listofRooms.Length; i++)
                {
                    Room room = listofRooms[i];
                    Console.WriteLine("Room " + room.RoomNo + " Allocation status: " + room.IsAllocated);
                }

                Console.Write("Pick a room to de-allocate: ");
                int DARoomNo = Convert.ToInt32(Console.ReadLine());

                if (DARoomNo < 1 || DARoomNo > 100)
                {
                    Console.WriteLine("Invalid input. Enter integer number from 1 - 100");
                    return;
                }

                if (listofRooms[DARoomNo - 1].IsAllocated)
                {
                    Console.WriteLine("This room is currently allocated.");
                }
                else
                {
                    Console.WriteLine("This room is currently not allocated.");
                    return;
                }

                listofRooms[DARoomNo - 1].IsAllocated = false;

                Console.WriteLine("Room " + DARoomNo + " has been de-allocted successfully");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid input. Enter integer number from 1 - 100. The exception is " + ex.Message);
            }
        }

        static void DisplayRoomAllocations()
        {
            Console.WriteLine("|| Room Allocation Details ||");

            foreach (RoomAllocation RoomAllocation in listOfRoomAllocations)
            {
                if (RoomAllocation.AllocatedRoomNo != -1)
                {
                    Console.WriteLine("Room Number: " + RoomAllocation.AllocatedRoomNo);
                    Console.WriteLine("Customer Number: " + RoomAllocation.AllocatedCustomer.CustomerNo);
                    Console.WriteLine("Customer Name: " + RoomAllocation.AllocatedCustomer.CustomerName);
                    Console.WriteLine();
                }
            }
        }

        static void SaveRoomAllocationsToFile()
        {
            FileStream fs = new FileStream("C:\\Users\\coper\\source\\repos\\HotelManagement\\lhms_studentid.txt", FileMode.Create);
            fs.Close();
            Console.Write("File has been created successfully");

            string sDateTime = DateTime.Now.ToString();

            string filepath = @"C:\Users\coper\source\repos\HotelManagement\lhms_studentid.txt";

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (RoomAllocation RoomAllo in listOfRoomAllocations)
                {
                    if (RoomAllo.AllocatedRoomNo != -1)
                    {
                        writer.WriteLine("Time stamp: " + sDateTime);
                        writer.WriteLine("Room Number: " + RoomAllo.AllocatedRoomNo);
                        writer.WriteLine("Customer Number: " + RoomAllo.AllocatedCustomer.CustomerNo);
                        writer.WriteLine("Customer Name: " + RoomAllo.AllocatedCustomer.CustomerName);
                        writer.WriteLine();
                    }
                }
            }
        }

        static void ShowRoomAllocationsFromFile()
        {
            string path = @"C:\Users\coper\source\repos\HotelManagement\lhms_studentid.txt";

            string all_text;
            Console.WriteLine("|| Showing all allocations in the file ||");
            all_text = File.ReadAllText(path);
            Console.WriteLine(all_text);
        }
    }
}
